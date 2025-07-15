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
    private readonly LanguageVersion _languageVersion;

    protected GeneratorTests(ReferenceAssemblies assemblies, LanguageVersion languageVersion, params Type[] generators)
    {
        _generators = generators.Length == 0
            ? [typeof(TGenerator)]
            : generators;
        _languageVersion = languageVersion;
        ReferenceAssemblies = assemblies;
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
}