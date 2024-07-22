namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ISymbol"/>.
/// </summary>
internal static partial class ISymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="symbol"/> provided is annotated with an attribute.
    /// </summary>
    /// <param name="symbol">The symbol for the type to be checked for the presence of an attribute.</param>
    /// <param name="name">The name of the attribute.</param>
    /// <returns>True if the attribute is present on the <paramref name="symbol"/>, otherwise False.</returns>
    public static bool HasAttribute(this ISymbol symbol, string name)
    {
        return symbol.GetAttribute(name) is not null;
    }
}