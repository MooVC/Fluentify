namespace Fluentify.Semantics;

using System;
using System.Linq;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    private const string AllowNullAttribute = "AllowNull";
    private const string CodeAnalysisNamespace = "System.Diagnostics.CodeAnalysis";
    private const string DisallowNullAttribute = "DisallowNull";
    private const string MaybeNullAttribute = "MaybeNull";

    /// <summary>
    /// Determines whether an assigned value should throw when it is null based on the nullable annotation and nullability attributes.
    /// </summary>
    /// <param name="property">The property for which the null assignment behavior is to be determined.</param>
    /// <returns>
    /// True when an assigned null value should throw, otherwise False.
    /// </returns>
    public static bool ThrowsWhenValueIsNull(this IPropertySymbol property)
    {
        bool isNullable = property.Type.IsNullable();

        if (isNullable)
        {
            return property.HasCodeAnalysisAttribute(DisallowNullAttribute);
        }

        if (property.Type.IsNonNullableValueType())
        {
            return false;
        }

        return !property.HasCodeAnalysisAttribute(AllowNullAttribute)
            && !property.HasCodeAnalysisAttribute(MaybeNullAttribute);
    }

    private static bool HasCodeAnalysisAttribute(this IPropertySymbol property, string name)
    {
        if (((ISymbol)property).HasCodeAnalysisAttribute(name))
        {
            return true;
        }

        IParameterSymbol? parameter = property.GetParameter();

        return parameter is not null && parameter.HasCodeAnalysisAttribute(name);
    }

    private static bool HasCodeAnalysisAttribute(this ISymbol symbol, string name)
    {
        return symbol
            .GetAttributes()
            .Any(attribute => attribute.AttributeClass.IsCodeAnalysisAttribute(name));
    }

    private static bool IsCodeAnalysisAttribute(this INamedTypeSymbol? symbol, string name)
    {
        return symbol is not null
            && string.Equals(symbol.Name, string.Concat(name, "Attribute"), StringComparison.Ordinal)
            && string.Equals(symbol.ContainingNamespace.ToDisplayString(), CodeAnalysisNamespace, StringComparison.Ordinal);
    }
}