﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ObjectToTest.Arguments;
using ObjectToTest.Exceptions;

namespace ObjectToTest.Constructors
{
    [SuppressMessage("Blocker Code Smell", "S3877:Exceptions should not be thrown from unexpected methods")]
    public class InvalidConstructor : IConstructor
    {
        private readonly object _object;
        private readonly IArguments _sharedArguments;

        public InvalidConstructor(object @object)
            : this(@object, new ObjectSharedArguments(@object, false))
        {
        }
        
        public InvalidConstructor(object @object, IArguments sharedArguments)
        {
            _object = @object;
            _sharedArguments = sharedArguments;
        }

        public bool IsValid => false;

        public IList<IArgument> Arguments 
        {
            get
            {
                foreach (var ctor in _object.Constructors())
                {
                    var arguments = new List<IArgument>();
                    foreach (var parameter in ctor.GetParameters())
                    {
                        arguments.Add(new ParameterInfoAsArgument(parameter, _object, _sharedArguments));
                    }

                    if (arguments.Any(a => !a.Constructor.IsValid))
                    {
                        return arguments;
                    }
                }

                return new List<IArgument>();
            }    
        }

        public object? Object => _object;

        public override bool Equals(object? obj)
        {
            return (obj is IConstructor constructor && constructor.Equals(_object)) || _object.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _object.GetHashCode();
        }

        public override string ToString()
        {
            throw new NoConstructorException(_object);
        }
    }
}

