namespace Fluentify;

using Microsoft.CodeAnalysis;

internal static partial class TypeExtensions
{
    public static IPropertySymbol GetProperty(this Definition type, string name)
    {
        return type
            .Symbol
            .GetMembers()
            .OfType<IPropertySymbol>()
            .Single(property => property.Name == name);
    }
}