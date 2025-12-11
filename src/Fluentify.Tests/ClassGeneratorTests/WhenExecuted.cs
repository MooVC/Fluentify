namespace Fluentify.ClassGeneratorTests;

using Fluentify.Snippets;

public sealed class WhenExecuted
    : GeneratorTests<ClassGenerator>
{
    private static readonly Type[] _generators =
    [
        typeof(ClassGenerator),
        typeof(DescriptorAttributeGenerator),
        typeof(FluentifyAttributeGenerator),
        typeof(IgnoreAttributeGenerator),
        typeof(InternalExtensionsGenerator),
        typeof(SkipAutoInstantiationAttributeGenerator),
    ];

    public WhenExecuted()
        : base(Classes.ReferenceAssemblies, Classes.LanguageVersion, _generators)
    {
    }

    [Theory]
    [Declared(typeof(Classes))]
    public async Task GivenAClassTheExpectedSourceIsGenerated(Declared declared)
    {
        // Arrange
        declared.IsDeclaredIn(TestState);

        Attributes.Descriptor.IsExpectedIn(TestState);
        Attributes.Fluentify.IsExpectedIn(TestState);
        Attributes.Ignore.IsExpectedIn(TestState);
        Extensions.Internal.IsExpectedIn(TestState);
        Attributes.SkipAutoInstantiation.IsExpectedIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }
}