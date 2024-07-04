namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    /// <summary>
    /// Checks whether or not the specified <paramref name="property"/> is deemed mutable in the context of an Internal extension method.
    /// </summary>
    /// <param name="property">The property to check.</param>
    /// <returns>True if the property is deemed mutable, otherwise False.</returns>
    public static bool IsMutable(this IPropertySymbol property)
    {
        static bool IsAccessible(Accessibility accessibility)
        {
            return accessibility == Accessibility.Public || accessibility == Accessibility.Internal;
        }

        static bool IsInitializable(IPropertySymbol property)
        {
            return property.SetMethod is not null && IsAccessible(property.SetMethod.DeclaredAccessibility);
        }

        static bool IsExplicitlyDeclaredInstanceProperty(IPropertySymbol property)
        {
            return !(property.IsStatic || property.IsImplicitlyDeclared);
        }

        return IsExplicitlyDeclaredInstanceProperty(property)
            && IsAccessible(property.DeclaredAccessibility)
            && IsInitializable(property);
    }
}