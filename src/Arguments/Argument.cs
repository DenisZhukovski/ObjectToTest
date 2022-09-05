using System;
using System.Collections.Generic;
using ObjectToTest.Constructors;

namespace ObjectToTest.Arguments
{
    public class Argument : IArgument
    {
        public Argument(string name, IConstructor constructor)
        {
            Name = name;
            Constructor = constructor;
        }

        public string Name { get; }

        public IConstructor Constructor { get; }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj)
                || TheSameArgument(obj)
                || Constructor.Equals(obj);
        }

        public override int GetHashCode()
        {
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

