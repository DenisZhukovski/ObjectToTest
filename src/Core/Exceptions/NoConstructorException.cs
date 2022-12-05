using System;
using System.Runtime.Serialization;

namespace ObjectToTest.Exceptions
{
    [Serializable]
    public class NoConstructorException : Exception
    {
        private const string _messageFormat = "Can not find a constructor for {0} object, valid constructor is not available.";

        public NoConstructorException(object @object)
            : base(string.Format(_messageFormat, @object.GetType().Name))
        {
            Object = @object;
        }

        protected NoConstructorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public object? Object { get; }
    }
}
