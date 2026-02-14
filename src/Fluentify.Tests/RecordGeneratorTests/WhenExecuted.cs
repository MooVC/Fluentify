namespace Fluentify.RecordGeneratorTests;

using Fluentify.Snippets;

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

    public WhenExecuted()
        : base(Records.ReferenceAssemblies, Records.LanguageVersion, _generators)
    {
    }

    [Theory]
    [Declared(typeof(Records))]
    public async Task GivenARecordTheExpectedSourceIsGenerated(Declared declared)
    {
        // Arrange
        Attributes.Descriptor.IsExpectedIn(TestState);
        Attributes.Fluentify.IsExpectedIn(TestState);
        Attributes.Hide.IsExpectedIn(TestState);
        Attributes.Ignore.IsExpectedIn(TestState);
        Extensions.Internal.IsExpectedIn(TestState);
        Attributes.SkipAutoInitialization.IsExpectedIn(TestState);

        declared.IsDeclaredIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }
}