namespace Fluentify;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>
/// Generates extension methods that engineers to rapidly develop rich, expressive, and maintainable Fluent APIs with ease.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class ClassGenerator
    : FluentifyGenerator<ClassDeclarationSyntax>
{
}