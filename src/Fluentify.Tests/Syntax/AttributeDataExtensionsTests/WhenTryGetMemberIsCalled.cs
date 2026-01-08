namespace Fluentify.Syntax.AttributeDataExtensionsTests;

using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

public sealed class WhenTryGetMemberIsCalled
{
    private const string AttributeSource = """
        namespace Fluentify
        {
            using System;

            internal sealed class AutoInitiateWithAttribute : Attribute
            {
                public AutoInitiateWithAttribute()
                {
                }

                public AutoInitiateWithAttribute(string factory)
                {
                }
            }
        }
        """;

    [Fact]
    public void GivenStringLiteralArgumentThenMemberIsReturned()
    {
        // Arrange
        AttributeData attribute = GetAttributeFromSource("""
            using Fluentify;

            public sealed class Sample
            {
                [AutoInitiateWith("Create")]
                public int Value { get; }
            }
            """);

        // Act
        bool result = attribute.TryGetMember(out string member);

        // Assert
        result.ShouldBeTrue();
        member.ShouldBe("Create");
    }

    [Fact]
    public void GivenNameofArgumentThenMemberIsReturned()
    {
        // Arrange
        AttributeData attribute = GetAttributeFromSource("""
            using Fluentify;

            public sealed class Sample
            {
                [AutoInitiateWith(nameof(Create))]
                public int Value { get; }

                public static void Create()
                {
                }
            }
            """);

        // Act
        bool result = attribute.TryGetMember(out string member);

        // Assert
        result.ShouldBeTrue();
        member.ShouldBe("nameof(Create)");
    }

    [Fact]
    public void GivenAttributeWithoutArgumentsThenFalseIsReturned()
    {
        // Arrange
        AttributeData attribute = GetAttributeFromSource("""
            using Fluentify;

            public sealed class Sample
            {
                [AutoInitiateWith]
                public int Value { get; }
            }
            """);

        // Act
        bool result = attribute.TryGetMember(out string member);

        // Assert
        result.ShouldBeFalse();
        member.ShouldBe(string.Empty);
    }

    [Fact]
    public void GivenConstructorArgumentFromMetadataThenMemberIsReturned()
    {
        // Arrange
        AttributeData attribute = GetAttributeFromMetadata("""
            public sealed class Sample
            {
                [Fluentify.AutoInitiateWith("Build")]
                public int Value { get; }
            }
            """);

        // Act
        bool result = attribute.TryGetMember(out string member);

        // Assert
        result.ShouldBeTrue();
        member.ShouldBe("Build");
    }

    private static AttributeData GetAttributeFromSource(string source)
    {
        Compilation compilation = CreateCompilation($"{AttributeSource}\n\n{source}");
        INamedTypeSymbol type = compilation.GetTypeByMetadataName("Sample")!;
        IPropertySymbol property = type.GetMembers().OfType<IPropertySymbol>().Single();

        return property.GetAttributes().Single();
    }

    private static AttributeData GetAttributeFromMetadata(string source)
    {
        Compilation compilation = CreateCompilation($"{AttributeSource}\n\n{source}");

        using var stream = new MemoryStream();
        EmitResult emitResult = compilation.Emit(stream);
        emitResult.Success.ShouldBeTrue();

        PortableExecutableReference reference = MetadataReference.CreateFromImage(stream.ToArray());

        var metadataCompilation = CSharpCompilation.Create(
            "Metadata",
            references: GetReferences().Add(reference));

        INamedTypeSymbol type = metadataCompilation.GetTypeByMetadataName("Sample")!;
        IPropertySymbol property = type.GetMembers().OfType<IPropertySymbol>().Single();

        return property.GetAttributes().Single();
    }

    private static Compilation CreateCompilation(string source)
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText(source);

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