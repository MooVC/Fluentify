namespace Fluentify.RecordGeneratorTests;

using Fluentify.Snippets;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;

[MethodDataSource(typeof(DeclaredCases), nameof(DeclaredCases.ForRecords))]
public sealed class WhenExecuted
    : GeneratorTests<RecordGenerator>
{
    private static readonly Type[] _generators =
    [
        typeof(DescriptorAttributeGenerator),
        typeof(FluentifyAttributeGenerator),
        typeof(HideAttributeGenerator),
        typeof(IgnoreAttributeGenerator),
        typeof(InternalExtensionsGenerator),
        typeof(RecordGenerator),
        typeof(SkipAutoInitializationAttributeGenerator),
    ];

    private readonly Declared _declared;

    public WhenExecuted(Declared declaration, ReferenceAssemblies assembly, LanguageVersion language)
        : base(assembly, language, _generators)
    {
        _declared = declaration;
    }

    [Test]
    public async Task GivenARecordTheExpectedSourceIsGenerated()
    {
        // Arrange
        Attributes.Descriptor.IsExpectedIn(TestState);
        Attributes.Fluentify.IsExpectedIn(TestState);
        Attributes.Hide.IsExpectedIn(TestState);
        Attributes.Ignore.IsExpectedIn(TestState);
        Extensions.Internal.IsExpectedIn(TestState);

        _declared.IsDeclaredIn(TestState);
        Attributes.SkipAutoInitialization.IsExpectedIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }
}