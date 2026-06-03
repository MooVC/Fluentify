namespace Fluentify.ClassGeneratorTests;

using Fluentify.Snippets;
using Microsoft.CodeAnalysis.Testing;

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

    [Fact]
    public async Task GivenInitializedListImplementationWithoutAccessibleDefaultConstructorThenNoCompilationErrorIsRaised()
    {
        // Arrange
        TestBehaviors = TestBehaviors.SkipGeneratedSourcesCheck;

        TestCode = """
            namespace Fluentify
            {
                using System;

                internal sealed class AutoInitializeWithAttribute
                    : Attribute
                {
                    public AutoInitializeWithAttribute(string factory)
                    {
                    }
                }
            }

            namespace Demo
            {
                using System.Collections.Generic;
                using Fluentify;

                [Fluentify]
                public sealed class Sample
                {
                    public CustomItems Items { get; set; }
                }

                [AutoInitializeWith(nameof(Create))]
                public sealed class CustomItems
                    : List<int>
                {
                    private CustomItems()
                    {
                    }

                    public static CustomItems Create()
                    {
                        return new CustomItems();
                    }
                }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenNestedClassSharesNameWithClassInSameNamespaceThenNoCompilationErrorIsRaised()
    {
        // Arrange
        TestBehaviors = TestBehaviors.SkipGeneratedSourcesCheck;

        TestCode = """
            namespace Demo
            {
                using Fluentify;

                [Fluentify]
                public sealed class Level3
                {
                    public int Value { get; set; }
                }

                public sealed class Level1
                {
                    public sealed class Level2
                    {
                        [Fluentify]
                        public sealed class Level3
                        {
                            public int Value { get; set; }
                        }
                    }
                }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }
}