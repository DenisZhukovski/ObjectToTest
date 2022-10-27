using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ObjectToTest.ILReader
{
    public static class Globals
    {
        public static OpCode[] MultiByteOpCodes;
        public static OpCode[] SingleByteOpCodes;
        public static Module[] Modules = null;

        static Globals()
        {
            LoadOpCodes();
        }
        
        public static void LoadOpCodes()
        {
            SingleByteOpCodes = new OpCode[0x100];
            MultiByteOpCodes = new OpCode[0x100];
            FieldInfo[] infoArray1 = typeof(OpCodes).GetFields();
            for (int num1 = 0; num1 < infoArray1.Length; num1++)
            {
                FieldInfo info1 = infoArray1[num1];
                if (info1.FieldType == typeof(OpCode))
                {
                    var code = (OpCode)info1.GetValue(null);
                    ushort num2 = (ushort)code.Value;
                    if (num2 < 0x100)
                    {
                        SingleByteOpCodes[num2] = code;
                    }
                    else
                    {
                        if ((num2 & 0xff00) != 0xfe00)
                        {
                            throw new InvalidOperationException($"Invalid OpCode '{code}'.");
                        }
                        MultiByteOpCodes[num2 & 0xff] = code;
                    }
                }
            }
        }

        /// <summary>
        /// Retrieve the friendly name of a type
        /// </summary>
        /// <param name="typeName">
        /// The complete name to the type
        /// </param>
        /// <returns>
        /// The simplified name of the type (i.e. "int" instead f System.Int32)
        /// </returns>
        public static string ProcessSpecialTypes(string typeName)
        {
            string result = typeName;
            switch (typeName)
            {
                case "System.string":
                case "System.String":
                case "String":
                    result = "string"; break;
                case "System.Int32":
                case "Int":
                case "Int32":
                    result = "int"; break;
            }
            return result;
        }
    }
}
