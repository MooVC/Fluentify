namespace Fluentify;

using System.Collections.Generic;
using System.Text;
using Fluentify.Model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>
/// Generates extension methods that engineers to rapidly develop rich, expressive, and maintainable Fluent APIs with ease.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class ClassGenerator
    : FluentifyGenerator<ClassDeclarationSyntax>
{
    /// <inheritdoc/>
    private protected override IEnumerable<Source> GetSource(Subject subject)
    {
        return subject.HasDefaultConstructor
            ? base.GetSource(subject)
            : [];
    }

    /// <inheritdoc/>
    private protected override string? GetScalarExtensionMethodBody(Property property, Subject subject)
    {
        string initializers = GetInitializers(property, subject);

        return $$"""
            return new {{subject.Name}}
            {
                {{initializers.Indent()}}
            };
            """;
    }

    private static string GetInitializers(Property property, Subject subject)
    {
        var initializers = new StringBuilder();

        foreach (Property member in subject.Properties)
        {
            string initializer = member == property
                ? $"{property.Name} = value"
                : $"{property.Name} = subject.{property.Name}";

            initializers = initializers.AppendLine(initializer);
        }

        return initializers.ToString();
    }
}