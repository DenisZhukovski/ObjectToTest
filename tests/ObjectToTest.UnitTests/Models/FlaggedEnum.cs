using System;
namespace ObjectToTest.UnitTests.Models
{
    [Flags]
    public enum FlaggedEnum
    {
        None = 0,
        Basic = 2,
        Advanced = 4,
        Expert = 8
    }
}
