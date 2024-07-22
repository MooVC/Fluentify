namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

using static Fluentify.IgnoreAttributeGenerator;

/// <summary>
/// Provides extensions relating to <see cref="ISymbol"/>.
/// </summary>
internal static partial class ISymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="symbol"/> provided is annotated with the Ignore attribute.
    /// </summary>
    /// <param name="symbol">The symbol for the record to be checked for the presence of the Ignore attribute.</param>
    /// <returns>True if the Ignore attribute is present on the <paramref name="symbol"/>, otherwise False.</returns>
    public static bool HasIgnore(this ISymbol symbol)
    {
        return symbol.HasAttribute(Name);
    }
}