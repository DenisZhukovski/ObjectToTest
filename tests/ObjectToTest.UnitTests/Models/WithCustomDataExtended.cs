using System;
namespace ObjectToTest.UnitTests.Models
{
    public class WithCustomDataExtended
    {
        private readonly WithCustomData _data;
        private readonly WithCustomHashCode _withCustomHashCode;

        public WithCustomDataExtended(WithCustomData data, WithCustomHashCode withCustomHashCode)
        {
            this._data = data;
            this._withCustomHashCode = withCustomHashCode;
        }

        public string Foo()
        {
            return _data + _withCustomHashCode.ToString();
        }
    }
}
