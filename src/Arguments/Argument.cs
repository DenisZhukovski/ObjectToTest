using System;
using System.Collections.Generic;
using ObjectToTest.Constructors;

namespace ObjectToTest.Arguments
{
    public class Argument : IArgument
    {
        public Argument(string name, object @object)
            : this(name, @object.GetType(), @object, @object.Constructor(new MockArguments()))
        {
        }
        
        public Argument(string name, object @object, IConstructor constructor)
        : this(name, @object.GetType(), @object, constructor)
        {
        }
        
        public Argument(string name, Type type, object? @object, IConstructor constructor)
        {
            Name = name;
            Type = type?.TypeName() ?? "null";
            Object = @object;
            Constructor = constructor;
        }

        public string Name { get; }
        
        public string Type { get; }
        
        public object? Object { get; }

        public IConstructor Constructor { get; }
        
        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj)
                || TheSameArgument(obj)
                || Constructor.Equals(obj);
        }

        public override int GetHashCode()
        {
            if (Object != null)
            {
                return Object.GetHashCode();
            }
            
            int hashCode = 726871525;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<IConstructor>.Default.GetHashCode(Constructor);
            return hashCode;
        }

        public override string ToString()
        {
            return Constructor.ToString();
        }

        private bool TheSameArgument(object? obj)
        {
            return obj is IArgument argument
                && Name == argument.Name
                && Constructor.Equals(argument.Constructor);
        }
    }
}

