namespace Fluentify.Source.SubjectExtensionsTests;

using Fluentify.Model;

public sealed class WhenToMetadataIsCalled
{
    [Test]
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
        metadata.Constraints.ShouldBeSubsetOf(
        [
            "where T : class, new()",
            "where U : struct",
        ]);

        metadata.Parameters.ShouldBe("<T, U>");
        metadata.Subject.ShouldBe(subject);
    }

    [Test]
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
        metadata.Constraints.ShouldBeEmpty();
        metadata.Parameters.ShouldBeEmpty();
        metadata.Subject.ShouldBe(subject);
    }
}