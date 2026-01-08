namespace Fluentify.Semantics;

using System;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ITypeSymbol"/>.
/// </summary>
internal static partial class ITypeSymbolExtensions
{
    private const int ExpectedArgumentsForNullableType = 1;

    /// <summary>
    /// Determines if the <paramref name="type"/> is a value type.
    /// </summary>
    /// <param name="type">The <see cref="ITypeSymbol"/> for which the determination is to be carried out.</param>
    /// <returns>True if the <paramref name="type"/> is a value type, otherwise False.</returns>
    public static bool IsValueType(this ITypeSymbol type)
    {
        if (type is INamedTypeSymbol symbol
            && (symbol.SpecialType == SpecialType.System_Nullable_T || type.NullableAnnotation == NullableAnnotation.Annotated))
        {
            type = symbol.TypeArguments.Length == ExpectedArgumentsForNullableType
                ? symbol.TypeArguments[0]
                : symbol.OriginalDefinition;
        }

        return type.IsValueType;
    }
}