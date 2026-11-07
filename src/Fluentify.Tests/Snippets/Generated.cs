namespace Fluentify.Snippets;

using System.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

[DebuggerDisplay("{Hint,nq}")]
public sealed record Generated(string Content, Type Generator, string Hint)
{
    public void IsExpectedIn(SolutionState state, bool withNullableDirectives = false)
    {
        string content = withNullableDirectives
            ? $"""
                #nullable enable
                #pragma warning disable CS8625

                {Content}

                #pragma warning restore CS8625
                #nullable restore
                """
            : Content;

        state.GeneratedSources.Add((sourceGeneratorType: Generator, filename: $"{Hint}.g.cs", content));
    }
}