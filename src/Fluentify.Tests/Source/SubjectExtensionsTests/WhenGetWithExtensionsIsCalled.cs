namespace Fluentify.Source.SubjectExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;

public sealed class WhenGetWithExtensionsIsCalled
{
    [Fact]
    public void GivenInternalSubjectWithReservedAndSubjectPropertiesThenEscapedParametersAreGenerated()
    {
        // Arrange
        var subject = new Subject
        {
            Accessibility = Accessibility.Internal,
            Name = "Sample",
            Properties =
            [
                new Property
                {
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "int",
                        },
                    },
                    Name = "Class",
                },
                new Property
                {
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "int",
                        },
                    },
                    Name = "Subject",
                },
            ],
            Type = new()
            {
                Name = "global::Demo.Sample",
            },
        };

        // Act
        string result = subject.GetWithExtensions();

        // Assert
        result.ShouldContain("internal static partial class SampleExtensions");
        result.ShouldContain("Func<int> @class = default");
        result.ShouldContain("Func<int> subject1 = default");
        result.ShouldContain("Class = @classValue");
        result.ShouldContain("Subject = subject1Value");
    }
}
