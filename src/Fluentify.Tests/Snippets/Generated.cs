namespace Fluentify.Snippets;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;

[DebuggerDisplay("{Hint,nq}")]
public sealed record Generated(string Content, Type Generator, string Hint)
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1316:Tuple element names should use correct casing", Justification = "The casing matches the delcaration.")]
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