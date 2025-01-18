namespace Fluentify.Semantics.ISymbolExtensionsTests;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

public sealed class WhenHasAttributeIsCalled
{
    [Fact]
    public void GivenSymbolWithMatchingAttributeThenReturnsTrue()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns("Fluentify.TestAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        ISymbol symbol = Substitute.For<ISymbol>();
        _ = symbol.GetAttributes().Returns([data]);

        // Act
        bool result = symbol.HasAttribute("Test");

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenSymbolWithNonMatchingAttributeThenReturnsFalse()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString().Returns("Fluentify.OtherAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        ISymbol symbol = Substitute.For<ISymbol>();
        _ = symbol.GetAttributes().Returns([data]);

        // Act
        bool result = symbol.HasAttribute("Test");

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenSymbolWithNoAttributesThenReturnsFalse()
    {
        // Arrange
        ISymbol symbol = Substitute.For<ISymbol>();
        _ = symbol.GetAttributes().Returns([]);

        // Act
        bool result = symbol.HasAttribute("Test");

        // Assert
        result.ShouldBeFalse();
    }
}