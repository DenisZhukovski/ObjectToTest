using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Dump
{
    public class ArgumentDump
    {
        private readonly IArgument _statement;
        private readonly Tabs _tabs;

        public ArgumentDump(IArgument statement, Tabs tab)
        {
            _statement = statement;
            _tabs = tab;
        }

        public override string ToString()
        {
            switch (_statement)
            {
                case IInstantiationStatement instantiationStatement:
                    return new InstantiationStatementDump(instantiationStatement, _tabs).ToString();
                case IUnknownArgument unknownArgument:
                    return $"{_tabs}Cannot parse (look into ArgumentDump for more details): {unknownArgument}";
                case ILiteral literal:
                    return $"{_tabs}Literal: {literal}";
                case ILambda lambda:
                    return $"{_tabs}Lambda: {lambda}";
                default:
                    return $"{_tabs}Cannot dump (look into ArgumentDump for more details): {_statement.GetType().Name}-{_statement}";
            }
        }
    }
}