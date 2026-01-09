namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

using static Fluentify.SkipAutoInstantiationAttributeGenerator;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="property"/> provided is annotated with the SkipAutoInstantiation attribute.
    /// </summary>
    /// <param name="property">The property to be checked for the presence of the SkipAutoInstantiation attribute.</param>
    /// <returns>True if the SkipAutoInstantiation attribute is present on the <paramref name="property"/>, otherwise False.</returns>
    public static bool HasSkipAutoInstantiation(this IPropertySymbol property)
    {
        if (property.HasAttribute(Name))
        {
            return true;
        }

        if (property.Type is null)
        {
            return false;
        }

        if (property.Type.HasAttribute(Name))
        {
            return true;
        }

        if (property.Type is IArrayTypeSymbol arrayType)
        {
            return arrayType.ElementType.HasAttribute(Name);
        }

        if (property.Type is INamedTypeSymbol namedType)
        {
            foreach (ITypeSymbol typeArgument in namedType.TypeArguments)
            {
                if (typeArgument.HasAttribute(Name))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
