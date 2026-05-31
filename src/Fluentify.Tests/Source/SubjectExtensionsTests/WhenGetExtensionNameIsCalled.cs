namespace Fluentify.Source.SubjectExtensionsTests;

using Fluentify.Model;

public sealed class WhenGetExtensionNameIsCalled
{
    [Fact]
    public void GivenNestedSubjectThenCompositeNameIsReturned()
    {
        // Arrange
        const string Expected = "Level1Level2Level3Extensions";

        var subject = new Subject
        {
            Name = "Level3",
            Nesting =
            [
                new Nesting
                {
                    Name = "Level1",
                },
                new Nesting
                {
                    Name = "Level2",
                },
            ],
        };

        // Act
        string result = subject.GetExtensionClassName();

        // Assert
        result.ShouldBe(Expected);
    }

    [Fact]
    public void GivenTopLevelSubjectThenSubjectNameIsReturned()
    {
        // Arrange
        const string Expected = "Level3Extensions";

        var subject = new Subject
        {
            Name = "Level3",
        };

        // Act
        string result = subject.GetExtensionClassName();

        // Assert
        result.ShouldBe(Expected);
    }
}