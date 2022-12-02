﻿namespace ObjectToTest.CodeFormatting.Syntax.Contracts
{
    /// <summary>
    /// new Foo(), new int[] {}, dictionary or list
    /// </summary>
    public interface IInstantiationStatement : IRightAssignmentPart, ICodeStatement, IArgument
    {
        /*
         * @todo #91 60m/DEV Define necessary properties
         * Most likely: new [TypeDefinition]([Arguments]) {[InlinePropertiesAssignment]}
         */

        ITypeDefinition Type { get; }

        IArguments Arguments { get; }

        IPropertyAssignments InlinePropertiesAssignment { get; }
    }
}