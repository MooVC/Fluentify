namespace Fluentify.Console.Record.NestedInRefRecordStructTests;

public sealed class WhenWithNameIsCalled
{
    [Test]
    [Arguments("Avery Brooks")]
    [Arguments("")]
    [Arguments(" ")]
    public void GivenANameThenTheValueIsApplied(string name)
    {
        // Arrange
        var original = new NestedInRefRecordStruct.Simple
        {
            Age = Random.Shared.Next(),
            Attributes = [new(), new()],
            Name = "Patrick Stewart",
        };

        // Act
        NestedInRefRecordStruct.Simple actual = original.WithName(name);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.Attributes.ShouldBeEquivalentTo(original.Attributes);
        actual.Name.ShouldBeEquivalentTo(name);
    }

    [Test]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        NestedInRefRecordStruct.Simple? subject = default;

        // Act
        Func<NestedInRefRecordStruct.Simple> act = () => subject!.WithName("Avery Brooks");

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
    }
}