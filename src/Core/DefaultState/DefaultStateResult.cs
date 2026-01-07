using System;

namespace ObjectToTest.Core.DefaultState
{
    internal readonly struct DefaultStateResult
    {
        public DefaultStateResult(bool isDefaultState, string constructorCall)
        {
            IsDefaultState = isDefaultState;
            ConstructorCall = constructorCall ?? throw new ArgumentNullException(nameof(constructorCall));
        }

        public bool IsDefaultState { get; }

        public string ConstructorCall { get; }
    }
}

