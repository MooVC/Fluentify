namespace Fluentify.Semantics.INamedTypeSymbolExtensionsTests;

using Fluentify.Model;
using Fluentify.Semantics;
using Microsoft.CodeAnalysis;
using Type = Fluentify.Type;

public sealed class WhenGetPropertiesIsCalled
{
    public static readonly TheoryData<Compilation, Type> GivenBooleanThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.Boolean },
        { Records.Instance.Compilation, Records.Instance.Boolean },
    };

    public static readonly TheoryData<Compilation, Type> GivenCrossReferencedThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.CrossReferenced },
        { Records.Instance.Compilation, Records.Instance.CrossReferenced },
    };

    public static readonly TheoryData<Compilation, Type> GivenMultipleGenericsThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.MultipleGenerics },
        { Records.Instance.Compilation, Records.Instance.MultipleGenerics },
    };

    public static readonly TheoryData<Compilation, Type> GivenSimpleThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.Simple },
        { Records.Instance.Compilation, Records.Instance.Simple },
    };

    public static readonly TheoryData<Compilation, Type> GivenSingleGenericThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.SingleGeneric },
        { Records.Instance.Compilation, Records.Instance.SingleGeneric },
    };

    public static readonly TheoryData<Compilation, Type> GivenOneOfThreeIgnoredThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.OneOfThreeIgnored },
        { Records.Instance.Compilation, Records.Instance.OneOfThreeIgnored },
    };

    public static readonly TheoryData<Compilation, Type> GivenTwoOfThreeIgnoredThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.TwoOfThreeIgnored },
        { Records.Instance.Compilation, Records.Instance.TwoOfThreeIgnored },
    };

    public static readonly TheoryData<Compilation, Type> GivenAllThreeIgnoredThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.AllThreeIgnored },
        { Records.Instance.Compilation, Records.Instance.AllThreeIgnored },
    };

    public static readonly TheoryData<Compilation, Type> GivenDescriptorOnRequiredThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.DescriptorOnRequired },
        { Records.Instance.Compilation, Records.Instance.DescriptorOnRequired },
    };

    public static readonly TheoryData<Compilation, Type> GivenDescriptorOnOptionalThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.DescriptorOnOptional },
        { Records.Instance.Compilation, Records.Instance.DescriptorOnOptional },
    };

    public static readonly TheoryData<Compilation, Type> GivenDescriptorOnIgnoredThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.DescriptorOnIgnored },
        { Records.Instance.Compilation, Records.Instance.DescriptorOnIgnored },
    };

    private static readonly Property age = new()
    {
        Descriptor = "WithAge",
        Kind = new()
        {
            Type = new()
            {
                Name = "int",
            },
        },
        Name = "Age",
    };

    private static readonly Property attributes = new()
    {
        Descriptor = "WithAttributes",
        Kind = new()
        {
            Type = new()
            {
                IsNullable = true,
                Name = "IReadOnlyList<object>?",
            },
        },
        Name = "Attributes",
    };

    private static readonly Property name = new()
    {
        Descriptor = "WithName",
        Kind = new()
        {
            Type = new()
            {
                Name = "string",
            },
        },
        Name = "Name",
    };

    [Theory]
    [MemberData(nameof(GivenBooleanThenTheExpectedPropertiesAreReturnedData))]
    public void GivenBooleanThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Arrange
        var isRetired = new Property
        {
            Descriptor = "IsRetired",
            Kind = new()
            {
                Type = new()
                {
                    Name = "bool",
                },
            },
            Name = "IsRetired",
        };

        // Act & Assert
        ActAndAssert(compilation, type, age, isRetired, name);
    }

    [Theory]
    [MemberData(nameof(GivenCrossReferencedThenTheExpectedPropertiesAreReturnedData))]
    public void GivenCrossReferencedThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Arrange
        var description = new Property
        {
            Descriptor = "WithDescription",
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "Description",
        };

        var simple = new Property
        {
            Descriptor = "WithSimple",
            Kind = new()
            {
                Type = new()
                {
                    IsBuildable = true,
                    Name = "global::Fluentify.Tests.Compilation.Simple",
                },
            },
            Name = "Simple",
        };

        // Act & Assert
        ActAndAssert(compilation, type, description, simple);
    }

    [Theory]
    [MemberData(nameof(GivenMultipleGenericsThenTheExpectedPropertiesAreReturnedData))]
    public void GivenMultipleGenericsThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Arrange
        var age = new Property
        {
            Descriptor = "WithAge",
            Kind = new()
            {
                Type = new()
                {
                    IsNullable = true,
                    Name = "T1?",
                },
            },
            Name = "Age",
        };

        var attributes = new Property
        {
            Descriptor = "WithAttributes",
            Kind = new()
            {
                Type = new()
                {
                    Name = "T3",
                },
            },
            Name = "Attributes",
        };

        var name = new Property
        {
            Descriptor = "WithName",
            Kind = new()
            {
                Type = new()
                {
                    IsBuildable = true,
                    IsNullable = true,
                    Name = "T2",
                },
            },
            Name = "Name",
        };

        // Act & Assert
        ActAndAssert(compilation, type, age, attributes, name);
    }

    [Theory]
    [MemberData(nameof(GivenSimpleThenTheExpectedPropertiesAreReturnedData))]
    public void GivenSimpleThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Act & Assert
        ActAndAssert(compilation, type, age, attributes, name);
    }

    [Theory]
    [MemberData(nameof(GivenSingleGenericThenTheExpectedPropertiesAreReturnedData))]
    public void GivenSingleGenericThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Arrange
        var attributes = new Property
        {
            Descriptor = "WithAttributes",
            Kind = new()
            {
                Type = new()
                {
                    IsNullable = true,
                    Name = "T",
                },
            },
            Name = "Attributes",
        };

        // Act & Assert
        ActAndAssert(compilation, type, age, attributes, name);
    }

    [Theory]
    [MemberData(nameof(GivenOneOfThreeIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenOneOfThreeIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Arrange
        var name = new Property
        {
            Descriptor = "WithName",
            Name = "Name",
        };

        // Act & Assert
        ActAndAssert(compilation, type, age, attributes, name);
    }

    [Theory]
    [MemberData(nameof(GivenTwoOfThreeIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenTwoOfThreeIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Arrange
        var age = new Property
        {
            Descriptor = "WithAge",
            Name = "Age",
        };

        var name = new Property
        {
            Descriptor = "WithName",
            Name = "Name",
        };

        // Act & Assert
        ActAndAssert(compilation, type, age, attributes, name);
    }

    [Theory]
    [MemberData(nameof(GivenAllThreeIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenAllThreeIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Act
        IReadOnlyList<Property> properties = type.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        _ = properties.Should().HaveCount(3);
        _ = properties.Should().OnlyContain(property => property.IsIgnored);
    }

    [Theory]
    [MemberData(nameof(GivenDescriptorOnRequiredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenDescriptorOnRequiredThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Arrange
        var age = new Property
        {
            Descriptor = "Aged",
            Kind = new()
            {
                Type = new()
                {
                    Name = "int",
                },
            },
            Name = "Age",
        };

        // Act & Assert
        ActAndAssert(compilation, type, age, attributes, name);
    }

    [Theory]
    [MemberData(nameof(GivenDescriptorOnOptionalThenTheExpectedPropertiesAreReturnedData))]
    public void GivenDescriptorOnOptionalThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Arrange
        var attributes = new Property
        {
            Descriptor = "AttributedWith",
            Kind = new()
            {
                Type = new()
                {
                    IsNullable = true,
                    Name = "IReadOnlyList<object>?",
                },
            },
            Name = "Attributes",
        };

        // Act & Assert
        ActAndAssert(compilation, type, age, attributes, name);
    }

    [Theory]
    [MemberData(nameof(GivenDescriptorOnIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenDescriptorOnIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        GivenOneOfThreeIgnoredThenTheExpectedPropertiesAreReturned(compilation, type);
    }

    private static void ActAndAssert(Compilation compilation, Type type, params Property[] expected)
    {
        // Act
        IReadOnlyList<Property> actual = type.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        _ = actual.Should().HaveCount(expected.Length);
        _ = actual.Should().Contain(expected);
    }
}