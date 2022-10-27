using System;
using System.Reflection.Emit;

namespace ObjectToTest.ILReader
{
    public static class ILExtensions
    {
        /// <summary>
        ///  Gets the operation code of the current instruction
        /// </summary>
        public static OpCode InstructionOpCode(this byte[] il, ref int position)
        {
            OpCode code;
            ushort value = il[position++];
            if (value != 0xfe)
            {
                code = Globals.SingleByteOpCodes[value];
            }
            else
            {
                value = il[position++];
                code = Globals.MultiByteOpCodes[value];
            }

            return code;
        }
        
        public static int ReadInt16(this byte[] bodyIL, ref int position)
        {
            return bodyIL[position++] | (bodyIL[position++] << 8);
        }
        
        public static ushort ReadUInt16(this byte[] bodyIL, ref int position)
        {
            return (ushort)(bodyIL[position++] | (bodyIL[position++] << 8));
        }
        
        public static int ReadInt32(this byte[] bodyIL, ref int position)
        {
            return bodyIL[position++] | (bodyIL[position++] << 8) | (bodyIL[position++] << 0x10) | (bodyIL[position++] << 0x18);
        }
        
        public static ulong ReadInt64(this byte[] bodyIL, ref int position)
        {
            return (ulong)(bodyIL[position++] | (bodyIL[position++] << 8) | (bodyIL[position++] << 0x10) | (bodyIL[position++] << 0x18) | (bodyIL[position++] << 0x20) | (bodyIL[position++] << 0x28) | (bodyIL[position++] << 0x30) | (bodyIL[position++] << 0x38));
        }
        
        public static double ReadDouble(this byte[] bodyIL, ref int position)
        {
            return bodyIL[position++] | (bodyIL[position++] << 8) | (bodyIL[position++] << 0x10) | (bodyIL[position++] << 0x18) | (bodyIL[position++] << 0x20) | (bodyIL[position++] << 0x28) | (bodyIL[position++] << 0x30) | (bodyIL[position++] << 0x38);
        }
        
        public static sbyte ReadSByte(this byte[] bodyIL, ref int position)
        {
            return (sbyte)bodyIL[position++];
        }
        
        public static byte ReadByte(this byte[] bodyIL, ref int position)
        {
            return bodyIL[position++];
        }
        
        public static Single ReadSingle(this byte[] bodyIL, ref int position)
        {
            return bodyIL[position++] | (bodyIL[position++] << 8) | (bodyIL[position++] << 0x10) | (bodyIL[position++] << 0x18);
        }
    }
}