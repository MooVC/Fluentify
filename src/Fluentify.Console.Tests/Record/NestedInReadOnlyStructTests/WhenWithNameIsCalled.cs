﻿namespace Fluentify.Console.Record.NestedInReadOnlyStructTests;

public sealed class WhenWithNameIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        NestedInReadOnlyStruct.Simple? subject = default;

        // Act
        Func<NestedInReadOnlyStruct.Simple> act = () => subject!.WithName("Avery Brooks");

        // Assert
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
    }

    [Theory]
    [InlineData("Avery Brooks")]
    [InlineData("")]
    [InlineData(" ")]
    public void GivenANameThenTheValueIsApplied(string name)
    {
        // Arrange
        var original = new NestedInReadOnlyStruct.Simple
        {
            Age = Random.Shared.Next(),
            Attributes = [new(), new()],
            Name = "Patrick Stewart",
        };

        // Act
        NestedInReadOnlyStruct.Simple actual = original.WithName(name);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.Attributes.Should().BeEquivalentTo(original.Attributes);
        _ = actual.Name.Should().BeEquivalentTo(name);
    }
}