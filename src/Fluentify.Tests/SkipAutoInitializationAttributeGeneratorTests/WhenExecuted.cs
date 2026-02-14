namespace Fluentify.SkipAutoInitializationAttributeGeneratorTests;

using Fluentify.Snippets;

public sealed class WhenExecuted
    : GeneratorTests<SkipAutoInitializationAttributeGenerator>
{
    public WhenExecuted()
        : base(Classes.ReferenceAssemblies, Classes.LanguageVersion)
    {
    }

    [Fact]
    public async Task GivenAnAssemblyThenTheAttributeIsGenerated()
    {
        // Arrange
        Attributes.SkipAutoInitialization.IsExpectedIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }
}