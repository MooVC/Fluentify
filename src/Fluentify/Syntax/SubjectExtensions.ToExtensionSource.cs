namespace Fluentify.Syntax;

using Fluentify.Model;
using Microsoft.CodeAnalysis;
using MooVC.Syntax;
using MooVC.Syntax.CSharp;
using Accessibility = Microsoft.CodeAnalysis.Accessibility;

internal static partial class SubjectExtensions
{
    public static Snippet ToExtensionSource(this Subject subject, params Method[] methods)
    {
        Scopes scope = subject.Accessibility < Accessibility.Public ? Scopes.Internal : Scopes.Public;

        Snippet source = Class.Undefined.Named(subject.GetExtensionClassName())
            .WithScope(scope)
            .IsPartial(true)
            .IsStatic(true)
            .WithMethods(methods)
            .ToSnippet(Configuration.Options);

        // MooVC 10.1.0 separates multiple where clauses with a comma.
        string[] lines = Enumerable.Range(0, source.Lines).Select(index => SeparateConstraints(source[index])).ToArray();

        return Snippet.From(Configuration.Options, lines);
    }

    private static string SeparateConstraints(string line)
    {
        string clause = line.TrimStart();

        if (!clause.StartsWith("where ", StringComparison.Ordinal))
        {
            return line;
        }

        string indent = line.Substring(0, line.Length - clause.Length);

        return line.Replace(", where ", $"\n{indent}where ");
    }
}