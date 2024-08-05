namespace Fluentify;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;

public abstract class GeneratorTests<TGenerator>
    : CSharpSourceGeneratorTest<TGenerator, DefaultVerifier>
    where TGenerator : new()
{
    private readonly Type[] generators;
    private readonly LanguageVersion languageVersion;

    protected GeneratorTests(ReferenceAssemblies assemblies, LanguageVersion languageVersion, params Type[] generators)
    {
        this.generators = generators.Length == 0
            ? [typeof(TGenerator)]
            : generators;

        this.languageVersion = languageVersion;
        ReferenceAssemblies = assemblies;
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
        return RunAsync();
    }
}