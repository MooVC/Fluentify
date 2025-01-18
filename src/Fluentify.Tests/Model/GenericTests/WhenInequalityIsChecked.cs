namespace Fluentify.Model.GenericTests;

public abstract class WhenInequalityIsChecked
{
    [Fact]
    public void GivenIdenticalInstancesThenTheyAreDeemedEqual()
    {
        // Arrange
        var constraints = new List<string> { "constraint1", "constraint2" };

        var instance1 = new Generic
        {
            Constraints = constraints,
            Name = "GenericName",
        };

        var instance2 = new Generic
        {
            Constraints = constraints,
            Name = "GenericName",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenADifferentNameThenTheyAreNotDeemedEqual()
    {
        // Arrange
        var constraints = new List<string> { "constraint1", "constraint2" };

        var instance1 = new Generic
        {
            Constraints = constraints,
            Name = "GenericName1",
        };

        var instance2 = new Generic
        {
            Constraints = constraints,
            Name = "GenericName2",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeTrue();
    }

    [Fact]
    public void GivenDifferentConstraintsThenTheyAreDeemedNotEqual()
    {
        // Arrange
        var instance1 = new Generic
        {
            Constraints = ["constraint1"],
            Name = "GenericName",
        };

        var instance2 = new Generic
        {
            Constraints = ["constraint2"],
            Name = "GenericName",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeTrue();
    }

    [Fact]
    public void GivenOneInstanceIsNullThenTheyAreDeemedNotEqual()
    {
        // Arrange
        var instance = new Generic
        {
            Constraints = ["constraint1"],
            Name = "GenericName",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance, default);

        // Assert
        areNotEqual.ShouldBeTrue();
    }

    [Fact]
    public void GivenBothInstancesAreNullThenTheyAreDeemedEqual()
    {
        // Arrange
        Generic? instance1 = default;
        Generic? instance2 = default;

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenSameReferenceThenTheyAreDeemedEqual()
    {
        // Arrange
        var instance1 = new Generic
        {
            Constraints = ["constraint1"],
            Name = "GenericName",
        };

        Generic instance2 = instance1;

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeFalse();
    }

    private protected abstract bool AreNotEqual(Generic? instance1, Generic? instance2);
}