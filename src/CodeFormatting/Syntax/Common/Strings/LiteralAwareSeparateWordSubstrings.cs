﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
{
    public class LiteralAwareSeparateWordSubstrings : IEnumerable<ISubstring>
    {
        private readonly string _source;
        private readonly string _pattern;

        public LiteralAwareSeparateWordSubstrings(string source, string pattern)
        {
            _source = source;
            _pattern = pattern;
        }

        public IEnumerator<ISubstring> GetEnumerator()
        {
            var matches = new LiteralAwareSubstring(_source, _pattern);

            var result = matches.ExcludeNotSeparateWords(_source).ToList();

            return result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}