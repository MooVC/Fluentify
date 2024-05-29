namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="property"/> provided is annotated with an attribute.
    /// </summary>
    /// <param name="property">The property to be checked for the presence of an attribute.</param>
    /// <param name="name">The name of the attribute.</param>
    /// <returns>True if the attribute is present on the <paramref name="property"/>, otherwise False.</returns>
    public static bool HasAttribute(this IPropertySymbol property, string name)
    {
        if (ISymbolExtensions.HasAttribute(property, name))
        {
            return true;
        }

        IParameterSymbol? parameter = property.GetParameter();

        if (parameter is not null)
        {
            return parameter.HasAttribute(name);
        }

        return false;
    }
}