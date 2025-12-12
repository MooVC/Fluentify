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
            return new {{subject.Type}}
            {
                {{initializers.Indent()}}
            };
            """;
    }

    /// <inheritdoc/>
    private protected override string Wrap(string content)
    {
        return $"""
            #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            #nullable enable
            #endif
            
            {content}

            #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            #nullable restore
            #endif
            """;
    }

    private static string GetInitializers(Property property, Subject subject)
    {
        var initializers = new StringBuilder();

        foreach (Property member in subject.Properties.OrderBy(property => property.Name))
        {
            string initializer = member == property
                ? $"{member.Name} = value"
                : $"{member.Name} = subject.{member.Name}";

            initializers = initializers.AppendLine($"{initializer},");
        }

        return initializers.ToString();
    }
}