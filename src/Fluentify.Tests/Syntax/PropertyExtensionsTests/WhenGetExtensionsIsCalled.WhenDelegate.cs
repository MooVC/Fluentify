namespace Fluentify.Syntax.PropertyExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;

public sealed partial class WhenGetExtensionsIsCalled
{
    [Test]
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

        Subject subject = CreateSubject();

        // Act
        string result = property.GetExtensions(subject, _ => "return subject;").ToString();

        // Assert
        result.ShouldContain("global::System.Func<global::Demo.Element, global::Demo.Element> builder");
        result.ShouldContain("instance = builder(instance);");
    }

    [Test]
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

        Subject subject = CreateSubject();

        // Act
        string result = property.GetExtensions(subject, _ => "return subject;").ToString();

        // Assert
        result.ShouldNotContain("global::System.Func<global::Demo.Element, global::Demo.Element> builder");
    }

    [Test]
    public void GivenNonScalarWithFrameworkTypeThenDelegateExtensionIsNotGenerated()
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
                    Initialization = "new global::System.String()",
                    IsBuildable = true,
                    IsFrameworkType = true,
                    Name = "string",
                },
                Type = new()
                {
                    Name = "global::System.Collections.Generic.ICollection<string>",
                },
            },
            Name = "Items",
        };

        Subject subject = CreateSubject();

        // Act
        string result = property.GetExtensions(subject, _ => "return subject;").ToString();

        // Assert
        result.ShouldNotContain("global::System.Func<string, string> builder");
    }

    private static Subject CreateSubject()
    {
        return new Subject
        {
            Accessibility = Accessibility.Public,
            Name = "Sample",
            Properties = [],
            Type = new()
            {
                Name = "global::Demo.Sample",
            },
        };
    }
}