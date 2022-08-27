using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ObjectToTest.Exceptions
{
    [Serializable]
    public class NoConstructorException : Exception
    {
        private const string _messageFormat = "Can not find a constructor for {0} object, not valid constructor available";

        public NoConstructorException(Type type)
            : base(string.Format(_messageFormat, type.Name))
        {
        }

        protected NoConstructorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
