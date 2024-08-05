namespace Fluentify.DescriptorAttributeAnalyzerTests;

using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public abstract partial class WhenExecuted
    : AnalyzerTests<DescriptorAttributeAnalyzer, DescriptorAttributeGenerator>
{
    private static readonly Type[] generators =
    [
        typeof(DescriptorAttributeGenerator),
        typeof(FluentifyAttributeGenerator),
        typeof(IgnoreAttributeGenerator),
    ];

    protected WhenExecuted(ReferenceAssemblies assemblies, LanguageVersion languageVersion)
        : base(assemblies, languageVersion, generators)
    {
    }

    protected static DiagnosticResult GetExpectedDisregardedRule(string member, LinePosition position)
    {
        return GetExpected(member, position, DescriptorAttributeAnalyzer.DisregardedRule);
    }

    protected static DiagnosticResult GetExpectedMissingFluentifyRule(string @class, LinePosition position)
    {
        return GetExpected(@class, position, DescriptorAttributeAnalyzer.MissingFluentifyRule);
    }

    protected static DiagnosticResult GetExpectedValidNamingRule(string descriptor, LinePosition position)
    {
        return GetExpected(descriptor, position, DescriptorAttributeAnalyzer.ValidNamingRule);
    }

    private static DiagnosticResult GetExpected(string argument, LinePosition position, DiagnosticDescriptor rule)
    {
        return new DiagnosticResult(rule)
            .WithArguments(argument)
            .WithLocation(position);
    }
}