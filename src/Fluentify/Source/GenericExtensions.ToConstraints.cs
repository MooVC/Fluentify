namespace Fluentify.Source;

using System.Collections.Generic;
using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Generic"/>.
/// </summary>
internal static partial class GenericExtensions
{
    /// <summary>
    /// Returns the generic constraints based for the <paramref name="generics"/> provided.
    /// </summary>
    /// <param name="generics">The list of generics for which the constraints are to be generated.</param>
    /// <returns>The generic constraints, expressed with the where clause.</returns>
    public static IReadOnlyList<string> ToConstraints(this IReadOnlyList<Generic> generics)
    {
        if (generics.Count == 0)
        {
            return [];
        }

        return generics
            .Select(generic => $"where {generic.Name} : {string.Join(", ", generic.Constraints)}")
            .ToArray();
    }
}