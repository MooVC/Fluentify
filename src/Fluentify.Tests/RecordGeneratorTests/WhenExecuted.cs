namespace Fluentify.RecordGeneratorTests;

using Fluentify.Snippets;

public sealed class WhenExecuted
    : GeneratorTests<RecordGenerator>
{
    private static readonly Type[] generators =
    [
        typeof(DescriptorAttributeGenerator),
        typeof(FluentifyAttributeGenerator),
        typeof(IgnoreAttributeGenerator),
        typeof(InternalExtensionsGenerator),
        typeof(RecordGenerator),
    ];

    public WhenExecuted()
        : base(Records.ReferenceAssemblies, Records.LanguageVersion, generators)
    {
    }

    [Theory]
    [Declared(typeof(Records))]
    public async Task GivenARecordTheExpectedSourceIsGenerated(Declared declared)
    {
        // Arrange
        Attributes.Descriptor.IsExpectedIn(TestState);
        Attributes.Fluentify.IsExpectedIn(TestState);
        Attributes.Ignore.IsExpectedIn(TestState);
        Extensions.Internal.IsExpectedIn(TestState);

        declared.IsDeclaredIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }
}