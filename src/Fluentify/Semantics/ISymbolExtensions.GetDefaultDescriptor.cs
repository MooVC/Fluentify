namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ISymbol"/>.
/// </summary>
internal static partial class ISymbolExtensions
{
    /// <summary>
    /// Gets the default descriptor associated with the <paramref name="symbol"/>.
    /// </summary>
    /// <param name="symbol">The symbol for which the descriptor is to be generated.</param>
    /// <returns>
    /// The default descriptor for the <paramref name="symbol"/>.
    /// </returns>
    public static string GetDefaultDescriptor(this ISymbol symbol)
    {
        return IsBoolean(symbol)
            ? symbol.Name
            : $"With{symbol.Name}";
    }

    private static bool IsBoolean(ISymbol symbol)
    {
        ITypeSymbol? type = symbol switch
        {
            IPropertySymbol property => property.Type,
            IParameterSymbol parameter => parameter.Type,
            _ => default,
        };

        return type is not null && (IsBoolean(type) || IsNullableBoolean(type));
    }

    private static bool IsBoolean(ITypeSymbol type)
    {
        return type.SpecialType == SpecialType.System_Boolean;
    }

    private static bool IsNullableBoolean(ITypeSymbol type)
    {
        if (type is INamedTypeSymbol symbol
         && symbol.IsGenericType
         && symbol.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
        {
            return IsBoolean(symbol.TypeArguments[0]);
        }

        return false;
    }
}