using System;
using ObjectToTest.Exceptions;
using ObjectToTest.UnitTests.Models;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace ObjectToTest.UnitTests.Extensions
{
    public static class ExceptionExtensions
    {
        public static T Deserialize<T>(this T exception) where T : Exception
        {
            var stream = new FileStream($"{typeof(T).Name}.dat", FileMode.Create);
            var formatter = new SoapFormatter(null, new StreamingContext(StreamingContextStates.File));
            formatter.Serialize(stream, exception);
            stream.Position = 0;  // Rewind the stream and deserialize the exception.
            return (T)formatter.Deserialize(stream);
        }
    }
}

