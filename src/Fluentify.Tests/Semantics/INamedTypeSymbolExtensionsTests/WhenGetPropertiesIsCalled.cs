namespace Fluentify.Semantics.INamedTypeSymbolExtensionsTests;

using Fluentify.Model;
using Fluentify.Semantics;
using Microsoft.CodeAnalysis;

public sealed class WhenGetPropertiesIsCalled
{
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
        Name = "Age",
        Type = "int",
    };

    private static readonly Property attributes = new()
    {
        Descriptor = "WithAttributes",
        Name = "Attributes",
        Type = "IReadOnlyList<object>?",
        IsNullable = true,
    };

    private static readonly Property name = new()
    {
        Descriptor = "WithName",
        Name = "Name",
        Type = "string",
    };

    [Theory]
    [MemberData(nameof(GivenCrossReferencedThenTheExpectedPropertiesAreReturnedData))]
    public void GivenCrossReferencedThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Arrange
        var description = new Property
        {
            Descriptor = "WithDescription",
            Name = "Description",
            Type = "string",
        };

        var simple = new Property
        {
            Descriptor = "WithSimple",
            IsBuildable = true,
            Name = "Simple",
            Type = "global::Fluentify.Tests.Compilation.Simple",
        };

        // Act
        IReadOnlyList<Property> properties = type.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        _ = properties.Should().HaveCount(2);
        _ = properties.Should().Contain(description);
        _ = properties.Should().Contain(simple);
    }

    [Theory]
    [MemberData(nameof(GivenMultipleGenericsThenTheExpectedPropertiesAreReturnedData))]
    public void GivenMultipleGenericsThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Arrange
        var age = new Property
        {
            Descriptor = "WithAge",
            IsNullable = true,
            Name = "Age",
            Type = "T1?",
        };

        var attributes = new Property
        {
            Descriptor = "WithAttributes",
            Name = "Attributes",
            Type = "T3",
        };

        var name = new Property
        {
            Descriptor = "WithName",
            IsBuildable = true,
            IsNullable = true,
            Name = "Name",
            Type = "T2",
        };

        // Act
        IReadOnlyList<Property> properties = type.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        _ = properties.Should().HaveCount(3);
        _ = properties.Should().Contain(age);
        _ = properties.Should().Contain(attributes);
        _ = properties.Should().Contain(name);
    }

    [Theory]
    [MemberData(nameof(GivenSimpleThenTheExpectedPropertiesAreReturnedData))]
    public void GivenSimpleThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Act
        IReadOnlyList<Property> properties = type.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        _ = properties.Should().HaveCount(3);
        _ = properties.Should().Contain(age);
        _ = properties.Should().Contain(attributes);
        _ = properties.Should().Contain(name);
    }

    [Theory]
    [MemberData(nameof(GivenSingleGenericThenTheExpectedPropertiesAreReturnedData))]
    public void GivenSingleGenericThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Arrange
        var attributes = new Property
        {
            Descriptor = "WithAttributes",
            IsNullable = true,
            Name = "Attributes",
            Type = "T",
        };

        // Act
        IReadOnlyList<Property> properties = type.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        _ = properties.Should().HaveCount(3);
        _ = properties.Should().Contain(age);
        _ = properties.Should().Contain(attributes);
        _ = properties.Should().Contain(name);
    }

    [Theory]
    [MemberData(nameof(GivenOneOfThreeIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenOneOfThreeIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Act
        IReadOnlyList<Property> properties = type.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        _ = properties.Should().HaveCount(2);
        _ = properties.Should().Contain(age);
        _ = properties.Should().Contain(attributes);
    }

    [Theory]
    [MemberData(nameof(GivenTwoOfThreeIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenTwoOfThreeIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Act
        IReadOnlyList<Property> properties = type.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        _ = properties.Should().HaveCount(1);
        _ = properties.Should().Contain(attributes);
    }

    [Theory]
    [MemberData(nameof(GivenAllThreeIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenAllThreeIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Act
        IReadOnlyList<Property> properties = type.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        _ = properties.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(GivenDescriptorOnRequiredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenDescriptorOnRequiredThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Arrange
        var age = new Property
        {
            Descriptor = "Aged",
            Name = "Age",
            Type = "int",
        };

        // Act
        IReadOnlyList<Property> properties = type.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        _ = properties.Should().HaveCount(3);
        _ = properties.Should().Contain(age);
        _ = properties.Should().Contain(attributes);
        _ = properties.Should().Contain(name);
    }

    [Theory]
    [MemberData(nameof(GivenDescriptorOnOptionalThenTheExpectedPropertiesAreReturnedData))]
    public void GivenDescriptorOnOptionalThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Arrange
        var attributes = new Property
        {
            Descriptor = "AttributedWith",
            Name = "Attributes",
            Type = "IReadOnlyList<object>?",
            IsNullable = true,
        };

        // Act
        IReadOnlyList<Property> properties = type.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        _ = properties.Should().HaveCount(3);
        _ = properties.Should().Contain(age);
        _ = properties.Should().Contain(attributes);
        _ = properties.Should().Contain(name);
    }

    [Theory]
    [MemberData(nameof(GivenDescriptorOnIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenDescriptorOnIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Type type)
    {
        // Act
        IReadOnlyList<Property> properties = type.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        _ = properties.Should().HaveCount(2);
        _ = properties.Should().Contain(age);
        _ = properties.Should().Contain(attributes);
    }
}