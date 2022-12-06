﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObjectToTest.Arguments;

namespace ObjectToTest
{
    /// <summary>
    /// The idea is to detect the objects that used more than once in different target object's arguments.
    /// It should allow to reuse the argument by injecting it into other object's constructors or property setters.
    /// </summary>
    public class ObjectSharedArguments : IArguments
    {
        private readonly object _object;
        private List<IArgument>? _sharedArguments;

        public ObjectSharedArguments(object @object)
        {
            _object = @object;
        }

        public IArgument? Argument(object? argument)
        {
            return ToList().FirstOrDefault(
                a => a.Equals(argument) && a.GetHashCode() == argument.GetHashCode()
            );
        }

        public List<IArgument> ToList()
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

        public override string ToString()
        {
            var arguments = new StringBuilder();
            foreach (var argument in ToList())
            {
                arguments.AppendLine($"{argument};");
            }

            return arguments.ToString();
        }
    }
}