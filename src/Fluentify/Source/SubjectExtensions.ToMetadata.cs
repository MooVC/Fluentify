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
        IReadOnlyList<Generic> generics = subject.Collect();
        IReadOnlyList<string> constraints = generics.ToConstraints();
        string parameters = generics.ToParameters();

        return new Metadata
        {
            Constraints = constraints,
            Parameters = parameters,
            Subject = subject,
        };
    }

    private static IReadOnlyList<Generic> Collect(this Subject subject)
    {
        var generics = new List<Generic>();

        foreach (Nesting nesting in subject.Nesting)
        {
            generics.AddRange(nesting.Generics);
        }

        generics.AddRange(subject.Generics);

        return [.. generics];
    }
}