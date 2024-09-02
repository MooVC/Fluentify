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
        return GetExpected(position, DescriptorAttributeAnalyzer.DisregardedRule, member);
    }

    protected static DiagnosticResult GetExpectedMissingFluentifyRule(string @class, LinePosition position)
    {
        return GetExpected(position, DescriptorAttributeAnalyzer.MissingFluentifyRule, @class);
    }

    protected static DiagnosticResult GetExpectedRedundantRule(string descriptor, string member, LinePosition position)
    {
        return GetExpected(position, DescriptorAttributeAnalyzer.RedundantRule, descriptor, member);
    }

    protected static DiagnosticResult GetExpectedValidNamingRule(string descriptor, LinePosition position)
    {
        return GetExpected(position, DescriptorAttributeAnalyzer.ValidNamingRule, descriptor);
    }

    private static DiagnosticResult GetExpected(LinePosition position, DiagnosticDescriptor rule, params object[] arguments)
    {
        return new DiagnosticResult(rule)
            .WithArguments(arguments)
            .WithLocation(position);
    }
}