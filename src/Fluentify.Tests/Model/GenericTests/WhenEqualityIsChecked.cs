namespace Fluentify.Model.GenericTests;

public abstract class WhenEqualityIsChecked
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
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeTrue();
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
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeFalse();
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
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeFalse();
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
        bool areEqual = AreEqual(instance, default);

        // Assert
        _ = areEqual.Should().BeFalse();
    }

    [Fact]
    public void GivenBothInstancesAreNullThenTheyAreDeemedEqual()
    {
        // Arrange
        Generic? instance1 = default;
        Generic? instance2 = default;

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeTrue();
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
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeTrue();
    }

    private protected abstract bool AreEqual(Generic? instance1, Generic? instance2);
}