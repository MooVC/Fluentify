namespace Fluentify.InternalExtensionsGeneratorTests;

using Fluentify.Snippets;

public sealed class WhenExecuted
    : GeneratorTests<InternalExtensionsGenerator>
{
    public WhenExecuted()
        : base(Classes.ReferenceAssemblies, Classes.LanguageVersion)
    {
    }

    [Fact]
    public async Task GivenAnAssemblyThenTheAttributeIsGenerated()
    {
        // Arrange
        Extensions.Internal.IsExpectedIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }
}