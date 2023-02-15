using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Statements.Assignment;

namespace ObjectToTest.CodeFormatting.Syntax.Dump
{
    public class RightAssignmentPartDump
    {
        private readonly IRightAssignmentPart _statement;
        private readonly Tabs _tabs;

        public RightAssignmentPartDump(IRightAssignmentPart statement, Tabs tabs)
        {
            _statement = statement;
            _tabs = tabs;
        }

        public override string ToString()
        {
            switch (_statement)
            {
                case IInstantiationStatement instantiationStatement:
                    return new InstantiationStatementDump(instantiationStatement, _tabs).ToString();
                case ILiteral literal:
                    return $"{_tabs}Literal: {literal}";
                case ILambda lambda:
                    return $"{_tabs}Lambda: {lambda}";
                case RawAssignmentPart rawAssignmentPart:
                    return rawAssignmentPart.ToString();
                default:
                    return $"Cannot dump (look into RightAssignmentPartDump for more details): {_statement.GetType().Name}-{_statement}";
            }
        }
    }
}