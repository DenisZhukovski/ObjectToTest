﻿using System;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Parse;
using ObjectToTest.CodeFormatting.Syntax.Statements.Args;
using ObjectToTest.CodeFormatting.Syntax.Statements.Instantiation;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Assignment
{
    public class PossibleAssignments : IPossibleItems<ICodeStatement>
    {
        private readonly Lazy<PossibleItems<ICodeStatement>> _possibleStatements = new(
            () =>
                new PossibleItems<ICodeStatement>(
                    DictionaryInlineAssignment.Parse,
                    Assignment.Parse,
                    LambdaExpression.Parse,
                    InstantiationStatement.Parse,
                    Literal.Parse,
                    codeStatement => new ParseSuccessful<ICodeStatement>(new RawAssignment(codeStatement))
                )
        );

        public ICodeStatement BestMatch(string value)
        {
            return _possibleStatements.Value.BestMatch(value);
        }
    }
}