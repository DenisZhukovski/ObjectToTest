﻿namespace ObjectToTest.CodeFormatting.Syntax
{
    /// <summary>
    /// [Left] = [Right];
    /// var i = 5;
    /// int a = 3;
    /// a = "234";
    /// var some = new Foo(bar) {A = 5};
    /// </summary>
    public interface IAssignmentPart : ICodeStatement
    {
        ILeftAssignmentPart Left { get; }

        IRightAssignmentPart Right { get; }
    }
}