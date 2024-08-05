namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

internal static partial class DefinitionExtensions
{
    public static IPropertySymbol GetProperty(this Definition definition, string name)
    {
        return definition
            .Symbol
            .GetMembers()
            .OfType<IPropertySymbol>()
            .Single(property => property.Name == name);
    }
}