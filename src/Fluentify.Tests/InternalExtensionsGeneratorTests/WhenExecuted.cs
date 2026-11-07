namespace Fluentify.InternalExtensionsGeneratorTests;

using Fluentify.Snippets;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;

[MethodDataSource(typeof(Frameworks), nameof(Frameworks.DefaultCases))]
public sealed class WhenExecuted
    : GeneratorTests<InternalExtensionsGenerator>
{
    public WhenExecuted(ReferenceAssemblies assembly, LanguageVersion language)
        : base(assembly, language)
    {
    }

    [Test]
    public async Task GivenAnAssemblyThenTheAttributeIsGenerated()
    {
        // Arrange
        Extensions.Internal.IsExpectedIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }
}