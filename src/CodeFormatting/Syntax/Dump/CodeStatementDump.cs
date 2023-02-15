using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Dump
{
    public class CodeStatementDump : ICodeStatement
    {
        private readonly ICodeStatement _statement;
        private readonly Tabs _tabs;

        public CodeStatementDump(ICodeStatement statement, Tabs tabs)
        {
            _statement = statement;
            _tabs = tabs;
        }

        public override string ToString()
        {
            switch (_statement)
            {
                case IAssignment assignment:
                    return new AssignmentDump(assignment, _tabs).ToString();
                case IInstantiationStatement instantiationStatement:
                    return new InstantiationStatementDump(instantiationStatement, _tabs).ToString();
                case ILiteral literal:
                    return $"{_tabs}Literal: {literal}";
                case ILambda lambda:
                    return $"{_tabs}Lambda: {lambda}";
                case IUnknownCodeStatement unknownCodeStatement:
                    return $"{_tabs}Cannot parse (look into CodeStatementDump for more details): {unknownCodeStatement}";
                default:
                    return $"{_tabs}Cannot dump (look into CodeStatementDump for more details): {_statement.GetType().Name}-{_statement}";
            }
        }
    }
}