namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

internal static partial class ITypeSymbolExtensions
{
    public static bool IsNullable(this ITypeSymbol type)
    {
        return type.NullableAnnotation == NullableAnnotation.Annotated
             || type.SpecialType == SpecialType.System_Nullable_T
             || type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T;
    }
}