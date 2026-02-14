namespace Fluentify.Source.PropertyExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;
using Metadata = Fluentify.Source.Metadata;

public sealed partial class WhenGetExtensionsIsCalled
{
    [Fact]
    public void GivenNonScalarAndNotBuildableThenDelegateExtensionIsNotGenerated()
    {
        // Arrange
        var property = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "WithItems",
            Kind = new()
            {
                Pattern = Pattern.Collection,
                Member = new()
                {
                    Initialization = "new global::Demo.Element()",
                    IsBuildable = false,
                    IsFrameworkType = false,
                    Name = "global::Demo.Element",
                },
                Type = new()
                {
                    Name = "global::System.Collections.Generic.ICollection<global::Demo.Element>",
                },
            },
            Name = "Items",
        };

        var metadata = CreateMetadata();

        // Act
        string result = property.GetExtensions(ref metadata, _ => "return subject;");

        // Assert
        result.ShouldNotContain("Func<global::Demo.Element, global::Demo.Element> builder");
    }

    [Fact]
    public void GivenNonScalarAndBuildableThenDelegateExtensionIsGenerated()
    {
        // Arrange
        var property = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "WithItems",
            Kind = new()
            {
                Pattern = Pattern.Collection,
                Member = new()
                {
                    Initialization = "new global::Demo.Element()",
                    IsBuildable = true,
                    IsFrameworkType = false,
                    Name = "global::Demo.Element",
                },
                Type = new()
                {
                    Name = "global::System.Collections.Generic.ICollection<global::Demo.Element>",
                },
            },
            Name = "Items",
        };

        var metadata = CreateMetadata();

        // Act
        string result = property.GetExtensions(ref metadata, _ => "return subject;");

        // Assert
        result.ShouldContain("Func<global::Demo.Element, global::Demo.Element> builder");
        result.ShouldContain("instance = builder(instance);");
    }

    private static Metadata CreateMetadata()
    {
        return new Metadata
        {
            Constraints = [],
            Parameters = string.Empty,
            Subject = new Subject
            {
                Accessibility = Accessibility.Public,
                Name = "Sample",
                Properties = [],
                Type = new()
                {
                    Name = "global::Demo.Sample",
                },
            },
        };
    }
}