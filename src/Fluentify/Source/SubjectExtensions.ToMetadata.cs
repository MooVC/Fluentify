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
        IReadOnlyList<string> constraints = subject.Generics.ToConstraints();
        string parameters = subject.Generics.ToParameters();

        return new Metadata
        {
            Constraints = constraints,
            Parameters = parameters,
            Subject = subject,
        };
    }
}