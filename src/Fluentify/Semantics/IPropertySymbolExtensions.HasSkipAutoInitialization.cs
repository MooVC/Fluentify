namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

using static Fluentify.SkipAutoInitializationAttributeGenerator;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="property"/> provided is annotated with the SkipAutoInitialization attribute.
    /// </summary>
    /// <param name="property">The property to be checked for the presence of the SkipAutoInitialization attribute.</param>
    /// <returns>True if the SkipAutoInitialization attribute is present on the <paramref name="property"/>, otherwise False.</returns>
    public static bool HasSkipAutoInitialization(this IPropertySymbol property)
    {
        return property.HasAttribute(Name) || (property.Type is not null && property.Type.HasAttribute(Name));
    }
}