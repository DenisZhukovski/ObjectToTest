﻿namespace ObjectToTest.Constructors
{
    internal class DefaultConstructor
    {
        private readonly object _object;

        public DefaultConstructor(object @object)
        {
            _object = @object;
        }

        public override string ToString()
        {
            return $"new {_object.GetType().Name}()";
        }
    }
}
