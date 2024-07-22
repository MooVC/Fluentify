namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

using static Fluentify.IgnoreAttributeGenerator;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="property"/> provided is annotated with the Ignore attribute.
    /// </summary>
    /// <param name="property">The symbol for the record to be checked for the presence of the Ignore attribute.</param>
    /// <returns>True if the Ignore attribute is present on the <paramref name="property"/>, otherwise False.</returns>
    public static bool HasIgnore(this IPropertySymbol property)
    {
        return property.HasAttribute(Name);
    }
}