using System;
using System.Reflection.Emit;

namespace ObjectToTest.ILReader
{
    public class ILInstruction
    {
        public ILInstruction(OpCode code, object? operand, int offset)
        {
            Code = code;
            Operand = operand;
            Offset = offset;
        }
        
        public OpCode Code { get; }

        public object? Operand { get; }

        public int Offset { get; }
        
        /// <summary>
        /// Returns a friendly string representation of this instruction
        /// </summary>
        public override string ToString()
        {
            string result = ExpandedOffset(Offset) + " : " + Code;
            if (Operand != null)
            {
                switch (Code.OperandType)
                {
                    case OperandType.InlineField:
                        System.Reflection.FieldInfo fOperand = (System.Reflection.FieldInfo)Operand;
                        result += " " + Globals.ProcessSpecialTypes(fOperand.FieldType.ToString()) + " " +
                            Globals.ProcessSpecialTypes(fOperand.ReflectedType.ToString()) +
                            "::" + fOperand.Name + "";
                        break;
                    case OperandType.InlineMethod:
                        try
                        {
                            System.Reflection.MethodInfo mOperand = (System.Reflection.MethodInfo)Operand;
                            result += " ";
                            if (!mOperand.IsStatic) result += "instance ";
                            result += Globals.ProcessSpecialTypes(mOperand.ReturnType.ToString()) +
                                " " + Globals.ProcessSpecialTypes(mOperand.ReflectedType.ToString()) +
                                "::" + mOperand.Name + "()";
                        }
                        catch
                        {
                            try
                            {
                                System.Reflection.ConstructorInfo mOperand = (System.Reflection.ConstructorInfo)Operand;
                                result += " ";
                                if (!mOperand.IsStatic) result += "instance ";
                                result += "void " +
                                    Globals.ProcessSpecialTypes(mOperand.ReflectedType.ToString()) +
                                    "::" + mOperand.Name + "()";
                            }
                            catch
                            {
                                // ignored
                            }
                        }
                        break;
                    case OperandType.ShortInlineBrTarget:
                    case OperandType.InlineBrTarget:
                        result += " " + ExpandedOffset((int)Operand);
                        break;
                    case OperandType.InlineType:
                        result += " " + Globals.ProcessSpecialTypes(Operand.ToString());
                        break;
                    case OperandType.InlineString:
                        if (Operand.ToString() == "\r\n") result += " \"\\r\\n\"";
                        else result += " \"" + Operand.ToString() + "\"";
                        break;
                    case OperandType.ShortInlineVar:
                        result += Operand.ToString();
                        break;
                    case OperandType.InlineI:
                    case OperandType.InlineI8:
                    case OperandType.InlineR:
                    case OperandType.ShortInlineI:
                    case OperandType.ShortInlineR:
                        result += Operand.ToString();
                        break;
                    case OperandType.InlineTok:
                        if (Operand is Type type)
                        {
                            result += type.FullName;
                        }
                        else
                        {
                            result += "not supported";
                        }
                            
                        break;

                    default: result += "not supported"; break;
                }
            }
            return result;
        }

        /// <summary>
        /// Add enough zeros to a number as to be represented on 4 characters
        /// </summary>
        /// <param name="offset">
        /// The number that must be represented on 4 characters
        /// </param>
        private string ExpandedOffset(long offset)
        {
            string result = offset.ToString();
            for (int i = 0; result.Length < 4; i++)
            {
                result = "0" + result;
            }
            return result;
        }
    }
}
