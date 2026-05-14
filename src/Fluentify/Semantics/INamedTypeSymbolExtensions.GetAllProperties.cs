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
        var mutable = new List<IPropertySymbol>();

        do
        {
            IEnumerable<IPropertySymbol> properties = current
                .GetMembers()
                .OfType<IPropertySymbol>()
                .Where(property => property.IsMutable());

            mutable.AddRange(properties);

            current = current.BaseType;
        }
        while (current is not null && !cancellationToken.IsCancellationRequested);

        return mutable
            .Distinct(PropertyNameEqualityComparer.Instance)
            .Select(property => property.Map(compilation, cancellationToken))
            .ToList();
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

    private sealed class PropertyNameEqualityComparer : IEqualityComparer<IPropertySymbol>
    {
        public static readonly PropertyNameEqualityComparer Instance = new();

        public bool Equals(IPropertySymbol? x, IPropertySymbol? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return StringComparer.Ordinal.Equals(x.Name, y.Name);
        }

        public int GetHashCode(IPropertySymbol obj)
        {
            return StringComparer.Ordinal.GetHashCode(obj.Name);
        }
    }
}