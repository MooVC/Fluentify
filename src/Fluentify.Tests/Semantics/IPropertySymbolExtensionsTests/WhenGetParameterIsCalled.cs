namespace Fluentify.Semantics.IPropertySymbolExtensionsTests;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

public sealed class WhenGetParameterIsCalled
{
    [Fact]
    public void GivenPropertyInRecordWithMatchingParameterThenReturnsParameter()
    {
        // Arrange
        IParameterSymbol parameter = Substitute.For<IParameterSymbol>();
        _ = parameter.Name.Returns("Property");

        IMethodSymbol constructor = Substitute.For<IMethodSymbol>();
        _ = constructor.Parameters.Returns(ImmutableArray.Create(parameter));

        INamedTypeSymbol containingType = Substitute.For<INamedTypeSymbol>();
        _ = containingType.IsRecord.Returns(true);
        _ = containingType.InstanceConstructors.Returns(ImmutableArray.Create(constructor));

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.Name.Returns("Property");
        _ = property.ContainingType.Returns(containingType);

        // Act
        IParameterSymbol? result = property.GetParameter();

        // Assert
        _ = result.Should().Be(parameter);
    }

    [Fact]
    public void GivenPropertyInRecordWithoutMatchingParameterThenReturnsNull()
    {
        // Arrange
        IParameterSymbol parameter = Substitute.For<IParameterSymbol>();
        _ = parameter.Name.Returns("DifferentProperty");

        IMethodSymbol constructor = Substitute.For<IMethodSymbol>();
        _ = constructor.Parameters.Returns(ImmutableArray.Create(parameter));

        INamedTypeSymbol containingType = Substitute.For<INamedTypeSymbol>();
        _ = containingType.IsRecord.Returns(true);
        _ = containingType.InstanceConstructors.Returns(ImmutableArray.Create(constructor));

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.Name.Returns("Property");
        _ = property.ContainingType.Returns(containingType);

        // Act
        IParameterSymbol? result = property.GetParameter();

        // Assert
        _ = result.Should().BeNull();
    }

    [Fact]
    public void GivenPropertyNotInRecordThenReturnsNull()
    {
        // Arrange
        INamedTypeSymbol containingType = Substitute.For<INamedTypeSymbol>();
        _ = containingType.IsRecord.Returns(false);

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.ContainingType.Returns(containingType);

        // Act
        IParameterSymbol? result = property.GetParameter();

        // Assert
        _ = result.Should().BeNull();
    }

    [Fact]
    public void GivenPropertyInRecordWithMultipleConstructorsThenReturnsMatchingParameter()
    {
        // Arrange
        IParameterSymbol parameter1 = Substitute.For<IParameterSymbol>();
        _ = parameter1.Name.Returns("Property");

        IParameterSymbol parameter2 = Substitute.For<IParameterSymbol>();
        _ = parameter2.Name.Returns("OtherProperty");

        IMethodSymbol constructor1 = Substitute.For<IMethodSymbol>();
        _ = constructor1.Parameters.Returns(ImmutableArray.Create(parameter2));

        IMethodSymbol constructor2 = Substitute.For<IMethodSymbol>();
        _ = constructor2.Parameters.Returns(ImmutableArray.Create(parameter1));

        INamedTypeSymbol containingType = Substitute.For<INamedTypeSymbol>();
        _ = containingType.IsRecord.Returns(true);
        _ = containingType.InstanceConstructors.Returns(ImmutableArray.Create(constructor1, constructor2));

        IPropertySymbol property = Substitute.For<IPropertySymbol>();
        _ = property.Name.Returns("Property");
        _ = property.ContainingType.Returns(containingType);

        // Act
        IParameterSymbol? result = property.GetParameter();

        // Assert
        _ = result.Should().Be(parameter1);
    }
}