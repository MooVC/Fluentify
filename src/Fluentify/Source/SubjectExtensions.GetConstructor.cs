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

        string code = $$"""
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

        code = Nest(code, subject);

        return $"""
            using System.Diagnostics.CodeAnalysis;

            {code}
            """;
    }

    private static string Nest(string code, Subject subject)
    {
        foreach (Nesting parent in subject.Nesting)
        {
            code = code.Indent();

            code = $$"""
                {{parent.Declaration}} {{parent.Qualification}}
                {
                    {{code}}
                }
                """;
        }

        return code;
    }
}