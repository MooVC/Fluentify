namespace Fluentify.HideAttributeGeneratorTests;

using Fluentify.Snippets;

public sealed class WhenExecuted
    : GeneratorTests<HideAttributeGenerator>
{
    public WhenExecuted()
        : base(Classes.ReferenceAssemblies, Classes.LanguageVersion)
    {
    }

    [Fact]
    public async Task GivenAnAssemblyThenTheAttributeIsGenerated()
    {
        // Arrange
        Attributes.Hide.IsExpectedIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }
}