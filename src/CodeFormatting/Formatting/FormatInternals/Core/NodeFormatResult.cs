namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class NodeFormatResult
    {
        private readonly object _arg;
        private readonly NodeFormatResults _results;

        public NodeFormatResult(object arg, NodeFormatResults results)
        {
            _arg = arg;
            _results = results;
        }

        public bool AlreadyCalculated => _results.HasFor(_arg);

        public string String
        {
            get
            {
                return _results.GetFor(_arg);
            }

            set
            {
                _results.Add(_arg, value);
            }
        }
    }
}