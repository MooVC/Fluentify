namespace Fluentify.Semantics.IPropertySymbolExtensionsTests;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using static Fluentify.IgnoreAttributeGenerator;

public sealed class WhenHasIgnoreIsCalled
{
    [Fact]
    public void GivenPropertyWithIgnoreAttributeThenReturnsTrue()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString().Returns($"Fluentify.{Name}Attribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns(ImmutableArray.Create(data));

        // Act
        bool result = property.HasIgnore();

        // Assert
        _ = result.Should().BeTrue();
    }

    [Fact]
    public void GivenPropertyWithoutIgnoreAttributeThenReturnsFalse()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString().Returns("Fluentify.OtherAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns(ImmutableArray.Create(data));

        // Act
        bool result = property.HasIgnore();

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
        bool result = property.HasIgnore();

        // Assert
        _ = result.Should().BeFalse();
    }
}