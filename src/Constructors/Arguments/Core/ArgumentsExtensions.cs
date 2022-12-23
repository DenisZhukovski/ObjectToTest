namespace ObjectToTest.Arguments
{
    public static class ArgumentsExtensions
    {
        public static bool Contains(this IArguments arguments, object @object)
        {
            return arguments.Argument(@object) != null;
        }
    }
}