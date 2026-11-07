namespace Fluentify.SkipAutoInitializationAttributeGeneratorTests;

using Fluentify.Snippets;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;

[MethodDataSource(typeof(Frameworks), nameof(Frameworks.DefaultCases))]
public sealed class WhenExecuted
    : GeneratorTests<SkipAutoInitializationAttributeGenerator>
{
    public WhenExecuted(ReferenceAssemblies assembly, LanguageVersion language)
        : base(assembly, language)
    {
    }

    [Test]
    public async Task GivenAnAssemblyThenTheAttributeIsGenerated()
    {
        // Arrange
        Attributes.SkipAutoInitialization.IsExpectedIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }
}