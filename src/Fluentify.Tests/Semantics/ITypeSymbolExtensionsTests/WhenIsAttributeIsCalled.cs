namespace Fluentify.Semantics.ITypeSymbolExtensionsTests;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

public sealed class WhenIsAttributeIsCalled
{
    [Fact]
    public void GivenAMatchingSymbolThenTrueIsReturned()
    {
        // Arrange
        ITypeSymbol attribute = Substitute.For<ITypeSymbol>();
        _ = attribute.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns("Fluentify.TestAttribute");

        // Act
        bool result = attribute.IsAttribute("Test");

        // Assert
        _ = result.Should().BeTrue();
    }

    [Fact]
    public void GivenANonMatchingSymbolThenFalseIsReturned()
    {
        // Arrange
        ITypeSymbol attribute = Substitute.For<ITypeSymbol>();
        _ = attribute.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns("Fluentify.OtherAttribute");

        // Act
        bool result = attribute.IsAttribute("Test");

        // Assert
        _ = result.Should().BeFalse();
    }

    [Fact]
    public void GivenASymbolWithAMatchingNameWhenTheNamespaceIsDifferentThenFalseIsReturned()
    {
        // Arrange
        ITypeSymbol attribute = Substitute.For<ITypeSymbol>();
        _ = attribute.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns("Fluentify.Tests.TestAttribute");

        // Act
        bool result = attribute.IsAttribute("Test");

        // Assert
        _ = result.Should().BeFalse();
    }
}