namespace Fluentify.IgnoreAttributeGeneratorTests;

using Fluentify.Snippets;

public sealed class WhenExecuted
    : GeneratorTests<IgnoreAttributeGenerator>
{
    public WhenExecuted()
        : base(Classes.ReferenceAssemblies, Classes.LanguageVersion)
    {
    }

    [Fact]
    public async Task GivenAnAssemblyThenTheAttributeIsGenerated()
    {
        // Arrange
        Attributes.Ignore.IsExpectedIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }
}