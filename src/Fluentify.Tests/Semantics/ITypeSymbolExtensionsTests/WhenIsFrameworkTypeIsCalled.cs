namespace Fluentify.Semantics.ITypeSymbolExtensionsTests;

using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public sealed class WhenIsFrameworkTypeIsCalled
{
    private static readonly Compilation _compilation = CreateCompilation();

    [Fact]
    public void GivenStringThenReturnsTrue()
    {
        // Arrange
        ITypeSymbol type = GetPropertyType("Name");

        // Act
        bool result = type.IsFrameworkType();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenAnnotatedNullablePrimitiveThenReturnsTrue()
    {
        // Arrange
        ITypeSymbol type = GetPropertyType("Age");

        // Act
        bool result = type.IsFrameworkType();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenAnnotatedNullableCustomTypeThenReturnsFalse()
    {
        // Arrange
        ITypeSymbol type = GetPropertyType("NullableDependency");

        // Act
        bool result = type.IsFrameworkType();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenCustomTypeThenReturnsFalse()
    {
        // Arrange
        ITypeSymbol type = GetPropertyType("Dependency");

        // Act
        bool result = type.IsFrameworkType();

        // Assert
        result.ShouldBeFalse();
    }

    private static ITypeSymbol GetPropertyType(string name)
    {
        INamedTypeSymbol type = _compilation.GetTypeByMetadataName("Demo.Sample")!;

        return type.GetMembers()
            .OfType<IPropertySymbol>()
            .Single(property => property.Name == name)
            .Type;
    }

    private static Compilation CreateCompilation()
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText("""
            #nullable enable
            namespace Demo
            {
                public sealed class Dependency
                {
                }

                public sealed class Sample
                {
                    public int? Age { get; } = 42;

                    public Dependency Dependency { get; } = new Dependency();

                    public Dependency? NullableDependency { get; } = new Dependency();

                    public string Name { get; } = string.Empty;
                }
            }
            """);

        return CSharpCompilation.Create(
            "Fluentify.Tests",
            [tree],
            GetReferences(),
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }

    private static ImmutableArray<MetadataReference> GetReferences()
    {
        return
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
        ];
    }
}