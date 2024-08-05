namespace Fluentify.Snippets;

using System.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

[DebuggerDisplay("{Hint,nq}")]
public sealed record Generated(string Content, Type Generator, string Hint)
{
    public void IsExpectedIn(SolutionState state)
    {
        state.GeneratedSources.Add((sourceGeneratorType: Generator, filename: $"{Hint}.g.cs", content: Content));
    }
}