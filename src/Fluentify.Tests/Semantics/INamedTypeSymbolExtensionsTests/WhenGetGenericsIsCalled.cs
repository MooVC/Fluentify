namespace Fluentify.Semantics.INamedTypeSymbolExtensionsTests;

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
        _ = symbol.TypeParameters.Returns([]);

        // Act
        IReadOnlyList<Generic> generics = symbol.GetGenerics();

        // Assert
        generics.ShouldBeEmpty();
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
        _ = parameter.ConstraintTypes.Returns([]);

        INamedTypeSymbol symbol = Substitute.For<INamedTypeSymbol>();
        _ = symbol.TypeParameters.Returns([parameter]);

        // Act
        IReadOnlyList<Generic> generics = symbol.GetGenerics();

        // Assert
        generics.Count.ShouldBe(1);
        Generic generic = generics[0];
        generic.Name.ShouldBe("T");
        generic.Constraints.ShouldBeSubsetOf(["class", "new()"]);
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
        _ = parameter.ConstraintTypes.Returns([]);

        INamedTypeSymbol symbol = Substitute.For<INamedTypeSymbol>();
        _ = symbol.TypeParameters.Returns([parameter]);

        // Act
        IReadOnlyList<Generic> generics = symbol.GetGenerics();

        // Assert
        generics.Count.ShouldBe(1);
        Generic generic = generics[0];
        generic.Name.ShouldBe("T");
        generic.Constraints.ShouldBeEmpty();
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
        _ = parameter1.ConstraintTypes.Returns([]);

        ITypeParameterSymbol parameter2 = Substitute.For<ITypeParameterSymbol>();
        _ = parameter2.Name.Returns("T2");
        _ = parameter2.HasReferenceTypeConstraint.Returns(false);
        _ = parameter2.HasValueTypeConstraint.Returns(true);
        _ = parameter2.HasConstructorConstraint.Returns(false);
        _ = parameter2.ConstraintTypes.Returns([]);

        INamedTypeSymbol symbol = Substitute.For<INamedTypeSymbol>();
        _ = symbol.TypeParameters.Returns([parameter1, parameter2]);

        // Act
        IReadOnlyList<Generic> generics = symbol.GetGenerics();

        // Assert
        generics.Count.ShouldBe(2);

        Generic generic1 = generics[0];
        generic1.Name.ShouldBe("T1");
        generic1.Constraints.ShouldBeSubsetOf(["class", "new()"]);

        Generic generic2 = generics[^1];
        generic2.Name.ShouldBe("T2");
        generic2.Constraints.ShouldContain("struct");
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
        _ = parameter.ConstraintTypes.Returns([constraint]);

        INamedTypeSymbol symbol = Substitute.For<INamedTypeSymbol>();
        _ = symbol.TypeParameters.Returns([parameter]);

        // Act
        IReadOnlyList<Generic> generics = symbol.GetGenerics();

        // Assert
        generics.Count.ShouldBe(1);
        Generic generic = generics[0];
        generic.Name.ShouldBe("T");
        generic.Constraints.ShouldBeSubsetOf(["global::System.IDisposable", "new()"]);
    }
}