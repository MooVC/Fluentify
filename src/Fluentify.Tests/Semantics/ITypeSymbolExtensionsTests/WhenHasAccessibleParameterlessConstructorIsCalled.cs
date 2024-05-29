namespace Fluentify.Semantics.ITypeSymbolExtensionsTests;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

public sealed class WhenHasAccessibleParameterlessConstructorIsCalled
{
    [Fact]
    public void GivenTypeWithPublicParameterlessConstructorThenReturnsTrue()
    {
        // Arrange
        IMethodSymbol constructor = Substitute.For<IMethodSymbol>();
        _ = constructor.MethodKind.Returns(MethodKind.Constructor);
        _ = constructor.Parameters.Returns(ImmutableArray<IParameterSymbol>.Empty);
        _ = constructor.DeclaredAccessibility.Returns(Accessibility.Public);

        ITypeSymbol type = Substitute.For<ITypeSymbol>();
        _ = type.GetMembers().Returns(ImmutableArray.Create<ISymbol>(constructor));

        // Act
        bool result = type.HasAccessibleParameterlessConstructor(true);

        // Assert
        _ = result.Should().BeTrue();
    }

    [Fact]
    public void GivenTypeWithInternalParameterlessConstructorAndInternalAccessThenReturnsTrue()
    {
        // Arrange
        IMethodSymbol constructor = Substitute.For<IMethodSymbol>();
        _ = constructor.MethodKind.Returns(MethodKind.Constructor);
        _ = constructor.Parameters.Returns(ImmutableArray<IParameterSymbol>.Empty);
        _ = constructor.DeclaredAccessibility.Returns(Accessibility.Internal);

        ITypeSymbol type = Substitute.For<ITypeSymbol>();
        _ = type.GetMembers().Returns(ImmutableArray.Create<ISymbol>(constructor));

        // Act
        bool result = type.HasAccessibleParameterlessConstructor(true);

        // Assert
        _ = result.Should().BeTrue();
    }

    [Fact]
    public void GivenTypeWithPrivateParameterlessConstructorThenReturnsFalse()
    {
        // Arrange
        IMethodSymbol constructor = Substitute.For<IMethodSymbol>();
        _ = constructor.MethodKind.Returns(MethodKind.Constructor);
        _ = constructor.Parameters.Returns(ImmutableArray<IParameterSymbol>.Empty);
        _ = constructor.DeclaredAccessibility.Returns(Accessibility.Private);

        ITypeSymbol type = Substitute.For<ITypeSymbol>();
        _ = type.GetMembers().Returns(ImmutableArray.Create<ISymbol>(constructor));

        // Act
        bool result = type.HasAccessibleParameterlessConstructor(true);

        // Assert
        _ = result.Should().BeFalse();
    }

    [Fact]
    public void GivenTypeWithNoParameterlessConstructorThenReturnsFalse()
    {
        // Arrange
        IMethodSymbol constructor = Substitute.For<IMethodSymbol>();
        _ = constructor.MethodKind.Returns(MethodKind.Constructor);
        _ = constructor.Parameters.Returns(ImmutableArray.Create(Substitute.For<IParameterSymbol>()));

        ITypeSymbol type = Substitute.For<ITypeSymbol>();
        _ = type.GetMembers().Returns(ImmutableArray.Create<ISymbol>(constructor));

        // Act
        bool result = type.HasAccessibleParameterlessConstructor(true);

        // Assert
        _ = result.Should().BeFalse();
    }

    [Fact]
    public void GivenTypeWithNoConstructorsThenReturnsTrue()
    {
        // Arrange
        ITypeSymbol type = Substitute.For<ITypeSymbol>();
        _ = type.GetMembers().Returns(ImmutableArray<ISymbol>.Empty);

        // Act
        bool result = type.HasAccessibleParameterlessConstructor(true);

        // Assert
        _ = result.Should().BeTrue();
    }
}