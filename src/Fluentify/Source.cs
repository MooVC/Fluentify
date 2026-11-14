namespace Fluentify;

using MooVC.Syntax;

internal readonly struct Source(Snippet content, string hint)
{
    public Snippet Content { get; } = content;

    public string Hint { get; } = hint;
}