namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ITypeSymbol"/>.
/// </summary>
internal static partial class ITypeSymbolExtensions
{
    /// <summary>
    /// Determines if the <paramref name="type"/> is a non-nullable value type.
    /// </summary>
    /// <param name="type">The type to be checked.</param>
    /// <returns>
    /// True when <paramref name="type"/> is a non-nullable value type, otherwise False.
    /// </returns>
    public static bool IsNonNullableValueType(this ITypeSymbol type)
    {
        if (type.IsValueType())
        {
            return true;
        }

        return type.SpecialType is SpecialType.System_Boolean
            or SpecialType.System_Byte
            or SpecialType.System_Char
            or SpecialType.System_DateTime
            or SpecialType.System_Decimal
            or SpecialType.System_Double
            or SpecialType.System_Int16
            or SpecialType.System_Int32
            or SpecialType.System_Int64
            or SpecialType.System_SByte
            or SpecialType.System_Single
            or SpecialType.System_UInt16
            or SpecialType.System_UInt32
            or SpecialType.System_UInt64;
    }
}