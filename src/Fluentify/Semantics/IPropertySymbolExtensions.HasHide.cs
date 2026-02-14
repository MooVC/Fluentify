namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

using static Fluentify.HideAttributeGenerator;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="property"/> provided is annotated with the Hide attribute.
    /// </summary>
    /// <param name="property">The symbol for the record to be checked for the presence of the Hide attribute.</param>
    /// <returns>True if the Hide attribute is present on the <paramref name="property"/>, otherwise False.</returns>
    public static bool HasHide(this IPropertySymbol property)
    {
        return property.HasAttribute(Name);
    }
}