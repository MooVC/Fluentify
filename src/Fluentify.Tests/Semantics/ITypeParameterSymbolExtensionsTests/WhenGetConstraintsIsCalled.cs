namespace Fluentify.Semantics.ITypeParameterSymbolExtensionsTests;

using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

public sealed class WhenGetConstraintsIsCalled
{
    [Test]
    public void GivenConstraintTypesThenReturnsFullyQualifiedTypeNames()
    {
        // Arrange
        INamedTypeSymbol symbol = Substitute.For<INamedTypeSymbol>();

        _ = symbol
            .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
            .Returns("global::Namespace.TypeName");

        var constraints = ImmutableArray.Create<ITypeSymbol>(symbol);
        ITypeParameterSymbol parameter = Substitute.For<ITypeParameterSymbol>();
        _ = parameter.HasReferenceTypeConstraint.Returns(false);
        _ = parameter.HasValueTypeConstraint.Returns(false);
        _ = parameter.HasNotNullConstraint.Returns(false);
        _ = parameter.ConstraintTypes.Returns(constraints);
        _ = parameter.HasConstructorConstraint.Returns(false);

        // Act
        IReadOnlyList<string> result = parameter.GetConstraints();

        // Assert
        result.ShouldHaveSingleItem()
            .ShouldBe("global::Namespace.TypeName");
    }

    [Test]
    public void GivenConstructorConstraintThenReturnsNewConstraint()
    {
        // Arrange
        ITypeParameterSymbol parameter = Substitute.For<ITypeParameterSymbol>();
        _ = parameter.HasReferenceTypeConstraint.Returns(false);
        _ = parameter.HasValueTypeConstraint.Returns(false);
        _ = parameter.HasNotNullConstraint.Returns(false);
        _ = parameter.ConstraintTypes.Returns([]);
        _ = parameter.HasConstructorConstraint.Returns(true);

        // Act
        IReadOnlyList<string> result = parameter.GetConstraints();

        // Assert
        result.ShouldHaveSingleItem()
            .ShouldBe("new()");
    }

    [Test]
    public void GivenMultipleConstraintsThenReturnsAllConstraints()
    {
        // Arrange
        INamedTypeSymbol symbol = Substitute.For<INamedTypeSymbol>();

        _ = symbol
            .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
            .Returns("global::Namespace.TypeName");

        var constraints = ImmutableArray.Create<ITypeSymbol>(symbol);
        ITypeParameterSymbol parameter = Substitute.For<ITypeParameterSymbol>();
        _ = parameter.HasReferenceTypeConstraint.Returns(true);
        _ = parameter.HasValueTypeConstraint.Returns(false);
        _ = parameter.HasNotNullConstraint.Returns(true);
        _ = parameter.ConstraintTypes.Returns(constraints);
        _ = parameter.HasConstructorConstraint.Returns(true);

        // Act
        IReadOnlyList<string> result = parameter.GetConstraints();

        // Assert
        result.ShouldBeSubsetOf(["class", "notnull", "global::Namespace.TypeName", "new()"]);
    }

    [Test]
    public void GivenNoConstraintsThenReturnsEmptyList()
    {
        // Arrange
        ITypeParameterSymbol parameter = Substitute.For<ITypeParameterSymbol>();
        _ = parameter.HasReferenceTypeConstraint.Returns(false);
        _ = parameter.HasValueTypeConstraint.Returns(false);
        _ = parameter.HasNotNullConstraint.Returns(false);
        _ = parameter.ConstraintTypes.Returns([]);
        _ = parameter.HasConstructorConstraint.Returns(false);

        // Act
        IReadOnlyList<string> result = parameter.GetConstraints();

        // Assert
        result.ShouldBeEmpty();
    }

    [Test]
    public void GivenNotNullConstraintThenReturnsNotNullConstraint()
    {
        // Arrange
        ITypeParameterSymbol parameter = Substitute.For<ITypeParameterSymbol>();
        _ = parameter.HasReferenceTypeConstraint.Returns(false);
        _ = parameter.HasValueTypeConstraint.Returns(false);
        _ = parameter.HasNotNullConstraint.Returns(true);
        _ = parameter.ConstraintTypes.Returns([]);
        _ = parameter.HasConstructorConstraint.Returns(false);

        // Act
        IReadOnlyList<string> result = parameter.GetConstraints();

        // Assert
        result.ShouldHaveSingleItem()
            .ShouldBe("notnull");
    }

    [Test]
    public void GivenReferenceTypeConstraintThenReturnsClassConstraint()
    {
        // Arrange
        ITypeParameterSymbol parameter = Substitute.For<ITypeParameterSymbol>();
        _ = parameter.HasReferenceTypeConstraint.Returns(true);
        _ = parameter.HasValueTypeConstraint.Returns(false);
        _ = parameter.HasNotNullConstraint.Returns(false);
        _ = parameter.ConstraintTypes.Returns([]);
        _ = parameter.HasConstructorConstraint.Returns(false);

        // Act
        IReadOnlyList<string> result = parameter.GetConstraints();

        // Assert
        result.ShouldHaveSingleItem()
            .ShouldBe("class");
    }

    [Test]
    public void GivenValueTypeConstraintThenReturnsStructConstraint()
    {
        // Arrange
        ITypeParameterSymbol parameter = Substitute.For<ITypeParameterSymbol>();
        _ = parameter.HasReferenceTypeConstraint.Returns(false);
        _ = parameter.HasValueTypeConstraint.Returns(true);
        _ = parameter.HasNotNullConstraint.Returns(false);
        _ = parameter.ConstraintTypes.Returns([]);
        _ = parameter.HasConstructorConstraint.Returns(false);

        // Act
        IReadOnlyList<string> result = parameter.GetConstraints();

        // Assert
        result.ShouldHaveSingleItem()
            .ShouldBe("struct");
    }
}