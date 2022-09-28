﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using ObjectToTest.Arguments;

namespace ObjectToTest
{
    /// <summary>
    /// The idea is to detect the set of objects that used more that one time in different target object arguments.
    /// It should allow to reuse the argument injecting it into other object's constructors or property setters.
    /// </summary>
    public class ObjectSharedArguments : IArguments
    {
        private readonly object _object;
        private List<IArgument>? _sharedArguments;

        public ObjectSharedArguments(object @object)
        {
            _object = @object;
        }

        private List<IArgument> SharedArguments
        {
            get
            {
                if (_sharedArguments == null)
                {
                    // should be created first to avoid infinite recursion cycle
                    _sharedArguments = new List<IArgument>();
                    _sharedArguments.AddRange(
                        _object
                            .SharedObjects()
                            .Select(o => o.AsSharedArgument(this))
                    );
                }
                return _sharedArguments;
            }
        }

        public IArgument? Argument(object argument)
        {
            return SharedArguments.FirstOrDefault(a => a.Equals(argument));
        }

        public override string ToString()
        {
            var arguments = new StringBuilder();
            foreach (var argument in SharedArguments)
            {
                arguments.AppendLine(argument.ToString() + ";");
            }
            return arguments.ToString();
        }
    }
}
