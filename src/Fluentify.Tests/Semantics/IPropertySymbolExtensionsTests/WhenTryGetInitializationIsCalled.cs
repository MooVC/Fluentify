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
    private const string TypeWithAttributeName = "Demo.TypeWithAttribute";

    private static readonly Compilation Compilation = CreateCompilation();

    [Fact]
    public void GivenPropertyWithAutoInitiateWithAttributeThenInitializationIsReturned()
    {
        // Arrange
        var property = GetProperty(PropertySampleName, "Property");

        // Act
        bool result = property.TryGetInitialization(out string initialization);

        // Assert
        result.ShouldBeTrue();
        initialization.ShouldBe("global::Demo.Widget.Build()");
    }

    [Fact]
    public void GivenRecordParameterWithAutoInitiateWithAttributeThenInitializationIsReturned()
    {
        // Arrange
        var property = GetProperty(RecordSampleName, "Value");

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
        var property = GetProperty(PropertySampleName, "Missing");

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
        var property = GetProperty(PropertySampleName, "Skipped");

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
        var property = GetProperty(PropertySampleName, "TypeInitialization");

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
        var property = GetProperty(SkippedTypeName, "Property");

        // Act
        bool result = property.TryGetInitialization(out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    [Fact]
    public void GivenPropertyWithoutAutoInitiateWithAttributeThenFalseIsReturned()
    {
        // Arrange
        var property = GetProperty(TypeWithAttributeName, "Property");

        // Act
        bool result = property.TryGetInitialization(out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    private static IPropertySymbol GetProperty(string typeName, string propertyName)
    {
        var type = Compilation.GetTypeByMetadataName(typeName)!;

        return type
            .GetMembers()
            .OfType<IPropertySymbol>()
            .Single(property => property.Name == propertyName);
    }

    private static Compilation CreateCompilation()
    {
        var tree = CSharpSyntaxTree.ParseText(
            """
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
                public sealed class Widget
                {
                    public static Widget Build() => new Widget();
                }

                [Fluentify.AutoInitiateWith(nameof(Build))]
                public sealed class WidgetWithInitialization
                {
                    public static WidgetWithInitialization Build() => new WidgetWithInitialization();
                }

                public sealed record RecordSample([Fluentify.AutoInitiateWith(nameof(Widget.Build))] Widget Value);

                public sealed class PropertySample
                {
                    [Fluentify.AutoInitiateWith(nameof(Widget.Build))]
                    public Widget Property { get; } = new Widget();

                    [Fluentify.AutoInitiateWith("Missing")]
                    public Widget Missing { get; } = new Widget();

                    [Fluentify.SkipAutoInitialization]
                    public Widget Skipped { get; } = new Widget();

                    public WidgetWithInitialization TypeInitialization { get; } = new WidgetWithInitialization();
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
