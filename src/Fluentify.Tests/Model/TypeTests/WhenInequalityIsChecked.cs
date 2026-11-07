namespace Fluentify.Model.TypeTests;

public abstract class WhenInequalityIsChecked
{
    [Test]
    public void GivenBothInstancesAreNullThenTheyAreEqual()
    {
        // Arrange
        Type? instance1 = default;
        Type? instance2 = default;

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeFalse();
    }

    [Test]
    public void GivenDifferentIsNullableThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Type
        {
            IsNullable = true,
            Name = "TypeName",
        };
        var instance2 = new Type
        {
            Name = "TypeName",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentNameThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Type
        {
            Name = "TypeName1",
        };
        var instance2 = new Type
        {
            Name = "TypeName2",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeTrue();
    }

    [Test]
    public void GivenIdenticalInstancesThenTheyAreEqual()
    {
        // Arrange
        var instance1 = new Type
        {
            Name = "TypeName",
        };
        var instance2 = new Type
        {
            Name = "TypeName",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeFalse();
    }

    [Test]
    public void GivenOneInstanceIsNullThenTheyAreNotEqual()
    {
        // Arrange
        var instance = new Type
        {
            Name = "TypeName",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance, default);

        // Assert
        areNotEqual.ShouldBeTrue();
    }

    [Test]
    public void GivenSameReferenceThenTheyAreEqual()
    {
        // Arrange
        var instance = new Type
        {
            Name = "TypeName",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance, instance);

        // Assert
        areNotEqual.ShouldBeFalse();
    }

    private protected abstract bool AreNotEqual(Type? instance1, Type? instance2);
}