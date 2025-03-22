using System;

namespace ObjectToTest.Core.Extensions
{
    public static class ExceptionExtensions
    {
        public static bool Contains<T>(this Exception ex) where T : Exception
        {
            var innerException = ex;
            while (innerException != null)
            {
                if (innerException is T)
                {
                    return true;
                }

                innerException = innerException.InnerException;
            }

            return false;
        }
    }
}