namespace Fluentify.Source;

using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Subject"/>.
/// </summary>
internal static partial class SubjectExtensions
{
    /// <summary>
    /// Aggregates metadata relating to the <paramref name="subject"/> to facilitate efficient source generation.
    /// </summary>
    /// <param name="subject">The subject from which the metadata is to be sourced.</param>
    /// <returns>The <see cref="Metadata"/> for <paramref name="subject"/>.</returns>
    public static Metadata ToMetadata(this Subject subject)
    {
        IReadOnlyList<string> constraints = [];
        string parameters = string.Empty;

        if (subject.Generics.Count > 0)
        {
            constraints = subject
                .Generics
                .Select(generic => $"where {generic.Name} : {string.Join(", ", generic.Constraints)}")
                .ToArray();

            IEnumerable<string> generics = subject.Generics.Select(generic => generic.Name);

            parameters = string.Join(", ", generics);
            parameters = $"<{parameters}>";
        }

        return new Metadata
        {
            Constraints = constraints,
            Parameters = parameters,
            Subject = subject,
            Type = string.Concat(subject.Name, parameters),
        };
    }
}