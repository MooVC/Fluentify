namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ISymbol"/>.
/// </summary>
internal static partial class ISymbolExtensions
{
    /// <summary>
    /// Gets the specified attribute if annotated upon <paramref name="symbol"/>.
    /// </summary>
    /// <param name="symbol">The symbol for the type to be checked for the presence of an attribute.</param>
    /// <param name="name">The name of the attribute.</param>
    /// <returns>
    /// The <see cref="AttributeData"/> for the attribute if present on the <paramref name="symbol"/>, otherwise <see langword="null"/>.
    /// </returns>
    public static AttributeData? GetAttribute(this ISymbol symbol, string name)
    {
        string fullyQualifiedName = $"Fluentify.{name}Attribute";
        string globalQualifiedName = $"global::{fullyQualifiedName}";

        bool IsGlobal(INamedTypeSymbol attribute)
        {
            return attribute.ContainingNamespace.IsGlobalNamespace && attribute.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat) == name;
        }

        bool IsQualified(INamedTypeSymbol attribute)
        {
            string name = attribute.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

            return name == fullyQualifiedName || name == globalQualifiedName;
        }

        return symbol
            .GetAttributes()
            .Select(attribute => new
            {
                Class = attribute.AttributeClass,
                Data = attribute,
            })
            .Where(attribute => attribute.Class is not null && (IsQualified(attribute.Class) || IsGlobal(attribute.Class)))
            .Select(attribute => attribute.Data)
            .FirstOrDefault();
    }
}