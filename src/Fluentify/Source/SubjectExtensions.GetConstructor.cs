namespace Fluentify.Source;

using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Subject"/>.
/// </summary>
internal static partial class SubjectExtensions
{
    /// <summary>
    /// Gets the source code needed to add the default constructor to the <paramref name="subject"/>.
    /// </summary>
    /// <param name="subject">The <see cref="Subject"/> to which the default constructor is to be added.</param>
    /// <returns>
    /// The source code needed to add the default constructor to the <paramref name="subject"/>.
    /// </returns>
    public static string GetConstructor(this Subject subject)
    {
        IEnumerable<string> initializers = subject.Properties.Select(property => $"default({property.Kind})");
        string parameters = string.Join(", ", initializers);

        return $$"""
            using System.Diagnostics.CodeAnalysis;

            partial record {{subject}}
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public {{subject.Name}}()
                    : this({{parameters}})
                {
                }

                #pragma warning restore CS8604
            }
            """;
    }
}