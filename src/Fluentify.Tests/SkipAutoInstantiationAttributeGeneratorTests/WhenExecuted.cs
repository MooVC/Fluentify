namespace Fluentify.SkipAutoInstantiationAttributeGeneratorTests;

using Fluentify.Snippets;

public sealed class WhenExecuted
    : GeneratorTests<SkipAutoInstantiationAttributeGenerator>
{
    public WhenExecuted()
        : base(Classes.ReferenceAssemblies, Classes.LanguageVersion)
    {
    }

    [Fact]
    public async Task GivenAnAssemblyThenTheAttributeIsGenerated()
    {
        // Arrange
        Attributes.SkipAutoInstantiation.IsExpectedIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }
}