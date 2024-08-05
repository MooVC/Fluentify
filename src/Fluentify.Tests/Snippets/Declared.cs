namespace Fluentify.Snippets;

using System.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

[DebuggerDisplay("{Name,nq}")]
public sealed record Declared(string Content, string Name, params Generated[] Expected)
{
    public Declared(string[] content, string name, params Generated[] expected)
        : this(string.Join(Environment.NewLine, content), name, expected)
    {
    }

    public void IsDeclaredIn(SolutionState state)
    {
        state.Sources.Add(Content);

        foreach (Generated expected in Expected)
        {
            expected.IsExpectedIn(state);
        }
    }
}