using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ObjectToTest.Extensions;

namespace ObjectToTest.Core.DefaultState
{
    /// <summary>
    /// Detects whether an object is in the same observable state as one created with
    /// its canonical default constructor (including chained secondary constructors).
    /// For now, we focus on public readable instance members that are safe to compare.
    /// Unknown/non-reproducible members are ignored.
    /// </summary>
    internal static class DefaultStateEvaluator
    {
        private static readonly ConcurrentDictionary<Type, DefaultStateDescriptor?> Cache = new();

        public static bool IsDefaultState(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            var type = obj.GetType();
            var descriptor = Cache.GetOrAdd(type, BuildDescriptor);
            if (descriptor == null)
            {
                return false;
            }

            if (!descriptor.NonComparableFrameworkType && descriptor.Members.Count == 0)
            {
                // No comparable members means we cannot confidently assert default state.
                return false;
            }

            // For explicitly non-comparable framework types (e.g., HttpClient) we only
            // require that the default instance is constructible; everything else is ignored.
            if (descriptor.NonComparableFrameworkType)
            {
                return true;
            }

            foreach (var member in descriptor.Members)
            {
                var current = member.Reader(obj);
                if (!ValuesEqual(current, member.DefaultValue))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool TryGetDefaultConstructorCall(object obj, out ConstructorInfo? ctor, out object?[] arguments)
        {
            ctor = null;
            arguments = Array.Empty<object?>();
            if (obj == null)
            {
                return false;
            }

            var descriptor = Cache.GetOrAdd(obj.GetType(), BuildDescriptor);
            if (descriptor == null)
            {
                return false;
            }

            if (!IsDefaultState(obj))
            {
                return false;
            }

            ctor = descriptor.Constructor;
            arguments = descriptor.ConstructorArguments;
            return true;
        }

        private static DefaultStateDescriptor? BuildDescriptor(Type type)
        {
            try
            {
                // Ensure the type is decompilable; if not, bail out to avoid surprises.
                if (!TypeIsDecompilable(type))
                {
                    return null;
                }

                var ctor = CanonicalConstructor(type);
                if (ctor == null)
                {
                    return null;
                }

                var defaultInstance = CreateInstanceWithDefaults(type, ctor);
                if (defaultInstance == null)
                {
                    return null;
                }

                var ctorArguments = DefaultArguments(ctor);

                // For HttpClient and similar framework types, we intentionally avoid comparing
                // any public members because their internal/default state is not reproducible.
                if (IsNonReproducibleFrameworkType(type))
                {
                    return new DefaultStateDescriptor(
                        type,
                        nonComparableFrameworkType: true,
                        members: Array.Empty<MemberDefault>(),
                        constructor: ctor,
                        constructorArguments: ctorArguments
                    );
                }

                var members = CollectComparableMembers(type, defaultInstance).ToArray();
                return new DefaultStateDescriptor(
                    type,
                    nonComparableFrameworkType: false,
                    members,
                    ctor,
                    ctorArguments
                );
            }
            catch
            {
                return null;
            }
        }

        private static bool TypeIsDecompilable(Type type)
        {
            try
            {
                var decompiler = new CSharpDecompiler(
                    type.Assembly.Location,
                    new DecompilerSettings(LanguageVersion.Latest)
                );
                return decompiler.Type(type.FullName!) != null;
            }
            catch
            {
                return false;
            }
        }

        private static ConstructorInfo? CanonicalConstructor(Type type)
        {
            var ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            if (!ctors.Any())
            {
                return null;
            }

            // Prefer the ctor that no other ctor calls with "this(...)" (terminal of the chain).
            var callTargets = new HashSet<ConstructorInfo>(
                ctors
                    .Select(TryGetChainedConstructor)
                    .Where(r => r.Success && r.Target != null)
                    .Select(r => r.Target!)
            );

            var terminal = ctors.FirstOrDefault(c => !callTargets.Contains(c));
            if (terminal != null)
            {
                return terminal;
            }

            // Fallback: parameterless, then widest optional/defaultable.
            var parameterless = ctors.FirstOrDefault(c => c.GetParameters().Length == 0);
            if (parameterless != null)
            {
                return parameterless;
            }

            return ctors.FirstOrDefault(c => c.GetParameters().All(IsOptionalOrDefaultable));
        }

        private static bool IsOptionalOrDefaultable(ParameterInfo parameter)
        {
            return parameter.HasDefaultValue || parameter.IsOptional || parameter.ParameterType.IsValueType;
        }

        private static object? CreateInstanceWithDefaults(Type type, ConstructorInfo ctor)
        {
            try
            {
                // If ctor chains to another ctor with constants, use those constants.
                var chain = TryGetChainedConstructor(ctor);
                if (chain.Success && chain.Target != null)
                {
                    var targetArgs = chain.Arguments ?? chain.Target.GetParameters().Select(DefaultArgumentValue).ToArray();
                    return chain.Target.Invoke(targetArgs);
                }

                var args = ctor.GetParameters().Select(DefaultArgumentValue).ToArray();
                return ctor.Invoke(args);
            }
            catch
            {
                return null;
            }
        }

        private static object?[] DefaultArguments(ConstructorInfo ctor)
        {
            return ctor
                .GetParameters()
                .Select(DefaultArgumentValue)
                .ToArray();
        }

        private static object? DefaultArgumentValue(ParameterInfo parameter)
        {
            if (parameter.HasDefaultValue)
            {
                return parameter.DefaultValue;
            }

            // For value types, create default; for reference types, use null.
            return parameter.ParameterType.IsValueType
                ? Activator.CreateInstance(parameter.ParameterType)
                : null;
        }

        private static IEnumerable<MemberDefault> CollectComparableMembers(Type type, object defaultInstance)
        {
            var binding = BindingFlags.Instance | BindingFlags.Public;

            foreach (var property in type.GetProperties(binding))
            {
                if (!property.CanRead || property.GetIndexParameters().Any() || property.GetMethod?.IsStatic == true)
                {
                    continue;
                }

                if (!IsComparableType(property.PropertyType))
                {
                    continue;
                }

                object? defaultValue;
                try
                {
                    defaultValue = property.GetValue(defaultInstance);
                }
                catch
                {
                    continue;
                }

                yield return new MemberDefault(property.Name, property.PropertyType, o => property.GetValue(o), defaultValue);
            }

            foreach (var field in type.GetFields(binding))
            {
                if (field.IsStatic)
                {
                    continue;
                }

                if (!IsComparableType(field.FieldType))
                {
                    continue;
                }

                object? defaultValue;
                try
                {
                    defaultValue = field.GetValue(defaultInstance);
                }
                catch
                {
                    continue;
                }

                yield return new MemberDefault(field.Name, field.FieldType, o => field.GetValue(o), defaultValue);
            }
        }

        private static bool IsComparableType(Type type)
        {
            return type.IsPrimitive
                   || type.IsEnum
                   || type == typeof(string)
                   || type == typeof(decimal)
                   || (type.IsValueType && !typeof(System.Collections.IEnumerable).IsAssignableFrom(type));
        }

        private static bool ValuesEqual(object? left, object? right)
        {
            if (left == null && right == null)
            {
                return true;
            }

            if (left == null || right == null)
            {
                return false;
            }

            return left.Equals(right);
        }

        private static bool IsNonReproducibleFrameworkType(Type type)
        {
            return type.FullName == "System.Net.Http.HttpClient";
        }

        private static (bool Success, ConstructorInfo? Target, object?[]? Arguments) TryGetChainedConstructor(ConstructorInfo ctor)
        {
            var body = ctor.GetMethodBody();
            if (body == null)
            {
                return (false, null, null);
            }

            var il = body.GetILAsByteArray();
            if (il == null || il.Length == 0)
            {
                return (false, null, null);
            }

            var reader = new IlReader(ctor.Module, il);
            var stack = new Stack<object?>();

            while (reader.TryReadNext(out var instruction))
            {
                switch (instruction.OpCode.OperandType)
                {
                    case OperandType.InlineNone:
                        PushIfConstant(instruction.OpCode, stack);
                        break;
                    case OperandType.ShortInlineI:
                        if (instruction.OpCode == OpCodes.Ldc_I4_S)
                        {
                            stack.Push((sbyte)instruction.IntOperand);
                        }
                        break;
                    case OperandType.InlineI:
                        if (instruction.OpCode == OpCodes.Ldc_I4)
                        {
                            stack.Push(instruction.IntOperand);
                        }
                        break;
                    case OperandType.InlineI8:
                        if (instruction.OpCode == OpCodes.Ldc_I8)
                        {
                            stack.Push(instruction.LongOperand);
                        }
                        break;
                    case OperandType.ShortInlineR:
                        if (instruction.OpCode == OpCodes.Ldc_R4)
                        {
                            stack.Push(instruction.FloatOperand);
                        }
                        break;
                    case OperandType.InlineR:
                        if (instruction.OpCode == OpCodes.Ldc_R8)
                        {
                            stack.Push(instruction.DoubleOperand);
                        }
                        break;
                    case OperandType.InlineString:
                        stack.Push(instruction.StringOperand);
                        break;
                    case OperandType.InlineMethod:
                        if (instruction.MethodOperand is ConstructorInfo target && target.DeclaringType == ctor.DeclaringType)
                        {
                            var parameters = target.GetParameters();
                            if (stack.Count >= parameters.Length)
                            {
                                var args = new object?[parameters.Length];
                                for (var i = parameters.Length - 1; i >= 0; i--)
                                {
                                    args[i] = stack.Pop();
                                }

                                return (true, target, args);
                            }

                            return (false, null, null);
                        }
                        break;
                }
            }

            return (false, null, null);
        }

        private static void PushIfConstant(OpCode opCode, Stack<object?> stack)
        {
            if (opCode == OpCodes.Ldnull)
            {
                stack.Push(null);
                return;
            }

            if (opCode == OpCodes.Ldc_I4_M1)
            {
                stack.Push(-1);
                return;
            }

            if (opCode == OpCodes.Ldc_I4_0)
            {
                stack.Push(0);
                return;
            }

            if (opCode == OpCodes.Ldc_I4_1)
            {
                stack.Push(1);
                return;
            }

            if (opCode == OpCodes.Ldc_I4_2)
            {
                stack.Push(2);
                return;
            }

            if (opCode == OpCodes.Ldc_I4_3)
            {
                stack.Push(3);
                return;
            }

            if (opCode == OpCodes.Ldc_I4_4)
            {
                stack.Push(4);
                return;
            }

            if (opCode == OpCodes.Ldc_I4_5)
            {
                stack.Push(5);
                return;
            }

            if (opCode == OpCodes.Ldc_I4_6)
            {
                stack.Push(6);
                return;
            }

            if (opCode == OpCodes.Ldc_I4_7)
            {
                stack.Push(7);
                return;
            }

            if (opCode == OpCodes.Ldc_I4_8)
            {
                stack.Push(8);
            }
        }

        private sealed class IlReader
        {
            private static readonly OpCode[] SingleByteOpCodes = new OpCode[0x100];
            private static readonly OpCode[] MultiByteOpCodes = new OpCode[0x100];

            static IlReader()
            {
                foreach (var fi in typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    if (fi.GetValue(null) is OpCode op)
                    {
                        var value = (ushort)op.Value;
                        if (value < 0x100)
                        {
                            SingleByteOpCodes[value] = op;
                        }
                        else if ((value & 0xff00) == 0xfe00)
                        {
                            MultiByteOpCodes[value & 0xff] = op;
                        }
                    }
                }
            }

            private readonly Module _module;
            private readonly byte[] _il;
            private int _position;

            public IlReader(Module module, byte[] il)
            {
                _module = module;
                _il = il;
                _position = 0;
            }

            public bool TryReadNext(out IlInstruction instruction)
            {
                instruction = default;
                if (_position >= _il.Length)
                {
                    return false;
                }

                var op = ReadOpCode();
                instruction = new IlInstruction(op)
                {
                    IntOperand = ReadIntOperand(op),
                    LongOperand = ReadLongOperand(op),
                    FloatOperand = ReadFloatOperand(op),
                    DoubleOperand = ReadDoubleOperand(op),
                    StringOperand = ReadStringOperand(op),
                    MethodOperand = ReadMethodOperand(op)
                };

                return true;
            }

            private OpCode ReadOpCode()
            {
                var code = _il[_position++];
                if (code != 0xfe)
                {
                    return SingleByteOpCodes[code];
                }

                var second = _il[_position++];
                return MultiByteOpCodes[second];
            }

            private int ReadIntOperand(OpCode opCode)
            {
                if (opCode.OperandType == OperandType.InlineI)
                {
                    var value = BitConverter.ToInt32(_il, _position);
                    _position += 4;
                    return value;
                }

                if (opCode.OperandType == OperandType.ShortInlineI)
                {
                    var value = _il[_position];
                    _position += 1;
                    return value;
                }

                return 0;
            }

            private long ReadLongOperand(OpCode opCode)
            {
                if (opCode.OperandType == OperandType.InlineI8)
                {
                    var value = BitConverter.ToInt64(_il, _position);
                    _position += 8;
                    return value;
                }

                return 0;
            }

            private float ReadFloatOperand(OpCode opCode)
            {
                if (opCode.OperandType == OperandType.ShortInlineR && opCode == OpCodes.Ldc_R4)
                {
                    var value = BitConverter.ToSingle(_il, _position);
                    _position += 4;
                    return value;
                }

                return 0;
            }

            private double ReadDoubleOperand(OpCode opCode)
            {
                if (opCode.OperandType == OperandType.InlineR && opCode == OpCodes.Ldc_R8)
                {
                    var value = BitConverter.ToDouble(_il, _position);
                    _position += 8;
                    return value;
                }

                return 0;
            }

            private string? ReadStringOperand(OpCode opCode)
            {
                if (opCode.OperandType == OperandType.InlineString)
                {
                    var token = BitConverter.ToInt32(_il, _position);
                    _position += 4;
                    return _module.ResolveString(token);
                }

                return null;
            }

            private MethodBase? ReadMethodOperand(OpCode opCode)
            {
                if (opCode.OperandType == OperandType.InlineMethod)
                {
                    var token = BitConverter.ToInt32(_il, _position);
                    _position += 4;
                    try
                    {
                        return _module.ResolveMethod(token);
                    }
                    catch
                    {
                        return null;
                    }
                }

                return null;
            }
        }

        private struct IlInstruction
        {
            public IlInstruction(OpCode opCode)
            {
                OpCode = opCode;
                IntOperand = 0;
                LongOperand = 0;
                FloatOperand = 0;
                DoubleOperand = 0;
                StringOperand = null;
                MethodOperand = null;
            }

            public OpCode OpCode { get; }
            public int IntOperand { get; set; }
            public long LongOperand { get; set; }
            public float FloatOperand { get; set; }
            public double DoubleOperand { get; set; }
            public string? StringOperand { get; set; }
            public MethodBase? MethodOperand { get; set; }
        }
    }

    internal sealed class DefaultStateDescriptor
    {
        public DefaultStateDescriptor(
            Type type,
            bool nonComparableFrameworkType,
            MemberDefault[] members,
            ConstructorInfo constructor,
            object?[] constructorArguments)
        {
            Type = type;
            NonComparableFrameworkType = nonComparableFrameworkType;
            Members = members;
            Constructor = constructor;
            ConstructorArguments = constructorArguments;
        }

        public Type Type { get; }

        public bool NonComparableFrameworkType { get; }

        public IReadOnlyList<MemberDefault> Members { get; }

        public ConstructorInfo Constructor { get; }

        public object?[] ConstructorArguments { get; }
    }

    internal sealed class MemberDefault
    {
        public MemberDefault(string name, Type type, Func<object, object?> reader, object? defaultValue)
        {
            Name = name;
            Type = type;
            Reader = reader;
            DefaultValue = defaultValue;
        }

        public string Name { get; }

        public Type Type { get; }

        public Func<object, object?> Reader { get; }

        public object? DefaultValue { get; }
    }
}

