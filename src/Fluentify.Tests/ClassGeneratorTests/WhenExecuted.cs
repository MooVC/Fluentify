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
        typeof(HideAttributeGenerator),
        typeof(IgnoreAttributeGenerator),
        typeof(InternalExtensionsGenerator),
        typeof(SkipAutoInitializationAttributeGenerator),
    ];

    public WhenExecuted()
        : base(Classes.ReferenceAssemblies, Classes.LanguageVersion, _generators)
    {
    }

    [Fact]
    public async Task GivenClassWithoutDefaultConstructorThenNoClassGeneratorSourceIsProduced()
    {
        // Arrange
        TestCode = """
            namespace Demo
            {
                using Fluentify;

                [Fluentify]
                public sealed class Sample
                {
                    public Sample(int value)
                    {
                    }
            
                    public int Value { get; }
                }
            }
            """;

        Attributes.Descriptor.IsExpectedIn(TestState);
        Attributes.Fluentify.IsExpectedIn(TestState);
        Attributes.Hide.IsExpectedIn(TestState);
        Attributes.Ignore.IsExpectedIn(TestState);
        Extensions.Internal.IsExpectedIn(TestState);
        Attributes.SkipAutoInitialization.IsExpectedIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Theory]
    [Declared(typeof(Classes))]
    public async Task GivenAClassTheExpectedSourceIsGenerated(Declared declared)
    {
        // Arrange
        declared.IsDeclaredIn(TestState);

        Attributes.Descriptor.IsExpectedIn(TestState);
        Attributes.Fluentify.IsExpectedIn(TestState);
        Attributes.Hide.IsExpectedIn(TestState);
        Attributes.Ignore.IsExpectedIn(TestState);
        Extensions.Internal.IsExpectedIn(TestState);
        Attributes.SkipAutoInitialization.IsExpectedIn(TestState);

        // Act & Assert
        await ActAndAssertAsync();
    }
}