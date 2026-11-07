namespace Fluentify.HideAttributeGeneratorTests;

using Fluentify.Snippets;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;

[MethodDataSource(typeof(Frameworks), nameof(Frameworks.DefaultCases))]
public sealed class WhenExecuted
    : GeneratorTests<HideAttributeGenerator>
{
    public WhenExecuted(ReferenceAssemblies assembly, LanguageVersion language)
        : base(assembly, language)
    {
    }

    [Test]
    public async Task GivenAnAssemblyThenTheAttributeIsGenerated()
    {
        // Arrange
        Attributes.Hide.IsExpectedIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }
}