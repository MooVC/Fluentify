namespace Fluentify.IgnoreAttributeAnalyzerTests;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public abstract partial class WhenExecuted
    : AnalyzerTests<IgnoreAttributeAnalyzer, IgnoreAttributeGenerator>
{
    protected WhenExecuted(ReferenceAssemblies assemblies, LanguageVersion languageVersion)
        : base(assemblies, languageVersion)
    {
    }

    protected static DiagnosticResult GetExpectedMissingFluentifyRule(string @class, LinePosition position)
    {
        return GetExpected(@class, position, IgnoreAttributeAnalyzer.MissingFluentifyRule);
    }

    protected static DiagnosticResult GetExpectedRedundantUsageRule(LinePosition position, string property)
    {
        return GetExpected(property, position, IgnoreAttributeAnalyzer.RedundantUsageRule);
    }

    private static DiagnosticResult GetExpected(string argument, LinePosition position, DiagnosticDescriptor rule)
    {
        return new DiagnosticResult(rule)
            .WithArguments(argument)
            .WithLocation(position);
    }
}