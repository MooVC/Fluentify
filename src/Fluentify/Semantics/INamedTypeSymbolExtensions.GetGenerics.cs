namespace Fluentify.Semantics;

using Fluentify.Model;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Returns a collection of <see cref="Generic"/> for each type parameter identified for <paramref name="symbol"/>.
    /// </summary>
    /// <param name="symbol">The subject from which the type parameters are identified.</param>
    /// <returns>The collection of <see cref="Generic"/> for each type parameter identified for <paramref name="symbol"/>.</returns>
    public static IReadOnlyList<Generic> GetGenerics(this INamedTypeSymbol symbol)
    {
        return symbol
            .TypeParameters
            .Select(parameter => new Generic
            {
                Constraints = parameter.GetConstraints(),
                Name = parameter.Name,
            })
            .ToArray();
    }
}