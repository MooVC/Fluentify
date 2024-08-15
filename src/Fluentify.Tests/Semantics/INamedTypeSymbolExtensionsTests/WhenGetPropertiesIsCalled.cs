namespace Fluentify.Semantics.INamedTypeSymbolExtensionsTests;

using Fluentify.Model;
using Fluentify.Semantics;
using Microsoft.CodeAnalysis;

public sealed class WhenGetPropertiesIsCalled
{
    public static readonly TheoryData<Compilation, Definition> GivenAllThreeIgnoredThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.AllThreeIgnored },
        { Records.Instance.Compilation, Records.Instance.AllThreeIgnored },
    };

    public static readonly TheoryData<Compilation, Definition> GivenBooleanThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.Boolean },
        { Records.Instance.Compilation, Records.Instance.Boolean },
    };

    public static readonly TheoryData<Compilation, Definition, string> GivenCrossReferencedThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.CrossReferenced, nameof(Classes) },
        { Records.Instance.Compilation, Records.Instance.CrossReferenced, nameof(Records) },
    };

    public static readonly TheoryData<Compilation, Definition, bool> GivenDescriptorOnRequiredThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.DescriptorOnRequired, false },
        { Records.Instance.Compilation, Records.Instance.DescriptorOnRequired, true },
    };

    public static readonly TheoryData<Compilation, Definition, bool> GivenDescriptorOnOptionalThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.DescriptorOnOptional, false },
        { Records.Instance.Compilation, Records.Instance.DescriptorOnOptional, true },
    };

    public static readonly TheoryData<Compilation, Definition, bool> GivenDescriptorOnIgnoredThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.DescriptorOnIgnored, false },
        { Records.Instance.Compilation, Records.Instance.DescriptorOnIgnored, true },
    };

    public static readonly TheoryData<Compilation, Definition, bool> GivenGlobalThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.Global, false },
        { Records.Instance.Compilation, Records.Instance.Global, true },
    };

    public static readonly TheoryData<Compilation, Definition, bool> GivenMultipleGenericsThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.MultipleGenerics, false },
        { Records.Instance.Compilation, Records.Instance.MultipleGenerics, true },
    };

    public static readonly TheoryData<Compilation, Definition, bool> GivenOneOfThreeIgnoredThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.OneOfThreeIgnored, false },
        { Records.Instance.Compilation, Records.Instance.OneOfThreeIgnored, true },
    };

    public static readonly TheoryData<Compilation, Definition, bool> GivenSimpleThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.Simple, false },
        { Records.Instance.Compilation, Records.Instance.Simple, true },
    };

    public static readonly TheoryData<Compilation, Definition, bool> GivenSingleGenericThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.SingleGeneric, false },
        { Records.Instance.Compilation, Records.Instance.SingleGeneric, true },
    };

    public static readonly TheoryData<Compilation, Definition, bool> GivenTwoOfThreeIgnoredThenTheExpectedPropertiesAreReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.TwoOfThreeIgnored, false },
        { Records.Instance.Compilation, Records.Instance.TwoOfThreeIgnored, true },
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
    public void GivenBooleanThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition)
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
        ActAndAssert(compilation, definition, age, isRetired, name);
    }

    [Theory]
    [MemberData(nameof(GivenCrossReferencedThenTheExpectedPropertiesAreReturnedData))]
    public void GivenCrossReferencedThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, string type)
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
                    Name = $"global::Fluentify.{type}.Testing.Simple",
                },
            },
            Name = "Simple",
        };

        // Act & Assert
        ActAndAssert(compilation, definition, description, simple);
    }

    [Theory]
    [MemberData(nameof(GivenGlobalThenTheExpectedPropertiesAreReturnedData))]
    public void GivenGlobalThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        GivenSimpleThenTheExpectedPropertiesAreReturned(compilation, definition, isNullable);
    }

    [Theory]
    [MemberData(nameof(GivenMultipleGenericsThenTheExpectedPropertiesAreReturnedData))]
    public void GivenMultipleGenericsThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        string annotation = isNullable
            ? "?"
            : string.Empty;

        var age = new Property
        {
            Descriptor = "WithAge",
            Kind = new()
            {
                Type = new()
                {
                    IsNullable = isNullable,
                    Name = $"T1{annotation}",
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
                    IsNullable = isNullable,
                    Name = "T2",
                },
            },
            Name = "Name",
        };

        // Act & Assert
        ActAndAssert(compilation, definition, age, attributes, name);
    }

    [Theory]
    [MemberData(nameof(GivenSimpleThenTheExpectedPropertiesAreReturnedData))]
    public void GivenSimpleThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        Property attributes = GetAttributes(isNullable);

        // Act & Assert
        ActAndAssert(compilation, definition, age, attributes, name);
    }

    [Theory]
    [MemberData(nameof(GivenSingleGenericThenTheExpectedPropertiesAreReturnedData))]
    public void GivenSingleGenericThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        var attributes = new Property
        {
            Descriptor = "WithAttributes",
            Kind = new()
            {
                Type = new()
                {
                    IsNullable = isNullable,
                    Name = "T",
                },
            },
            Name = "Attributes",
        };

        // Act & Assert
        ActAndAssert(compilation, definition, age, attributes, name);
    }

    [Theory]
    [MemberData(nameof(GivenOneOfThreeIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenOneOfThreeIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        var name = new Property
        {
            Descriptor = "WithName",
            IsIgnored = true,
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "Name",
        };

        Property attributes = GetAttributes(isNullable);

        // Act & Assert
        ActAndAssert(compilation, definition, age, attributes, name);
    }

    [Theory]
    [MemberData(nameof(GivenTwoOfThreeIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenTwoOfThreeIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        var age = new Property
        {
            Descriptor = "WithAge",
            IsIgnored = true,
            Kind = new()
            {
                Type = new()
                {
                    Name = "int",
                },
            },
            Name = "Age",
        };

        Property attributes = GetAttributes(isNullable);

        var name = new Property
        {
            Descriptor = "WithName",
            IsIgnored = true,
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "Name",
        };

        // Act & Assert
        ActAndAssert(compilation, definition, age, attributes, name);
    }

    [Theory]
    [MemberData(nameof(GivenAllThreeIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenAllThreeIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition)
    {
        // Act
        IReadOnlyList<Property> properties = definition.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        _ = properties.Should().HaveCount(3);
        _ = properties.Should().OnlyContain(property => property.IsIgnored);
    }

    [Theory]
    [MemberData(nameof(GivenDescriptorOnRequiredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenDescriptorOnRequiredThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
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

        Property attributes = GetAttributes(isNullable);

        // Act & Assert
        ActAndAssert(compilation, definition, age, attributes, name);
    }

    [Theory]
    [MemberData(nameof(GivenDescriptorOnOptionalThenTheExpectedPropertiesAreReturnedData))]
    public void GivenDescriptorOnOptionalThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        Property attributes = GetAttributes(isNullable, descriptor: "AttributedWith");

        // Act & Assert
        ActAndAssert(compilation, definition, age, attributes, name);
    }

    [Theory]
    [MemberData(nameof(GivenDescriptorOnIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenDescriptorOnIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        var name = new Property
        {
            Descriptor = "WithName",
            IsIgnored = true,
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "Name",
        };

        Property attributes = GetAttributes(isNullable);

        // Act & Assert
        ActAndAssert(compilation, definition, age, attributes, name);
    }

    private static void ActAndAssert(Compilation compilation, Definition definition, params Property[] expected)
    {
        // Act
        IReadOnlyList<Property> actual = definition.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        _ = actual.Should().HaveCount(expected.Length);
        _ = actual.Should().Contain(expected);
    }

    private static Property GetAttributes(bool isNullable, string descriptor = "WithAttributes")
    {
        string annotation = isNullable
            ? "?"
            : string.Empty;

        return new Property
        {
            Descriptor = descriptor,
            Kind = new()
            {
                Type = new()
                {
                    IsNullable = isNullable,
                    Name = $"IReadOnlyList<object>{annotation}",
                },
            },
            Name = "Attributes",
        };
    }
}