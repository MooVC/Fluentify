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
        INamedTypeSymbol record = CreateNamedTypeSymbolWithAttributes(ImmutableArray<AttributeData>.Empty);

        // Act
        bool result = record.HasFluentify();

        // Assert
        _ = result.Should().BeFalse();
    }

    [Fact]
    public void GivenOtherAttributesThenReturnsFalse()
    {
        // Arrange
        AttributeData attribute = CreateAttributeData("SomeOtherAttribute");
        INamedTypeSymbol record = CreateNamedTypeSymbolWithAttributes(ImmutableArray.Create(attribute));

        // Act
        bool result = record.HasFluentify();

        // Assert
        _ = result.Should().BeFalse();
    }

    [Fact]
    public void GivenFluentifyAttributeThenReturnsTrue()
    {
        // Arrange
        AttributeData attribute = CreateAttributeData("Fluentify.FluentifyAttribute");
        INamedTypeSymbol record = CreateNamedTypeSymbolWithAttributes(ImmutableArray.Create(attribute));

        // Act
        bool result = record.HasFluentify();

        // Assert
        _ = result.Should().BeTrue();
    }

    [Fact]
    public void GivenMultipleAttributesIncludingFluentifyThenReturnsTrue()
    {
        // Arrange
        AttributeData other = CreateAttributeData("SomeOtherAttribute");
        AttributeData fluentify = CreateAttributeData("Fluentify.FluentifyAttribute");
        INamedTypeSymbol record = CreateNamedTypeSymbolWithAttributes(ImmutableArray.Create(other, fluentify));

        // Act
        bool result = record.HasFluentify();

        // Assert
        _ = result.Should().BeTrue();
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