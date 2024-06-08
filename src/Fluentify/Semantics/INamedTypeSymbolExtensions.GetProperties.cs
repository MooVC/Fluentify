﻿namespace Fluentify.Semantics;

using System.Threading;
using Fluentify.Model;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Returns a collection of <see cref="Property"/> for each property explicitly declared within <paramref name="symbol"/>.
    /// </summary>
    /// <param name="symbol">The subject from which the properties are identified.</param>
    /// <param name="compilation">The <see cref="Compilation"/> used to determine if the type associated with a property allows for internal construction.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> that can be used to cancel the operation.</param>
    /// <returns>The collection of <see cref="Property"/> for each property explicitly declared within <paramref name="symbol"/>.</returns>
    public static IReadOnlyList<Property> GetProperties(this INamedTypeSymbol symbol, Compilation compilation, CancellationToken cancellationToken)
    {
        return symbol
            .GetMembers()
            .OfType<IPropertySymbol>()
            .Where(IsMatch)
            .Select(property => property.Map(compilation, cancellationToken))
            .ToArray();
    }

    private static bool IsMatch(this IPropertySymbol property)
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
            && IsInitializable(property)
            && !property.HasIgnore();
    }

    private static Property Map(this IPropertySymbol property, Compilation compilation, CancellationToken cancellationToken)
    {
        static bool IsNullable(IPropertySymbol property)
        {
            return property.Type.NullableAnnotation == NullableAnnotation.Annotated
                 || property.Type.SpecialType == SpecialType.System_Nullable_T
                 || property.Type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T;
        }

        string? descriptor = property.GetDescriptor();

        descriptor ??= $"With{property.Name}";

        return new Property
        {
            Accessibility = property.DeclaredAccessibility,
            Descriptor = descriptor,
            IsBuildable = property.IsBuildable(compilation, cancellationToken),
            IsNullable = IsNullable(property),
            Name = property.Name,
            Type = property.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
        };
    }
}