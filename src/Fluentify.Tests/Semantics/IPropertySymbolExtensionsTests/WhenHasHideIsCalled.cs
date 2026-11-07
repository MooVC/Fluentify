namespace Fluentify.Semantics.IPropertySymbolExtensionsTests;

using Microsoft.CodeAnalysis;
using static Fluentify.HideAttributeGenerator;

public sealed class WhenHasHideIsCalled
{
    [Test]
    public void GivenPropertyWithHideAttributeThenReturnsTrue()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns($"Fluentify.{Name}Attribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([data]);

        // Act
        bool result = property.HasHide();

        // Assert
        result.ShouldBeTrue();
    }

    [Test]
    public void GivenPropertyWithNoAttributesThenReturnsFalse()
    {
        // Arrange
        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([]);

        // Act
        bool result = property.HasHide();

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void GivenPropertyWithoutHideAttributeThenReturnsFalse()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString().Returns("Fluentify.OtherAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([data]);

        // Act
        bool result = property.HasHide();

        // Assert
        result.ShouldBeFalse();
    }
}