namespace Fluentify.Semantics.ITypeSymbolExtensionsTests;

using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public sealed class WhenTryGetInitializationIsCalled
{
    private const string MissingTypeName = "Demo.Missing";
    private const string SkippedTypeName = "Demo.Skipped";
    private const string WithInitializationTypeName = "Demo.WithInitialization";

    private static readonly Compilation _compilation = CreateCompilation();

    [Fact]
    public void GivenTypeWithAutoInitiateWithAttributeThenInitializationIsReturned()
    {
        // Arrange
        INamedTypeSymbol type = _compilation.GetTypeByMetadataName(WithInitializationTypeName)!;

        // Act
        bool result = type.TryGetInitialization(out string initialization);

        // Assert
        result.ShouldBeTrue();
        initialization.ShouldBe("global::Demo.WithInitialization.Create()");
    }

    [Fact]
    public void GivenTypeWithSkipAutoInitializationThenFalseIsReturned()
    {
        // Arrange
        INamedTypeSymbol type = _compilation.GetTypeByMetadataName(SkippedTypeName)!;

        // Act
        bool result = type.TryGetInitialization(out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    [Fact]
    public void GivenTypeWithMissingMemberThenFalseIsReturned()
    {
        // Arrange
        INamedTypeSymbol type = _compilation.GetTypeByMetadataName(MissingTypeName)!;

        // Act
        bool result = type.TryGetInitialization(out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    private static Compilation CreateCompilation()
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText("""
            namespace Fluentify
            {
                using System;

                internal sealed class AutoInitiateWithAttribute : Attribute
                {
                    public AutoInitiateWithAttribute(string factory)
                    {
                    }
                }

                internal sealed class SkipAutoInitializationAttribute : Attribute
                {
                }
            }

            namespace Demo
            {
                [Fluentify.AutoInitiateWith(nameof(Create))]
                public sealed class WithInitialization
                {
                    public static WithInitialization Create() => new WithInitialization();
                }

                [Fluentify.SkipAutoInitialization]
                [Fluentify.AutoInitiateWith(nameof(Create))]
                public sealed class Skipped
                {
                    public static Skipped Create() => new Skipped();
                }

                [Fluentify.AutoInitiateWith("Missing")]
                public sealed class Missing
                {
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