namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

using static Fluentify.HideAttributeGenerator;

/// <summary>
/// Provides extensions relating to <see cref="ISymbol"/>.
/// </summary>
internal static partial class ISymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="symbol"/> provided is annotated with the Hide attribute.
    /// </summary>
    /// <param name="symbol">The symbol for the record to be checked for the presence of the Hide attribute.</param>
    /// <returns>True if the Hide attribute is present on the <paramref name="symbol"/>, otherwise False.</returns>
    public static bool HasHide(this ISymbol symbol)
    {
        return symbol.HasAttribute(Name);
    }
}