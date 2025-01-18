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
        _ = @class.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns($"Fluentify.{Name}Attribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([data]);

        // Act
        bool result = property.HasIgnore();

        // Assert
        result.ShouldBeTrue();
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
        _ = property.GetAttributes().Returns([data]);

        // Act
        bool result = property.HasIgnore();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenPropertyWithNoAttributesThenReturnsFalse()
    {
        // Arrange
        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([]);

        // Act
        bool result = property.HasIgnore();

        // Assert
        result.ShouldBeFalse();
    }
}