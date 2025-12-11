namespace Fluentify.Semantics;

using System;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ITypeSymbol"/>.
/// </summary>
internal static partial class ITypeSymbolExtensions
{
    /// <summary>
    /// Determines if the <paramref name="type"/> is a value type.
    /// </summary>
    /// <param name="type">The <see cref="ITypeSymbol"/> for which the determination is to be carried out.</param>
    /// <returns>True if the <paramref name="type"/> is a value type, otherwise False.</returns>
    public static bool IsValueType(this ITypeSymbol type)
    {
        if (type.IsValueType)
        {
            return true;
        }

        if (type.NullableAnnotation == NullableAnnotation.Annotated)
        {
            type = type.OriginalDefinition;
        }

        if (type is INamedTypeSymbol { SpecialType: SpecialType.System_Nullable_T } nullable)
        {
            type = nullable.TypeArguments[0];
        }

        return type.IsValueType;
    }
}