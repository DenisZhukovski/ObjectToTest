﻿using System;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Parse;
using ObjectToTest.CodeFormatting.Syntax.Statements.Instantiation;
using ObjectToTest.CodeFormatting.Syntax.Statements.Unknown;

namespace ObjectToTest.CodeFormatting.Syntax.Statements
{
    public class PossibleCodeStatements : IPossibleItems<ICodeStatement>
    {
        /*
         * @todo #97 60m/DEV Implement different statements and register them here.
         */
        private readonly Lazy<PossibleItems<ICodeStatement>> _possibleStatements = new(
            () =>
                new PossibleItems<ICodeStatement>(
                    InstantiationStatement.Parse,
                    codeStatement => new ParseSuccessful<ICodeStatement>(new UnknownCodeStatement(codeStatement))
                )
        );

        public ICodeStatement BestMatch(string value)
        {
            return _possibleStatements.Value.BestMatch(value);
        }
    }
}