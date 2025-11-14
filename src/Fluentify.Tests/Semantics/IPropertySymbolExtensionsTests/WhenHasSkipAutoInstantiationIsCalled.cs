namespace Fluentify.Semantics.IPropertySymbolExtensionsTests;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using static Fluentify.SkipAutoInstantiationAttributeGenerator;

public sealed class WhenHasSkipAutoInstantiationIsCalled
{
    [Fact]
    public void GivenPropertyWithSkipAutoInstantiationAttributeThenReturnsTrue()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns($"Fluentify.{Name}Attribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([data]);

        ITypeSymbol type = Substitute.For<ITypeSymbol>();
        _ = type.GetAttributes().Returns([]);
        _ = property.Type.Returns(type);

        // Act
        bool result = property.HasSkipAutoInstantiation();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenTypeWithSkipAutoInstantiationAttributeThenReturnsTrue()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns($"Fluentify.{Name}Attribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        ITypeSymbol type = Substitute.For<ITypeSymbol>();
        _ = type.GetAttributes().Returns([data]);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.Type.Returns(type);
        _ = property.GetAttributes().Returns([]);

        // Act
        bool result = property.HasSkipAutoInstantiation();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenPropertyWithoutSkipAutoInstantiationAttributeThenReturnsFalse()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString().Returns("Fluentify.OtherAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([data]);

        ITypeSymbol type = Substitute.For<ITypeSymbol>();
        _ = type.GetAttributes().Returns([]);
        _ = property.Type.Returns(type);

        // Act
        bool result = property.HasSkipAutoInstantiation();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenPropertyWithNoAttributesThenReturnsFalse()
    {
        // Arrange
        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([]);

        ITypeSymbol type = Substitute.For<ITypeSymbol>();
        _ = type.GetAttributes().Returns([]);
        _ = property.Type.Returns(type);

        // Act
        bool result = property.HasSkipAutoInstantiation();

        // Assert
        result.ShouldBeFalse();
    }
}