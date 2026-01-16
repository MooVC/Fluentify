namespace Fluentify.Semantics.IPropertySymbolExtensionsTests;

using Microsoft.CodeAnalysis;

public sealed class WhenIsMutableIsCalled
{
    [Fact]
    public void GivenIndexerPropertyThenReturnsFalse()
    {
        // Arrange
        const Accessibility declaredAccessibility = Accessibility.Public;
        const Accessibility setMethodAccessibility = Accessibility.Public;

        IMethodSymbol setMethod = Substitute.For<IMethodSymbol>();
        _ = setMethod.DeclaredAccessibility.Returns(setMethodAccessibility);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.DeclaredAccessibility.Returns(declaredAccessibility);
        _ = property.IsImplicitlyDeclared.Returns(false);
        _ = property.IsIndexer.Returns(true);
        _ = property.IsStatic.Returns(false);
        _ = property.SetMethod.Returns(setMethod);

        // Act
        bool result = property.IsMutable();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenMutablePropertyThenReturnsTrue()
    {
        // Arrange
        const Accessibility declaredAccessibility = Accessibility.Public;
        const Accessibility setMethodAccessibility = Accessibility.Public;

        IMethodSymbol setMethod = Substitute.For<IMethodSymbol>();
        _ = setMethod.DeclaredAccessibility.Returns(setMethodAccessibility);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.DeclaredAccessibility.Returns(declaredAccessibility);
        _ = property.IsImplicitlyDeclared.Returns(false);
        _ = property.IsIndexer.Returns(false);
        _ = property.IsStatic.Returns(false);
        _ = property.SetMethod.Returns(setMethod);

        // Act
        bool result = property.IsMutable();

        // Assert
        result.ShouldBeTrue();
    }
}