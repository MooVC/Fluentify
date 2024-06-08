namespace Fluentify.Semantics.IPropertySymbolExtensionsTests;

using System.Collections.Immutable;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using NSubstitute;
using Xunit;

public sealed class WhenHasAttributeIsCalled
{
    [Fact]
    public void GivenPropertyWithMatchingAttributeThenReturnsTrue()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns("Fluentify.TestAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns(ImmutableArray.Create(data));

        // Act
        bool result = property.HasAttribute("Test");

        // Assert
        _ = result.Should().BeTrue();
    }

    [Fact]
    public void GivenPropertyWithNonMatchingAttributeThenReturnsFalse()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString().Returns("Fluentify.OtherAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns(ImmutableArray.Create(data));

        // Act
        bool result = property.HasAttribute("Test");

        // Assert
        _ = result.Should().BeFalse();
    }

    [Fact]
    public void GivenPropertyWithNoAttributesThenReturnsFalse()
    {
        // Arrange
        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns(ImmutableArray<AttributeData>.Empty);

        // Act
        bool result = property.HasAttribute("Test");

        // Assert
        _ = result.Should().BeFalse();
    }

    [Fact]
    public void GivenPropertyParameterWithMatchingAttributeThenReturnsTrue()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns("Fluentify.TestAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IParameterSymbol parameter = Substitute.For<IParameterSymbol>();
        _ = parameter.GetAttributes().Returns(ImmutableArray.Create(data));

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns(ImmutableArray<AttributeData>.Empty);
        _ = property.Name.Returns("Property");

        INamedTypeSymbol containingType = Substitute.For<INamedTypeSymbol>();
        _ = containingType.IsRecord.Returns(true);
        _ = containingType.InstanceConstructors.Returns(ImmutableArray.Create(Substitute.For<IMethodSymbol>()));
        _ = containingType.InstanceConstructors[0].Parameters.Returns(ImmutableArray.Create(parameter));
        _ = property.ContainingType.Returns(containingType);
        _ = parameter.Name.Returns("Property");

        // Act
        bool result = property.HasAttribute("Test");

        // Assert
        _ = result.Should().BeTrue();
    }

    [Fact]
    public void GivenPropertyParameterWithNonMatchingAttributeThenReturnsFalse()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString().Returns("Fluentify.OtherAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IParameterSymbol parameter = Substitute.For<IParameterSymbol>();
        _ = parameter.GetAttributes().Returns(ImmutableArray.Create(data));

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns(ImmutableArray<AttributeData>.Empty);
        _ = property.Name.Returns("Property");

        INamedTypeSymbol containingType = Substitute.For<INamedTypeSymbol>();
        _ = containingType.IsRecord.Returns(true);
        _ = containingType.InstanceConstructors.Returns(ImmutableArray.Create(Substitute.For<IMethodSymbol>()));
        _ = containingType.InstanceConstructors[0].Parameters.Returns(ImmutableArray.Create(parameter));
        _ = property.ContainingType.Returns(containingType);
        _ = parameter.Name.Returns("Property");

        // Act
        bool result = property.HasAttribute("Test");

        // Assert
        _ = result.Should().BeFalse();
    }

    [Fact]
    public void GivenPropertyParameterWithNoAttributesThenReturnsFalse()
    {
        // Arrange
        IParameterSymbol parameter = Substitute.For<IParameterSymbol>();
        _ = parameter.GetAttributes().Returns(ImmutableArray<AttributeData>.Empty);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns(ImmutableArray<AttributeData>.Empty);
        _ = property.Name.Returns("Property");

        INamedTypeSymbol containingType = Substitute.For<INamedTypeSymbol>();
        _ = containingType.IsRecord.Returns(true);
        _ = containingType.InstanceConstructors.Returns(ImmutableArray.Create(Substitute.For<IMethodSymbol>()));
        _ = containingType.InstanceConstructors[0].Parameters.Returns(ImmutableArray.Create(parameter));
        _ = property.ContainingType.Returns(containingType);
        _ = parameter.Name.Returns("Property");

        // Act
        bool result = property.HasAttribute("Test");

        // Assert
        _ = result.Should().BeFalse();
    }

    [Fact]
    public void GivenPropertyWithoutParameterThenReturnsFalse()
    {
        // Arrange
        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns(ImmutableArray<AttributeData>.Empty);

        INamedTypeSymbol containingType = Substitute.For<INamedTypeSymbol>();
        _ = containingType.IsRecord.Returns(false);
        _ = property.ContainingType.Returns(containingType);

        // Act
        bool result = property.HasAttribute("Test");

        // Assert
        _ = result.Should().BeFalse();
    }
}