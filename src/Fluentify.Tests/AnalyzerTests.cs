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
    private readonly Type[] _generators;
    private readonly LanguageVersion _languageVersion;

    protected AnalyzerTests(ReferenceAssemblies assemblies, LanguageVersion languageVersion, params Type[] generators)
    {
        _generators = generators.Length == 0
            ? [typeof(FluentifyAttributeGenerator), typeof(TGenerator)]
            : generators;

        _languageVersion = languageVersion;
        ReferenceAssemblies = assemblies;
        TestBehaviors = TestBehaviors.SkipGeneratedSourcesCheck;
    }

    protected sealed override ParseOptions CreateParseOptions()
    {
        return new CSharpParseOptions(_languageVersion);
    }

    protected sealed override IEnumerable<Type> GetSourceGenerators()
    {
        return _generators.Distinct();
    }

    protected Task ActAndAssertAsync()
    {
        // Act
        Func<Task> act = () => RunAsync();

        // Assert
        return act.ShouldNotThrowAsync();
    }
}