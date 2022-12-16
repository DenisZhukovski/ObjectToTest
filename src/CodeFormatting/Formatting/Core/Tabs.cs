namespace ObjectToTest.CodeFormatting.Formatting
{
    public class Tabs
    {
        private readonly int _initial;

        public Tabs(int initial)
        {
            _initial = initial;
        }

        public string Spaces => new(' ', _initial * 4);

        public Tabs Tab() => new Tabs(_initial + 1);

        public Tabs Including(Tabs parentTabs)
        {
            return new Tabs(_initial + parentTabs._initial);
        }

        public override string ToString()
        {
            return Spaces;
        }
    }
}