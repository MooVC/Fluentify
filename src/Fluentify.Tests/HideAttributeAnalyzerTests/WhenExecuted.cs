namespace Fluentify.HideAttributeAnalyzerTests;

using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public abstract partial class WhenExecuted
    : AnalyzerTests<HideAttributeAnalyzer, HideAttributeGenerator>
{
    protected WhenExecuted(ReferenceAssemblies assemblies, LanguageVersion languageVersion, params Type[] generators)
        : base(assemblies, languageVersion, generators)
    {
    }

    protected static DiagnosticResult GetExpectedMissingFluentifyRule(string property, LinePosition position)
    {
        return GetExpected(property, position, HideAttributeAnalyzer.MissingFluentifyRule);
    }

    protected static DiagnosticResult GetExpectedConflictingAttributesRule(LinePosition position, string property)
    {
        return GetExpected(property, position, HideAttributeAnalyzer.ConflictingAttributesRule);
    }

    private static DiagnosticResult GetExpected(string argument, LinePosition position, DiagnosticDescriptor rule)
    {
        return new DiagnosticResult(rule)
            .WithArguments(argument)
            .WithLocation(position);
    }
}