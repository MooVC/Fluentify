namespace Fluentify;

using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Kind = System.Type;

public abstract class AnalyzerTests<TAnalyzer, TGenerator>
    : CSharpAnalyzerTest<TAnalyzer, DefaultVerifier>
    where TAnalyzer : DiagnosticAnalyzer, new()
    where TGenerator : new()
{
    protected AnalyzerTests()
    {
        ReferenceAssemblies = ReferenceAssemblies.Net.Net60;
        TestBehaviors = TestBehaviors.SkipGeneratedSourcesCheck;
    }

    protected override ParseOptions CreateParseOptions()
    {
        return new CSharpParseOptions(LanguageVersion.CSharp9);
    }

    protected override IEnumerable<Kind> GetSourceGenerators()
    {
        return [typeof(FluentifyAttributeGenerator), typeof(TGenerator)];
    }

    protected Task ActAndAssertAsync()
    {
        // Act
        Func<Task> act = () => RunAsync();

        // Assert
        return act.Should().NotThrowAsync();
    }
}