namespace Fluentify.Source;

using System.Collections.Generic;
using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Generic"/>.
/// </summary>
internal static partial class GenericExtensions
{
    /// <summary>
    /// Returns the generic parameters based for the <paramref name="generics"/> provided.
    /// </summary>
    /// <param name="generics">The list of generics for which the parameters are to be generated.</param>
    /// <returns>The generic parameters, expressed as a string with comma separators and encased with &lt; and &gt;.</returns>
    public static string ToParameters(this IReadOnlyList<Generic> generics)
    {
        if (generics.Count == 0)
        {
            return string.Empty;
        }

        IEnumerable<string> arguments = generics.Select(generic => generic.Name);
        string parameters = string.Join(", ", arguments);

        return $"<{parameters}>";
    }
}