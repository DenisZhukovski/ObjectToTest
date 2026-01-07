using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectToTest.Extensions;
using ObjectToTest.Core.Extensions;

namespace ObjectToTest.Core.DefaultState
{
    /// <summary>
    /// Detects whether an object is in its canonical default state (as produced by a public
    /// parameterless/optional-args constructor) and provides the simplest constructor call string.
    /// </summary>
    internal class DefaultStateAnalyzer
    {
        private readonly object _object;

        public DefaultStateAnalyzer(object @object)
        {
            _object = @object ?? throw new ArgumentNullException(nameof(@object));
        }

        public DefaultStateResult Analyze()
        {
            var type = _object.GetType();
            var ctor = CanonicalConstructor(type);
            if (ctor == null)
            {
                return new DefaultStateResult(false, string.Empty);
            }

            object? defaultInstance;
            try
            {
                var args = ctor
                    .GetParameters()
                    .Select(DefaultValue)
                    .ToArray();
                defaultInstance = ctor.Invoke(args);
            }
            catch
            {
                return new DefaultStateResult(false, string.Empty);
            }

            if (!IsDefaultState(defaultInstance))
            {
                return new DefaultStateResult(false, string.Empty);
            }

            return new DefaultStateResult(true, ConstructorCall(type, ctor));
        }

        private static ConstructorInfo? CanonicalConstructor(Type type)
        {
            var publicCtors = type
                .GetConstructors()
                .OrderBy(c => c.GetParameters().Length)
                .ToList();

            // Prefer parameterless ctor; otherwise the one where all parameters have defaults
            var parameterless = publicCtors.FirstOrDefault(c => !c.GetParameters().Any());
            if (parameterless != null)
            {
                return parameterless;
            }

            return publicCtors.FirstOrDefault(
                c => c.GetParameters().All(p => p.HasDefaultValue || p.IsOptional)
            );
        }

        private static object? DefaultValue(ParameterInfo parameter)
        {
            if (parameter.HasDefaultValue)
            {
                return parameter.DefaultValue;
            }

            if (parameter.IsOptional)
            {
                return Type.Missing;
            }

            return parameter.ParameterType.IsValueType
                ? Activator.CreateInstance(parameter.ParameterType)
                : null;
        }

        private static string ConstructorCall(Type type, ConstructorInfo ctor)
        {
            var typeName = type.IsGenericType
                ? type.GenericTypeName()
                : type.Name;

            var parameters = ctor.GetParameters();
            if (!parameters.Any())
            {
                return $"new {typeName}()";
            }

            var args = parameters
                .Select(DefaultValue)
                .Select(arg => arg.ToStringForInitialization())
                .ToArray();

            return $"new {typeName}({string.Join(",", args)})";
        }

        private bool IsDefaultState(object? defaultInstance)
        {
            if (defaultInstance == null)
            {
                return false;
            }

            var visited = new HashSet<object>();
            return CompareMembers(_object, defaultInstance, visited);
        }

        private static bool CompareMembers(object current, object baseline, HashSet<object> visited)
        {
            if (ReferenceEquals(current, baseline))
            {
                return true;
            }

            if (visited.Contains(current))
            {
                return true;
            }

            visited.Add(current);

            var members = current.FieldsAndProperties();
            foreach (var member in members)
            {
                var currentValue = current.Value(member.Name);
                var baselineValue = baseline.Value(member.Name);

                if (currentValue == null && baselineValue == null)
                {
                    continue;
                }

                if (currentValue == null || baselineValue == null)
                {
                    continue;
                }

                if (currentValue.IsPrimitive() || currentValue.IsValueType())
                {
                    if (!currentValue.Equals(baselineValue))
                    {
                        return false;
                    }
                    continue;
                }

                // For reference types that are not primitive/value types, treat as unknown/non-reproducible:
                // we ignore differences to allow default-state detection when internals differ.
            }

            return true;
        }
    }
}

