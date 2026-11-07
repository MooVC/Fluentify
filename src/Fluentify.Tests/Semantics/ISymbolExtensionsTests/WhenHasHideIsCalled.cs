namespace Fluentify.Semantics.ISymbolExtensionsTests;

using Microsoft.CodeAnalysis;
using static Fluentify.HideAttributeGenerator;

public sealed class WhenHasHideIsCalled
{
    [Test]
    public void GivenSymbolWithAttributeMissingClassThenReturnsFalse()
    {
        // Arrange
        AttributeData attribute = Substitute.For<AttributeData>();
        _ = attribute.AttributeClass.Returns((INamedTypeSymbol?)null);

        ISymbol symbol = Substitute.For<ISymbol>();
        _ = symbol.GetAttributes().Returns([attribute]);

        // Act
        bool result = symbol.HasHide();

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void GivenSymbolWithHideAttributeThenReturnsTrue()
    {
        // Arrange
        INamedTypeSymbol attributeClass = Substitute.For<INamedTypeSymbol>();
        _ = attributeClass.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns($"Fluentify.{Name}Attribute");

        AttributeData attribute = Substitute.For<AttributeData>();
        _ = attribute.AttributeClass.Returns(attributeClass);

        ISymbol symbol = Substitute.For<ISymbol>();
        _ = symbol.GetAttributes().Returns([attribute]);

        // Act
        bool result = symbol.HasHide();

        // Assert
        result.ShouldBeTrue();
    }

    [Test]
    public void GivenSymbolWithoutHideAttributeThenReturnsFalse()
    {
        // Arrange
        INamedTypeSymbol attributeClass = Substitute.For<INamedTypeSymbol>();
        _ = attributeClass.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns("Fluentify.OtherAttribute");

        AttributeData attribute = Substitute.For<AttributeData>();
        _ = attribute.AttributeClass.Returns(attributeClass);

        ISymbol symbol = Substitute.For<ISymbol>();
        _ = symbol.GetAttributes().Returns([attribute]);

        // Act
        bool result = symbol.HasHide();

        // Assert
        result.ShouldBeFalse();
    }
}