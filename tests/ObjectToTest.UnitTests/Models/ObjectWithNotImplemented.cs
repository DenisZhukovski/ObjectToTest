using System;

namespace ObjectToTest.UnitTests.Models
{
    public class ObjectWithNotImplemented
    {
        public int Size => throw new NotImplementedException();
    }
}