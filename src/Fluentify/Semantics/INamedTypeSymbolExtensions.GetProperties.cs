namespace Fluentify.Semantics;

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
            .Where(property => property.IsMutable())
            .Select(property => property.Map(compilation, cancellationToken))
            .ToArray();
    }

    private static Property Map(this IPropertySymbol property, Compilation compilation, CancellationToken cancellationToken)
    {
        string? descriptor = default;
        Kind kind = Kind.Unspecified;

        if (!property.HasIgnore())
        {
            descriptor = property.GetDescriptor();
            kind = property.GetKind(compilation, cancellationToken);
        }

        descriptor ??= kind.Type.IsBoolean
            ? property.Name
            : $"With{property.Name}";

        return new Property
        {
            Accessibility = property.DeclaredAccessibility,
            Descriptor = descriptor,
            Name = property.Name,
            Kind = kind,
        };
    }
}