namespace Fluentify.Semantics;

using Fluentify.Model;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Returns a collection of <see cref="Property"/> for each property available within <paramref name="symbol"/>.
    /// </summary>
    /// <param name="symbol">The subject from which the properties are identified.</param>
    /// <param name="compilation">The <see cref="Compilation"/> used to determine if the type associated with a property allows for internal construction.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> that can be used to cancel the operation.</param>
    /// <returns>The collection of <see cref="Property"/> for each property available within <paramref name="symbol"/>.</returns>
    public static IReadOnlyList<Property> GetAllProperties(this INamedTypeSymbol symbol, Compilation compilation, CancellationToken cancellationToken)
    {
        INamedTypeSymbol? current = symbol;
        var mutable = new List<Property>();

        do
        {
            IEnumerable<IPropertySymbol> properties = current
                .GetMembers()
                .OfType<IPropertySymbol>()
                .Where(property => property.IsMutable());

            foreach (IPropertySymbol property in properties)
            {
                mutable.Add(property.Map(compilation, cancellationToken));
            }

            current = current.BaseType;
        }
        while (current is not null && !cancellationToken.IsCancellationRequested);

        return mutable;
    }

    private static Property Map(this IPropertySymbol property, Compilation compilation, CancellationToken cancellationToken)
    {
        Kind kind = property.GetKind(compilation, cancellationToken);
        bool isIgnored = property.HasIgnore();
        bool isHidden = !isIgnored && property.HasHide();
        string? descriptor = default;

        if (!isIgnored)
        {
            descriptor = property.GetDescriptor();
        }

        descriptor ??= property.GetDefaultDescriptor();

        return new Property
        {
            Accessibility = property.DeclaredAccessibility,
            Descriptor = descriptor,
            IsHidden = isHidden,
            IsIgnored = isIgnored,
            Kind = kind,
            Name = property.Name,
        };
    }
}