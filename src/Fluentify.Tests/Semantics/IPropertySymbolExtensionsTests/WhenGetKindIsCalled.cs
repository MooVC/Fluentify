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

    private static readonly Compilation _compilation = CreateCompilation();

    [Fact]
    public void GivenArrayPropertyThenKindIsArray()
    {
        // Arrange
        IPropertySymbol property = GetProperty("ArrayItems");

        // Act
        Kind kind = property.GetKind(_compilation, CancellationToken.None);

        // Assert
        kind.Pattern.ShouldBe(Pattern.Array);
        kind.Member.Name.ShouldBe("global::Demo.Element");
    }

    [Fact]
    public void GivenCollectionPropertyThenKindIsCollection()
    {
        // Arrange
        IPropertySymbol property = GetProperty("CollectionItems");

        // Act
        Kind kind = property.GetKind(_compilation, CancellationToken.None);

        // Assert
        kind.Pattern.ShouldBe(Pattern.Collection);
        kind.Member.Name.ShouldBe("global::Demo.Element");
    }

    [Fact]
    public void GivenListInterfacePropertyThenKindIsEnumerable()
    {
        // Arrange
        IPropertySymbol property = GetProperty("ListItems");

        // Act
        Kind kind = property.GetKind(_compilation, CancellationToken.None);

        // Assert
        kind.Pattern.ShouldBe(Pattern.Enumerable);
        kind.Member.Name.ShouldBe("global::Demo.Element");
    }

    [Fact]
    public void GivenNonGenericCollectionPropertyThenKindIsScalar()
    {
        // Arrange
        IPropertySymbol property = GetProperty("NonGenericCollectionItems");

        // Act
        Kind kind = property.GetKind(_compilation, CancellationToken.None);

        // Assert
        kind.Pattern.ShouldBe(Pattern.Scalar);
        kind.Type.IsFrameworkType.ShouldBeTrue();
    }

    [Fact]
    public void GivenNonGenericEnumerablePropertyThenKindIsScalar()
    {
        // Arrange
        IPropertySymbol property = GetProperty("NonGenericEnumerableItems");

        // Act
        Kind kind = property.GetKind(_compilation, CancellationToken.None);

        // Assert
        kind.Pattern.ShouldBe(Pattern.Scalar);
        kind.Type.IsFrameworkType.ShouldBeTrue();
    }

    [Fact]
    public void GivenNonGenericListPropertyThenKindIsScalar()
    {
        // Arrange
        IPropertySymbol property = GetProperty("NonGenericListItems");

        // Act
        Kind kind = property.GetKind(_compilation, CancellationToken.None);

        // Assert
        kind.Pattern.ShouldBe(Pattern.Scalar);
        kind.Type.IsFrameworkType.ShouldBeTrue();
    }

    [Fact]
    public void GivenOnlyNonGenericCollectionImplementationThenKindIsScalar()
    {
        // Arrange
        IPropertySymbol property = GetProperty("NonGenericOnlyItems");

        // Act
        Kind kind = property.GetKind(_compilation, CancellationToken.None);

        // Assert
        kind.Pattern.ShouldBe(Pattern.Scalar);
        kind.Type.IsBuildable.ShouldBeFalse();
        kind.Type.IsFrameworkType.ShouldBeTrue();
    }

    [Fact]
    public void GivenImmutableArrayPropertyThenKindIsEnumerable()
    {
        // Arrange
        IPropertySymbol property = GetProperty("ImmutableItems");

        // Act
        Kind kind = property.GetKind(_compilation, CancellationToken.None);

        // Assert
        kind.Pattern.ShouldBe(Pattern.Enumerable);
        kind.Member.Name.ShouldBe("global::Demo.Element");
    }

    [Fact]
    public void GivenImmutableCollectionContractPropertyThenKindIsScalar()
    {
        // Arrange
        IPropertySymbol property = GetProperty("ImmutableInterfaceItems");

        // Act
        Kind kind = property.GetKind(_compilation, CancellationToken.None);

        // Assert
        kind.Pattern.ShouldBe(Pattern.Scalar);
    }

    [Fact]
    public void GivenInitializedImmutableListPropertyThenKindIsEnumerable()
    {
        // Arrange
        IPropertySymbol property = GetProperty("ImmutableListItems");

        // Act
        Kind kind = property.GetKind(_compilation, CancellationToken.None);

        // Assert
        kind.Pattern.ShouldBe(Pattern.Enumerable);
        kind.Member.Name.ShouldBe("global::Demo.Element");
    }

    [Fact]
    public void GivenInitializedListImplementationThenKindIsCollection()
    {
        // Arrange
        IPropertySymbol property = GetProperty("InitializedListItems");

        // Act
        Kind kind = property.GetKind(_compilation, CancellationToken.None);

        // Assert
        kind.Pattern.ShouldBe(Pattern.Collection);
        kind.Type.Initialization.ShouldBe("global::Demo.InitializedList.Create()");
        kind.Member.Name.ShouldBe("global::Demo.Element");
    }

    [Fact]
    public void GivenNullablePropertyThenInitializationUsesNonNullableType()
    {
        // Arrange
        IPropertySymbol property = GetProperty("NullableElement");

        // Act
        Kind kind = property.GetKind(_compilation, CancellationToken.None);

        // Assert
        kind.Type.IsNullable.ShouldBeTrue();
        kind.Type.Initialization.ShouldBe("new global::Demo.Element()");
    }

    [Fact]
    public void GivenSkipAutoInitializationThenTypeIsNotBuildable()
    {
        // Arrange
        IPropertySymbol property = GetProperty("SkipAutoInitializationElement");

        // Act
        Kind kind = property.GetKind(_compilation, CancellationToken.None);

        // Assert
        kind.Type.IsBuildable.ShouldBeFalse();
    }

    private static IPropertySymbol GetProperty(string propertyName)
    {
        INamedTypeSymbol type = _compilation.GetTypeByMetadataName(SampleTypeName)!;

        return type
            .GetMembers()
            .OfType<IPropertySymbol>()
            .Single(property => property.Name == propertyName);
    }

    private static Compilation CreateCompilation()
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText("""
            #nullable enable

            namespace Fluentify
            {
                using System;

                internal sealed class AutoInitializeWithAttribute : Attribute
                {
                    public AutoInitializeWithAttribute(string factory)
                    {
                    }
                }

                internal sealed class SkipAutoInitializationAttribute : Attribute
                {
                }

                internal sealed class SkipAutoInitializationAttribute : Attribute
                {
                }
            }

            namespace Demo
            {
                using System;
                using System.Collections;
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

                [Fluentify.AutoInitializeWith(nameof(Create))]
                public sealed class InitializedList : List<Element>
                {
                    private InitializedList()
                    {
                    }

                    public static InitializedList Create()
                    {
                        return new InitializedList();
                    }
                }

                public sealed class NonGenericOnlyCollection : ICollection
                {
                    public int Count => 0;

                    public bool IsSynchronized => false;

                    public object SyncRoot => this;

                    public void CopyTo(Array array, int index)
                    {
                    }

                    public IEnumerator GetEnumerator()
                    {
                        yield break;
                    }
                }

                public interface IImmutableArray<T> : ICollection<T>
                {
                }

                public sealed class Sample
                {
                    public Element[] ArrayItems { get; } = new Element[0];

                    public BuildableCollection CollectionItems { get; } = new BuildableCollection();

                    public ImmutableArray<Element> ImmutableItems { get; } = ImmutableArray<Element>.Empty;

                    [Fluentify.AutoInitializeWith(nameof(CreateImmutableInterfaceItems))]
                    public IImmutableArray<Element> ImmutableInterfaceItems { get; } = CreateImmutableInterfaceItems();

                    [Fluentify.AutoInitializeWith(nameof(CreateImmutableListItems))]
                    public ImmutableList<Element> ImmutableListItems { get; } = CreateImmutableListItems();

                    public InitializedList InitializedListItems { get; } = InitializedList.Create();

                    public IList<Element> ListItems { get; } = new List<Element>();

                    public ICollection NonGenericCollectionItems { get; } = new ArrayList();

                    public IEnumerable NonGenericEnumerableItems { get; } = new ArrayList();

                    public IList NonGenericListItems { get; } = new ArrayList();

                    public NonGenericOnlyCollection NonGenericOnlyItems { get; } = new NonGenericOnlyCollection();

                    public Element? NullableElement { get; } = default;

                    [Fluentify.SkipAutoInitialization]
                    public Element SkipAutoInitializationElement { get; } = new Element();

                    public static IImmutableArray<Element> CreateImmutableInterfaceItems()
                    {
                        return default!;
                    }

                    public static ImmutableList<Element> CreateImmutableListItems()
                    {
                        return ImmutableList<Element>.Empty;
                    }
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