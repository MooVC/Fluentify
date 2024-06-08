namespace Fluentify;

using System.Collections.Generic;
using Fluentify.Model;
using Fluentify.Source;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>
/// Generates extension methods that engineers to rapidly develop rich, expressive, and maintainable Fluent APIs with ease.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class RecordGenerator
    : FluentifyGenerator<RecordDeclarationSyntax>
{
    /// <inheritdoc/>
    private protected override string? GetScalarExtensionMethodBody(Property property, Subject subject)
    {
        return $$"""
            return subject with
            {
                {{property.Name}} = value,
            };
            """;
    }

    /// <inheritdoc/>
    private protected override IEnumerable<Source> GetSource(Subject subject)
    {
        if (subject.IsPartial && !subject.HasDefaultConstructor)
        {
            yield return GenerateConstructor(subject);
        }

        foreach (Source source in base.GetSource(subject))
        {
            yield return source;
        }
    }

    private static Source GenerateConstructor(Subject subject)
    {
        string content = subject.GetConstructor();
        string hint = $"{subject.Namespace}.{subject.Name}.ctor.g.cs";

        return new Source
        {
            Content = content,
            Hint = hint,
        };
    }
}