namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ITypeSymbol"/>.
/// </summary>
internal static partial class ITypeSymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="symbol"/> provided is the attribute specified as <paramref name="name"/>.
    /// </summary>
    /// <param name="symbol">The property to be checked for the presence of an attribute.</param>
    /// <param name="name">The name of the attribute.</param>
    /// <returns>True if the <paramref name="symbol"/> is the expected attribute, otherwise False.</returns>
    public static bool IsAttribute(this ITypeSymbol symbol, string name)
    {
        string fullyQualifiedName = $"Fluentify.{name}Attribute";
        string globalQualifiedName = $"global::{fullyQualifiedName}";

        bool IsGlobal()
        {
            return symbol.ContainingNamespace.IsGlobalNamespace && symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat) == name;
        }

        bool IsQualified()
        {
            string name = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

            return name == fullyQualifiedName || name == globalQualifiedName;
        }

        return IsQualified() || IsGlobal();
    }
}