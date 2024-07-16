namespace Fluentify.Source.SubjectExtensionsTests;

using Fluentify.Model;

public sealed class WhenToMetadataIsCalled
{
    [Fact]
    public void GivenSubjectWithoutGenericsThenMetadataHasNoConstraintsAndEmptyParameters()
    {
        // Arrange
        var subject = new Subject
        {
            Name = "TestClass",
            Generics = [],
            Properties = [],
        };

        // Act
        var metadata = subject.ToMetadata();

        // Assert
        _ = metadata.Constraints.Should().BeEmpty();
        _ = metadata.Parameters.Should().BeEmpty();
        _ = metadata.Subject.Should().Be(subject);
        _ = metadata.Type.Should().Be("TestClass");
    }

    [Fact]
    public void GivenSubjectWithGenericsThenMetadataContainsConstraintsAndParameters()
    {
        // Arrange
        var generics = new List<Generic>
        {
            new()
            {
                Name = "T",
                Constraints = ["class", "new()"],
            },
            new()
            {
                Name = "U",
                Constraints = ["struct"],
            },
        };

        var subject = new Subject
        {
            Name = "TestClass",
            Generics = generics,
            Properties = [],
        };

        // Act
        var metadata = subject.ToMetadata();

        // Assert
        _ = metadata.Constraints.Should().Contain(
        [
            "where T : class, new()",
            "where U : struct",
        ]);

        _ = metadata.Parameters.Should().Be("<T, U>");
        _ = metadata.Subject.Should().Be(subject);
        _ = metadata.Type.Should().Be("TestClass<T, U>");
    }
}