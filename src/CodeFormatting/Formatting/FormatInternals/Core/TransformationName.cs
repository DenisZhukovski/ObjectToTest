namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class TransformationName
    {
        private readonly INodeTransformation _format;

        public TransformationName(INodeTransformation format)
        {
            _format = format;
        }

        public override string ToString()
        {
            if (_format is NodeTransformation transformationAndCondition && transformationAndCondition.Name != null)
            {
                return transformationAndCondition.Name;
            }

            return _format.GetType().Name;
        }
    }
}