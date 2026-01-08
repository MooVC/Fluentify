namespace Fluentify.Semantics.IPropertySymbolExtensionsTests;

using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public sealed class WhenTryGetInitializationIsCalled
{
    private const string PropertySampleName = "Demo.PropertySample";
    private const string RecordSampleName = "Demo.RecordSample";
    private const string SkippedTypeName = "Demo.SkippedType";

    private static readonly Compilation _compilation = CreateCompilation();

    [Fact]
    public void GivenPropertyWithAutoInitiateWithAttributeThenInitializationIsReturned()
    {
        // Arrange
        IPropertySymbol property = GetProperty(PropertySampleName, "Property");

        // Act
        bool result = property.TryGetInitialization(out string initialization);

        // Assert
        result.ShouldBeTrue();
        initialization.ShouldBe("global::Demo.Widget.LocalBuild()");
    }

    [Fact]
    public void GivenRecordParameterWithAutoInitiateWithAttributeThenInitializationIsReturned()
    {
        // Arrange
        IPropertySymbol property = GetProperty(RecordSampleName, "Value");

        // Act
        bool result = property.TryGetInitialization(out string initialization);

        // Assert
        result.ShouldBeTrue();
        initialization.ShouldBe("global::Demo.Widget.Build()");
    }

    [Fact]
    public void GivenPropertyWithMissingMemberThenFalseIsReturned()
    {
        // Arrange
        IPropertySymbol property = GetProperty(PropertySampleName, "Missing");

        // Act
        bool result = property.TryGetInitialization(out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    [Fact]
    public void GivenPropertyWithSkipAutoInitializationThenFalseIsReturned()
    {
        // Arrange
        IPropertySymbol property = GetProperty(PropertySampleName, "Skipped");

        // Act
        bool result = property.TryGetInitialization(out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    [Fact]
    public void GivenPropertyWithTypeInitializationThenInitializationIsReturned()
    {
        // Arrange
        IPropertySymbol property = GetProperty(PropertySampleName, "TypeInitialization");

        // Act
        bool result = property.TryGetInitialization(out string initialization);

        // Assert
        result.ShouldBeTrue();
        initialization.ShouldBe("global::Demo.WidgetWithInitialization.Build()");
    }

    [Fact]
    public void GivenPropertyWithSkippedTypeThenFalseIsReturned()
    {
        // Arrange
        IPropertySymbol property = GetProperty(SkippedTypeName, "Property");

        // Act
        bool result = property.TryGetInitialization(out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    private static IPropertySymbol GetProperty(string typeName, string propertyName)
    {
        INamedTypeSymbol type = _compilation.GetTypeByMetadataName(typeName)!;

        return type
            .GetMembers()
            .OfType<IPropertySymbol>()
            .Single(property => property.Name == propertyName);
    }

    private static Compilation CreateCompilation()
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText("""
            namespace Fluentify
            {
                using System;

                internal sealed class AutoInitiateWithAttribute
                    : Attribute
                {
                    public AutoInitiateWithAttribute(string factory)
                    {
                    }
                }

                internal sealed class SkipAutoInitializationAttribute
                    : Attribute
                {
                }
            }

            namespace Demo
            {
                [Fluentify.AutoInitiateWith(nameof(Build))]
                public sealed class Widget
                {
                    public static Widget Build() => new Widget();
                }

                [Fluentify.AutoInitiateWith(nameof(Build))]
                public sealed class WidgetWithInitialization
                {
                    public static WidgetWithInitialization Build() => new WidgetWithInitialization();
                }

                public sealed record RecordSample(Widget Value);

                public sealed class PropertySample
                {
                    [Fluentify.AutoInitiateWith(nameof(LocalBuild))]
                    public Widget Property { get; } = new Widget();

                    [Fluentify.AutoInitiateWith("Missing")]
                    public Widget Missing { get; } = new Widget();

                    [Fluentify.SkipAutoInitialization]
                    public Widget Skipped { get; } = new Widget();

                    public WidgetWithInitialization TypeInitialization { get; } = new WidgetWithInitialization();

                    internal static Widget LocalBuild()
                    {
                        return new Widget();
                    }
                }

                [Fluentify.AutoInitiateWith(nameof(Build))]
                public sealed class TypeWithAttribute
                {
                    public static TypeWithAttribute Build() => new TypeWithAttribute();

                    public Widget Property { get; } = new Widget();
                }

                [Fluentify.SkipAutoInitialization]
                public sealed class SkippedType
                {
                    [Fluentify.AutoInitiateWith(nameof(Widget.Build))]
                    public Widget Property { get; } = new Widget();
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