namespace Fluentify.FluentifyAttributeGeneratorTests;

using Fluentify.Snippets;

public sealed class WhenExecuted
    : GeneratorTests<FluentifyAttributeGenerator>
{
    public WhenExecuted()
        : base(Classes.ReferenceAssemblies, Classes.LanguageVersion)
    {
    }

    [Fact]
    public async Task GivenAnAssemblyThenTheAttributeIsGenerated()
    {
        // Arrange
        Attributes.Fluentify.IsExpectedIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }
}