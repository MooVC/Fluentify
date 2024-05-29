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
        _ = @class.ToDisplayString().Returns("Fluentify.TestAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        ISymbol symbol = Substitute.For<ISymbol>();
        _ = symbol.GetAttributes().Returns(ImmutableArray.Create(data));

        // Act
        bool result = symbol.HasAttribute("Test");

        // Assert
        _ = result.Should().BeTrue();
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
        _ = symbol.GetAttributes().Returns(ImmutableArray.Create(data));

        // Act
        bool result = symbol.HasAttribute("Test");

        // Assert
        _ = result.Should().BeFalse();
    }

    [Fact]
    public void GivenSymbolWithNoAttributesThenReturnsFalse()
    {
        // Arrange
        ISymbol symbol = Substitute.For<ISymbol>();
        _ = symbol.GetAttributes().Returns(ImmutableArray<AttributeData>.Empty);

        // Act
        bool result = symbol.HasAttribute("Test");

        // Assert
        _ = result.Should().BeFalse();
    }
}