﻿using System;
using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Args
{
    public class Arguments : IArguments
    {
        private readonly Lazy<PossibleArguments> _possibleArguments = new(() => new PossibleArguments());

        private readonly string _source;

        public Arguments(string source)
        {
            _source = source;
        }

        public override string ToString()
        {
            return _source;
        }

        public IEnumerator<IArgument> GetEnumerator()
        {
            foreach (var characterSeparatedSubstring in new CharacterSeparatedSubstrings(_source, ',', notAnalyzeIn: new LiteralsAndClosuresSubstrings(_source)))
            {
                yield return _possibleArguments.Value.BestMatch(characterSeparatedSubstring.ToString());
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}