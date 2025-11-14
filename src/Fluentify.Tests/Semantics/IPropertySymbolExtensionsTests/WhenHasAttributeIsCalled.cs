namespace Fluentify.Semantics.IPropertySymbolExtensionsTests;

using Microsoft.CodeAnalysis;

public sealed class WhenHasAttributeIsCalled
{
    [Fact]
    public void GivenPropertyWithMatchingAttributeThenReturnsTrue()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns("Fluentify.TestAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([data]);

        // Act
        bool result = property.HasAttribute("Test");

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenPropertyWithNonMatchingAttributeThenReturnsFalse()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString().Returns("Fluentify.OtherAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([data]);

        // Act
        bool result = property.HasAttribute("Test");

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenPropertyWithNoAttributesThenReturnsFalse()
    {
        // Arrange
        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([]);

        // Act
        bool result = property.HasAttribute("Test");

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenPropertyParameterWithMatchingAttributeThenReturnsTrue()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Returns("Fluentify.TestAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IParameterSymbol parameter = Substitute.For<IParameterSymbol>();
        _ = parameter.GetAttributes().Returns([data]);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([]);
        _ = property.Name.Returns("Property");

        INamedTypeSymbol containingType = Substitute.For<INamedTypeSymbol>();
        _ = containingType.IsRecord.Returns(true);
        _ = containingType.InstanceConstructors.Returns([Substitute.For<IMethodSymbol>()]);
        _ = containingType.InstanceConstructors[0].Parameters.Returns([parameter]);
        _ = property.ContainingType.Returns(containingType);
        _ = parameter.Name.Returns("Property");

        // Act
        bool result = property.HasAttribute("Test");

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenPropertyParameterWithNonMatchingAttributeThenReturnsFalse()
    {
        // Arrange
        INamedTypeSymbol @class = Substitute.For<INamedTypeSymbol>();
        _ = @class.ToDisplayString().Returns("Fluentify.OtherAttribute");

        AttributeData data = Substitute.For<AttributeData>();
        _ = data.AttributeClass.Returns(@class);

        IParameterSymbol parameter = Substitute.For<IParameterSymbol>();
        _ = parameter.GetAttributes().Returns([data]);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([]);
        _ = property.Name.Returns("Property");

        INamedTypeSymbol containingType = Substitute.For<INamedTypeSymbol>();
        _ = containingType.IsRecord.Returns(true);
        _ = containingType.InstanceConstructors.Returns([Substitute.For<IMethodSymbol>()]);
        _ = containingType.InstanceConstructors[0].Parameters.Returns([parameter]);
        _ = property.ContainingType.Returns(containingType);
        _ = parameter.Name.Returns("Property");

        // Act
        bool result = property.HasAttribute("Test");

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenPropertyParameterWithNoAttributesThenReturnsFalse()
    {
        // Arrange
        IParameterSymbol parameter = Substitute.For<IParameterSymbol>();
        _ = parameter.GetAttributes().Returns([]);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([]);
        _ = property.Name.Returns("Property");

        INamedTypeSymbol containingType = Substitute.For<INamedTypeSymbol>();
        _ = containingType.IsRecord.Returns(true);
        _ = containingType.InstanceConstructors.Returns([Substitute.For<IMethodSymbol>()]);
        _ = containingType.InstanceConstructors[0].Parameters.Returns([parameter]);
        _ = property.ContainingType.Returns(containingType);
        _ = parameter.Name.Returns("Property");

        // Act
        bool result = property.HasAttribute("Test");

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenPropertyWithoutParameterThenReturnsFalse()
    {
        // Arrange
        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.GetAttributes().Returns([]);

        INamedTypeSymbol containingType = Substitute.For<INamedTypeSymbol>();
        _ = containingType.IsRecord.Returns(false);
        _ = property.ContainingType.Returns(containingType);

        // Act
        bool result = property.HasAttribute("Test");

        // Assert
        result.ShouldBeFalse();
    }
}