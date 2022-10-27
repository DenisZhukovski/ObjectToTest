using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace ObjectToTest.ILReader
{
    public class MethodBody
    {
        private readonly MethodInfo _methodInfo;

        public MethodBody(MethodInfo methodInfo)
        {
            _methodInfo = methodInfo;
        }

        public List<ILInstruction> Instructions() 
        {
            var body = _methodInfo.GetMethodBody();
            if (body != null)
            {
                return Instructions(body.GetILAsByteArray());
            }

            return new List<ILInstruction>();
        }
        
        /// <summary>
        /// Constructs the array of ILInstructions according to the IL byte code.
        /// </summary>
        private List<ILInstruction> Instructions(byte[] bodyIL)
        {
            int position = 0;
            var instructions = new List<ILInstruction>();
            while (position < bodyIL.Length)
            {
                var code = bodyIL.InstructionOpCode(ref position);
                var operandPosition = position - 1;
                instructions.Add(
                    new ILInstruction(
                            code,
                            Operand(code, bodyIL, ref position),
                            operandPosition
                        )
                );
            }

            return instructions;
        }
        
        /// <summary>
        /// Gets the IL code of the method
        /// </summary>
        public override string ToString()
        {
            return string.Join(Environment.NewLine, Instructions());
        }
        
        /// <summary>
        ///  Gets the operand of the current operation
        /// </summary>
        private object? Operand(OpCode code, byte[] il, ref int position)
        {
            object? operand = null;
               
                switch (code.OperandType)
                {
                    case OperandType.InlineBrTarget:
                        operand = il.ReadInt32(ref position) + position;
                        break;
                    case OperandType.InlineField:
                        operand = _methodInfo.Module.ResolveField( 
                            il.ReadInt32(ref position)
                        );
                        break;
                    case OperandType.InlineMethod:
                        var metadataToken = il.ReadInt32(ref position);
                        try
                        {
                            operand = _methodInfo.Module.ResolveMethod(metadataToken);
                        }
                        catch
                        {
                            operand = _methodInfo.Module.ResolveMember(metadataToken);
                        }
                        break;
                    case OperandType.InlineSig:
                        operand = _methodInfo.Module.ResolveSignature(il.ReadInt32(ref position));
                        break;
                    case OperandType.InlineTok:
                        try
                        {
                            operand = _methodInfo.Module.ResolveType(il.ReadInt32(ref position));
                        }
                        catch
                        {
                            // ignored
                        }

                        // SSS : see what to do here
                        break;
                    case OperandType.InlineType:
                        // now we call the ResolveType always using the generic attributes type in order
                        // to support decompilation of generic methods and classes
                        // thanks to the guys from code project who commented on this missing feature
                        operand = _methodInfo.Module.ResolveType(
                            il.ReadInt32(ref position),
                            _methodInfo.DeclaringType.GetGenericArguments(),
                            _methodInfo.GetGenericArguments()
                        );
                        break;
                    case OperandType.InlineI:
                        operand = il.ReadInt32(ref position);
                        break;
                    case OperandType.InlineI8:
                        operand = il.ReadInt64(ref position);
                        break;
                    case OperandType.InlineNone:
                        operand = null;
                        break;
                    case OperandType.InlineR:
                        operand = il.ReadDouble(ref position);
                        break;
                    case OperandType.InlineString:
                        operand = _methodInfo.Module.ResolveString(il.ReadInt32(ref position));
                        break;
                    case OperandType.InlineSwitch:
                        int count = il.ReadInt32(ref position);
                        int[] casesAddresses = new int[count];
                        for (int i = 0; i < count; i++)
                        {
                            casesAddresses[i] = il.ReadInt32(ref position);
                        }
                        int[] cases = new int[count];
                        for (int i = 0; i < count; i++)
                        {
                            cases[i] = position + casesAddresses[i];
                        }
                        break;
                    case OperandType.InlineVar:
                        operand = il.ReadUInt16(ref position);
                        break;
                    case OperandType.ShortInlineBrTarget:
                        operand = il.ReadSByte(ref position) + position;
                        break;
                    case OperandType.ShortInlineI:
                        operand = il.ReadSByte(ref position);
                        break;
                    case OperandType.ShortInlineR:
                        operand = il.ReadSingle(ref position);
                        break;
                    case OperandType.ShortInlineVar:
                        operand = il.ReadByte(ref position);
                        break;
                    default:
                        throw new Exception("Unknown operand type.");
                }

                return operand;
        }
    }
}
