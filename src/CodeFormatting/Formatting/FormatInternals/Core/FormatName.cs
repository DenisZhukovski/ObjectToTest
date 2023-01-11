namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class FormatName
    {
        private readonly INodeFormat _format;

        public FormatName(INodeFormat format)
        {
            _format = format;
        }

        public override string ToString()
        {
            if (_format is NodeFormat formatAndCondition)
            {
                if (formatAndCondition.Name != null)
                {
                    return formatAndCondition.Name;
                }
            }

            return _format.GetType().Name;
        }
    }
}