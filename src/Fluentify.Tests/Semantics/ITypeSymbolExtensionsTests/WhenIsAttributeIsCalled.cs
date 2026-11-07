namespace Fluentify.Semantics.ITypeSymbolExtensionsTests;

using Microsoft.CodeAnalysis;

public sealed class WhenIsAttributeIsCalled
{
    [Test]
    public void GivenAMatchingSymbolThenTrueIsReturned()
    {
        // Arrange
        ITypeSymbol attribute = Substitute.For<ITypeSymbol>();
        _ = attribute.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns("Fluentify.TestAttribute");

        // Act
        bool result = attribute.IsAttribute("Test");

        // Assert
        result.ShouldBeTrue();
    }

    [Test]
    public void GivenANonMatchingSymbolThenFalseIsReturned()
    {
        // Arrange
        ITypeSymbol attribute = Substitute.For<ITypeSymbol>();
        _ = attribute.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns("Fluentify.OtherAttribute");

        // Act
        bool result = attribute.IsAttribute("Test");

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void GivenASymbolWithAMatchingNameWhenTheNamespaceIsDifferentThenFalseIsReturned()
    {
        // Arrange
        ITypeSymbol attribute = Substitute.For<ITypeSymbol>();
        _ = attribute.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns("Fluentify.Tests.TestAttribute");

        // Act
        bool result = attribute.IsAttribute("Test");

        // Assert
        result.ShouldBeFalse();
    }
}