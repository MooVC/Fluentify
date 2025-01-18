namespace Fluentify.Semantics.INamedTypeSymbolExtensionsTests;

using System.Collections.Immutable;
using Fluentify.Semantics;
using Microsoft.CodeAnalysis;

public sealed class WhenHasFluentifyIsCalled
{
    [Fact]
    public void GivenNoAttributesThenReturnsFalse()
    {
        // Arrange
        INamedTypeSymbol record = CreateNamedTypeSymbolWithAttributes([]);

        // Act
        bool result = record.HasFluentify();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenOtherAttributesThenReturnsFalse()
    {
        // Arrange
        AttributeData attribute = CreateAttributeData("SomeOtherAttribute");
        INamedTypeSymbol record = CreateNamedTypeSymbolWithAttributes([attribute]);

        // Act
        bool result = record.HasFluentify();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenFluentifyAttributeThenReturnsTrue()
    {
        // Arrange
        AttributeData attribute = CreateAttributeData("global::Fluentify.FluentifyAttribute");
        INamedTypeSymbol record = CreateNamedTypeSymbolWithAttributes([attribute]);

        // Act
        bool result = record.HasFluentify();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenMultipleAttributesIncludingFluentifyThenReturnsTrue()
    {
        // Arrange
        AttributeData other = CreateAttributeData("SomeOtherAttribute");
        AttributeData fluentify = CreateAttributeData("global::Fluentify.FluentifyAttribute");
        INamedTypeSymbol record = CreateNamedTypeSymbolWithAttributes([other, fluentify]);

        // Act
        bool result = record.HasFluentify();

        // Assert
        result.ShouldBeTrue();
    }

    private static INamedTypeSymbol CreateNamedTypeSymbolWithAttributes(ImmutableArray<AttributeData> attributes)
    {
        INamedTypeSymbol symbol = Substitute.For<INamedTypeSymbol>();
        _ = symbol.GetAttributes().Returns(attributes);

        return symbol;
    }

    private static AttributeData CreateAttributeData(string attributeName)
    {
        AttributeData data = Substitute.For<AttributeData>();
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString(Arg.Any<SymbolDisplayFormat>()).Returns(attributeName);
        _ = data.AttributeClass.Returns(@class);

        return data;
    }
}