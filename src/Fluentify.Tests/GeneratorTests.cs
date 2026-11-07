namespace Fluentify;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;

public abstract class GeneratorTests<TGenerator>
    : CSharpSourceGeneratorTest<TGenerator, DefaultVerifier>
    where TGenerator : new()
{
    private readonly Type[] _generators;

    private LanguageVersion _languageVersion;

    protected GeneratorTests(ReferenceAssemblies assemblies, LanguageVersion languageVersion, params Type[] generators)
    {
        _generators = generators.Length == 0
            ? [typeof(TGenerator)]
            : generators;

        SetTarget(assemblies, languageVersion);
    }

    protected sealed override ParseOptions CreateParseOptions()
    {
        return new CSharpParseOptions(_languageVersion);
    }

    protected sealed override IEnumerable<Type> GetSourceGenerators()
    {
        return _generators;
    }

    protected Task ActAndAssertAsync()
    {
        return RunAsync();
    }

    protected void SetTarget(ReferenceAssemblies assemblies, LanguageVersion languageVersion)
    {
        ReferenceAssemblies = assemblies;
        _languageVersion = languageVersion;
    }
}