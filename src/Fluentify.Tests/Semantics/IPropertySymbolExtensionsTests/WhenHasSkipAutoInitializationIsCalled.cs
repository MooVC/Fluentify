namespace Fluentify.Semantics.IPropertySymbolExtensionsTests;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using static Fluentify.SkipAutoInitializationAttributeGenerator;

public sealed class WhenHasSkipAutoInitializationIsCalled
{
    [Fact]
    public void GivenPropertyWithSkipAutoInitializationAttributeThenReturnsTrue()
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
        bool result = property.HasSkipAutoInitialization();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenTypeWithSkipAutoInitializationAttributeThenReturnsTrue()
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
        bool result = property.HasSkipAutoInitialization();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenTypeArgumentWithSkipAutoInitializationAttributeThenReturnsTrue()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns($"Fluentify.{Name}Attribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        ITypeSymbol elementType = Substitute.For<ITypeSymbol>();
        _ = elementType.GetAttributes().Returns([data]);

        INamedTypeSymbol collectionType = Substitute.For<INamedTypeSymbol>();
        _ = collectionType.GetAttributes().Returns([]);
        _ = collectionType.TypeArguments.Returns([elementType]);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.Type.Returns(collectionType);
        _ = property.GetAttributes().Returns([]);

        // Act
        bool result = property.HasSkipAutoInitialization();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenArrayElementWithSkipAutoInitializationAttributeThenReturnsTrue()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns($"Fluentify.{Name}Attribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        ITypeSymbol elementType = Substitute.For<ITypeSymbol>();
        _ = elementType.GetAttributes().Returns([data]);

        IArrayTypeSymbol arrayType = Substitute.For<IArrayTypeSymbol>();
        _ = arrayType.GetAttributes().Returns([]);
        _ = arrayType.ElementType.Returns(elementType);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.Type.Returns(arrayType);
        _ = property.GetAttributes().Returns([]);

        // Act
        bool result = property.HasSkipAutoInitialization();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenPropertyWithoutSkipAutoInitializationAttributeThenReturnsFalse()
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
        bool result = property.HasSkipAutoInitialization();

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
        bool result = property.HasSkipAutoInitialization();

        // Assert
        result.ShouldBeFalse();
    }
}