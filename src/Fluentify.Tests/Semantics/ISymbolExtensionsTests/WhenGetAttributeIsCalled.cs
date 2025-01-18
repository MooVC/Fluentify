namespace Fluentify.Semantics.ISymbolExtensionsTests;

using Microsoft.CodeAnalysis;

public sealed class WhenGetAttributeIsCalled
{
    [Fact]
    public void GivenSymbolWithMatchingAttributeThenReturnsAttributeData()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns("Fluentify.TestAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        ISymbol symbol = Substitute.For<ISymbol>();
        _ = symbol.GetAttributes().Returns([data]);

        // Act
        AttributeData? result = symbol.GetAttribute("Test");

        // Assert
        result.ShouldBe(data);
    }

    [Fact]
    public void GivenSymbolWithNonMatchingAttributeThenReturnsNull()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString().Returns("Fluentify.OtherAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        ISymbol symbol = Substitute.For<ISymbol>();
        _ = symbol.GetAttributes().Returns([data]);

        // Act
        AttributeData? result = symbol.GetAttribute("Test");

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void GivenSymbolWithNoAttributesThenReturnsNull()
    {
        // Arrange
        ISymbol symbol = Substitute.For<ISymbol>();
        _ = symbol.GetAttributes().Returns([]);

        // Act
        AttributeData? result = symbol.GetAttribute("Test");

        // Assert
        result.ShouldBeNull();
    }
}