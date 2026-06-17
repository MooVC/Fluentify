namespace Fluentify.Semantics.IPropertySymbolExtensionsTests;

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public sealed class WhenThrowsWhenValueIsNullIsCalled
{
    private const string ParameterSampleTypeName = "Demo.ParameterSample";
    private const string SampleTypeName = "Demo.Sample";

    private static readonly Compilation _compilation = CreateCompilation();

    [Fact]
    public void GivenNullablePropertyThenReturnsFalse()
    {
        // Arrange
        IPropertySymbol property = GetProperty(SampleTypeName, "Nullable");

        // Act
        bool result = property.ThrowsWhenValueIsNull();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenNullablePropertyWithDisallowNullThenReturnsTrue()
    {
        // Arrange
        IPropertySymbol property = GetProperty(SampleTypeName, "DisallowsNull");

        // Act
        bool result = property.ThrowsWhenValueIsNull();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenNonNullablePropertyThenReturnsTrue()
    {
        // Arrange
        IPropertySymbol property = GetProperty(SampleTypeName, "NonNullable");

        // Act
        bool result = property.ThrowsWhenValueIsNull();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenNonNullableValueTypePropertyThenReturnsFalse()
    {
        // Arrange
        IPropertySymbol property = GetProperty(SampleTypeName, "Age");

        // Act
        bool result = property.ThrowsWhenValueIsNull();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenNonNullableValueTypePropertyWithoutReferencesThenReturnsFalse()
    {
        // Arrange
        SyntaxTree tree = CSharpSyntaxTree.ParseText("""
            namespace Demo
            {
                public sealed class Sample
                {
                    public int Age { get; set; }
                }
            }
            """);

        Compilation compilation = CSharpCompilation.Create("Fluentify.Tests", [tree]);
        INamedTypeSymbol type = compilation.GetTypeByMetadataName(SampleTypeName)!;
        IPropertySymbol property = type
            .GetMembers()
            .OfType<IPropertySymbol>()
            .Single(property => property.Name == "Age");

        // Act
        bool result = property.ThrowsWhenValueIsNull();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenNonNullablePropertyWithAllowNullThenReturnsFalse()
    {
        // Arrange
        IPropertySymbol property = GetProperty(SampleTypeName, "AllowsNull");

        // Act
        bool result = property.ThrowsWhenValueIsNull();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenNonNullablePropertyWithMaybeNullThenReturnsFalse()
    {
        // Arrange
        IPropertySymbol property = GetProperty(SampleTypeName, "MaybeNull");

        // Act
        bool result = property.ThrowsWhenValueIsNull();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenRecordParameterWithAllowNullThenReturnsFalse()
    {
        // Arrange
        IPropertySymbol property = GetProperty(ParameterSampleTypeName, "AllowsNull");

        // Act
        bool result = property.ThrowsWhenValueIsNull();

        // Assert
        result.ShouldBeFalse();
    }

    private static Compilation CreateCompilation()
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText("""
            #nullable enable

            namespace Demo
            {
                using System.Diagnostics.CodeAnalysis;

                public sealed record ParameterSample([param: AllowNull] string AllowsNull);

                public sealed class Sample
                {
                    public int Age { get; set; }

                    [AllowNull]
                    public string AllowsNull { get; set; } = string.Empty;

                    [DisallowNull]
                    public string? DisallowsNull { get; set; }

                    [MaybeNull]
                    public string MaybeNull { get; set; } = string.Empty;

                    public string NonNullable { get; set; } = string.Empty;

                    public string? Nullable { get; set; }
                }
            }
            """);

        return CSharpCompilation.Create(
            "Fluentify.Tests",
            [tree],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location), MetadataReference.CreateFromFile(typeof(AllowNullAttribute).Assembly.Location)],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }

    private static IPropertySymbol GetProperty(string typeName, string propertyName)
    {
        INamedTypeSymbol type = _compilation.GetTypeByMetadataName(typeName)!;

        return type
            .GetMembers()
            .OfType<IPropertySymbol>()
            .Single(property => property.Name == propertyName);
    }
}