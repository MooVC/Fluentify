namespace Fluentify.Semantics.IPropertySymbolExtensionsTests;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Fluentify.Model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public sealed class WhenGetKindIsCalled
{
    private const string SampleTypeName = "Demo.Sample";

    private static readonly Compilation Compilation = CreateCompilation();

    [Fact]
    public void GivenArrayPropertyThenKindIsArray()
    {
        // Arrange
        var property = GetProperty("ArrayItems");

        // Act
        var kind = property.GetKind(Compilation, CancellationToken.None);

        // Assert
        kind.Pattern.ShouldBe(Pattern.Array);
        kind.Member.Name.ShouldBe("global::Demo.Element");
    }

    [Fact]
    public void GivenCollectionPropertyThenKindIsCollection()
    {
        // Arrange
        var property = GetProperty("CollectionItems");

        // Act
        var kind = property.GetKind(Compilation, CancellationToken.None);

        // Assert
        kind.Pattern.ShouldBe(Pattern.Collection);
        kind.Member.Name.ShouldBe("global::Demo.Element");
    }

    [Fact]
    public void GivenImmutableArrayPropertyThenKindIsEnumerable()
    {
        // Arrange
        var property = GetProperty("ImmutableItems");

        // Act
        var kind = property.GetKind(Compilation, CancellationToken.None);

        // Assert
        kind.Pattern.ShouldBe(Pattern.Enumerable);
        kind.Member.Name.ShouldBe("global::Demo.Element");
    }

    [Fact]
    public void GivenNullablePropertyThenInitializationUsesNonNullableType()
    {
        // Arrange
        var property = GetProperty("NullableElement");

        // Act
        var kind = property.GetKind(Compilation, CancellationToken.None);

        // Assert
        kind.Type.IsNullable.ShouldBeTrue();
        kind.Type.Initialization.ShouldBe("new global::Demo.Element()");
    }

    [Fact]
    public void GivenSkipAutoInstantiationThenTypeIsNotBuildable()
    {
        // Arrange
        var property = GetProperty("SkipAutoInstantiationElement");

        // Act
        var kind = property.GetKind(Compilation, CancellationToken.None);

        // Assert
        kind.Type.IsBuildable.ShouldBeFalse();
    }

    private static IPropertySymbol GetProperty(string propertyName)
    {
        var type = Compilation.GetTypeByMetadataName(SampleTypeName)!;

        return type
            .GetMembers()
            .OfType<IPropertySymbol>()
            .Single(property => property.Name == propertyName);
    }

    private static Compilation CreateCompilation()
    {
        var tree = CSharpSyntaxTree.ParseText(
            """
            #nullable enable

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

                internal sealed class SkipAutoInstantiationAttribute : Attribute
                {
                }
            }

            namespace Demo
            {
                using System.Collections.Generic;
                using System.Collections.Immutable;

                public sealed class Element
                {
                    public Element()
                    {
                    }
                }

                public sealed class BuildableCollection : List<Element>
                {
                    public BuildableCollection()
                    {
                    }
                }

                public sealed class Sample
                {
                    public Element[] ArrayItems { get; } = new Element[0];

                    public BuildableCollection CollectionItems { get; } = new BuildableCollection();

                    public ImmutableArray<Element> ImmutableItems { get; } = ImmutableArray<Element>.Empty;

                    public Element? NullableElement { get; } = null;

                    [Fluentify.SkipAutoInstantiation]
                    public Element SkipAutoInstantiationElement { get; } = new Element();
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
            MetadataReference.CreateFromFile(typeof(ImmutableArray<>).Assembly.Location),
        ];
    }
}
