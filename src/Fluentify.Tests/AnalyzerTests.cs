namespace Fluentify;

using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

public abstract class AnalyzerTests<TAnalyzer, TGenerator>
    : CSharpAnalyzerTest<TAnalyzer, DefaultVerifier>
    where TAnalyzer : DiagnosticAnalyzer, new()
    where TGenerator : new()
{
    private readonly Type[] generators;
    private readonly LanguageVersion languageVersion;

    protected AnalyzerTests(ReferenceAssemblies assemblies, LanguageVersion languageVersion, params Type[] generators)
    {
        this.generators = generators.Length == 0
            ? [typeof(FluentifyAttributeGenerator), typeof(TGenerator)]
            : generators;

        this.languageVersion = languageVersion;
        ReferenceAssemblies = assemblies;
        TestBehaviors = TestBehaviors.SkipGeneratedSourcesCheck;
    }

    protected sealed override ParseOptions CreateParseOptions()
    {
        return new CSharpParseOptions(languageVersion);
    }

    protected sealed override IEnumerable<Type> GetSourceGenerators()
    {
        return generators;
    }

    protected Task ActAndAssertAsync()
    {
        // Act
        Func<Task> act = () => RunAsync();

        // Assert
        return act.ShouldNotThrowAsync();
    }
}