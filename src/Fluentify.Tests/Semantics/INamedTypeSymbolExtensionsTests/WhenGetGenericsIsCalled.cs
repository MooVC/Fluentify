namespace Fluentify.Semantics.INamedTypeSymbolExtensionsTests;

using System.Collections.Immutable;
using Fluentify.Model;
using Fluentify.Semantics;
using Microsoft.CodeAnalysis;

public sealed class WhenGetGenericsIsCalled
{
    [Fact]
    public void GivenNoTypeParametersThenReturnsEmptyList()
    {
        // Arrange
        INamedTypeSymbol symbol = Substitute.For<INamedTypeSymbol>();
        _ = symbol.TypeParameters.Returns(ImmutableArray<ITypeParameterSymbol>.Empty);

        // Act
        IReadOnlyList<Generic> generics = symbol.GetGenerics();

        // Assert
        _ = generics.Should().BeEmpty();
    }

    [Fact]
    public void GivenTypeParametersWithConstraintsThenReturnsGenericsWithConstraints()
    {
        // Arrange
        ITypeParameterSymbol parameter = Substitute.For<ITypeParameterSymbol>();
        _ = parameter.Name.Returns("T");
        _ = parameter.HasReferenceTypeConstraint.Returns(true);
        _ = parameter.HasValueTypeConstraint.Returns(false);
        _ = parameter.HasConstructorConstraint.Returns(true);
        _ = parameter.ConstraintTypes.Returns(ImmutableArray<ITypeSymbol>.Empty);

        INamedTypeSymbol symbol = Substitute.For<INamedTypeSymbol>();
        _ = symbol.TypeParameters.Returns(ImmutableArray.Create(parameter));

        // Act
        IReadOnlyList<Generic> generics = symbol.GetGenerics();

        // Assert
        _ = generics.Should().HaveCount(1);
        Generic generic = generics[0];
        _ = generic.Name.Should().Be("T");
        _ = generic.Constraints.Should().ContainInOrder("class", "new()");
    }

    [Fact]
    public void GivenTypeParametersWithoutConstraintsThenReturnsGenericsWithoutConstraints()
    {
        // Arrange
        ITypeParameterSymbol parameter = Substitute.For<ITypeParameterSymbol>();
        _ = parameter.Name.Returns("T");
        _ = parameter.HasReferenceTypeConstraint.Returns(false);
        _ = parameter.HasValueTypeConstraint.Returns(false);
        _ = parameter.HasConstructorConstraint.Returns(false);
        _ = parameter.ConstraintTypes.Returns(ImmutableArray<ITypeSymbol>.Empty);

        INamedTypeSymbol symbol = Substitute.For<INamedTypeSymbol>();
        _ = symbol.TypeParameters.Returns(ImmutableArray.Create(parameter));

        // Act
        IReadOnlyList<Generic> generics = symbol.GetGenerics();

        // Assert
        _ = generics.Should().HaveCount(1);
        Generic generic = generics[0];
        _ = generic.Name.Should().Be("T");
        _ = generic.Constraints.Should().BeEmpty();
    }

    [Fact]
    public void GivenMultipleTypeParametersThenReturnsGenericsForAll()
    {
        // Arrange
        ITypeParameterSymbol parameter1 = Substitute.For<ITypeParameterSymbol>();
        _ = parameter1.Name.Returns("T1");
        _ = parameter1.HasReferenceTypeConstraint.Returns(true);
        _ = parameter1.HasValueTypeConstraint.Returns(false);
        _ = parameter1.HasConstructorConstraint.Returns(true);
        _ = parameter1.ConstraintTypes.Returns(ImmutableArray<ITypeSymbol>.Empty);

        ITypeParameterSymbol parameter2 = Substitute.For<ITypeParameterSymbol>();
        _ = parameter2.Name.Returns("T2");
        _ = parameter2.HasReferenceTypeConstraint.Returns(false);
        _ = parameter2.HasValueTypeConstraint.Returns(true);
        _ = parameter2.HasConstructorConstraint.Returns(false);
        _ = parameter2.ConstraintTypes.Returns(ImmutableArray<ITypeSymbol>.Empty);

        INamedTypeSymbol symbol = Substitute.For<INamedTypeSymbol>();
        _ = symbol.TypeParameters.Returns(ImmutableArray.Create(parameter1, parameter2));

        // Act
        IReadOnlyList<Generic> generics = symbol.GetGenerics();

        // Assert
        _ = generics.Should().HaveCount(2);

        Generic generic1 = generics[0];
        _ = generic1.Name.Should().Be("T1");
        _ = generic1.Constraints.Should().ContainInOrder("class", "new()");

        Generic generic2 = generics[^1];
        _ = generic2.Name.Should().Be("T2");
        _ = generic2.Constraints.Should().ContainInOrder("struct");
    }

    [Fact]
    public void GivenTypeParameterWithMultipleConstraintsThenReturnsAllConstraints()
    {
        // Arrange
        INamedTypeSymbol constraint = Substitute.For<INamedTypeSymbol>();
        _ = constraint.ToDisplayString(Arg.Any<SymbolDisplayFormat>()).Returns("global::System.IDisposable");

        ITypeParameterSymbol parameter = Substitute.For<ITypeParameterSymbol>();
        _ = parameter.Name.Returns("T");
        _ = parameter.HasReferenceTypeConstraint.Returns(false);
        _ = parameter.HasValueTypeConstraint.Returns(false);
        _ = parameter.HasConstructorConstraint.Returns(true);
        _ = parameter.ConstraintTypes.Returns(ImmutableArray.Create<ITypeSymbol>(constraint));

        INamedTypeSymbol symbol = Substitute.For<INamedTypeSymbol>();
        _ = symbol.TypeParameters.Returns(ImmutableArray.Create(parameter));

        // Act
        IReadOnlyList<Generic> generics = symbol.GetGenerics();

        // Assert
        _ = generics.Should().HaveCount(1);
        Generic generic = generics[0];
        _ = generic.Name.Should().Be("T");
        _ = generic.Constraints.Should().ContainInOrder("global::System.IDisposable", "new()");
    }
}