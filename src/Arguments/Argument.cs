using System;
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

        public override string ToString()
        {
            return Constructor.ToString();
        }
    }
}

