namespace Fluentify.Semantics;

using System;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ITypeSymbol"/>.
/// </summary>
internal static partial class ITypeSymbolExtensions
{
    private static readonly HashSet<SpecialType> _exclusions =
    [
        SpecialType.System_Boolean,
        SpecialType.System_Byte,
        SpecialType.System_Char,
        SpecialType.System_Double,
        SpecialType.System_Decimal,
        SpecialType.System_Enum,
        SpecialType.System_Int16,
        SpecialType.System_Int32,
        SpecialType.System_Int64,
        SpecialType.System_SByte,
        SpecialType.System_Single,
        SpecialType.System_String,
        SpecialType.System_UInt16,
        SpecialType.System_UInt32,
        SpecialType.System_UInt64,
    ];

    /// <summary>
    /// Determines if the <paramref name="type"/> is defined within the base class library.
    /// </summary>
    /// <param name="type">The <see cref="ITypeSymbol"/> for which the determination is to be carried out.</param>
    /// <returns>True if the <paramref name="type"/> is defined within the base class library, otherwise False.</returns>
    public static bool IsFrameworkType(this ITypeSymbol type)
    {
        if (type is INamedTypeSymbol { SpecialType: SpecialType.System_Nullable_T } nullable)
        {
            type = nullable.TypeArguments[0];
        }
        else if (type.NullableAnnotation == NullableAnnotation.Annotated)
        {
            type = type.WithNullableAnnotation(NullableAnnotation.NotAnnotated);
        }

        return _exclusions.Contains(type.SpecialType);
    }
}