namespace Fluentify.Semantics;

using System;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ITypeSymbol"/>.
/// </summary>
internal static partial class ITypeSymbolExtensions
{
    private const string SystemNamespace = "global::System";

    /// <summary>
    /// Determines if the <paramref name="type"/> is defined within the base class library.
    /// </summary>
    /// <param name="type">The <see cref="ITypeSymbol"/> for which the determination is to be carried out.</param>
    /// <returns>True if the <paramref name="type"/> is defined within the base class library, otherwise False.</returns>
    public static bool IsFrameworkType(this ITypeSymbol type)
    {
        if (type.SpecialType != SpecialType.None)
        {
            return true;
        }

        string? @namespace = type.ContainingNamespace?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        if (@namespace is null)
        {
            return false;
        }

        return @namespace.StartsWith(SystemNamespace, StringComparison.Ordinal);
    }
}
