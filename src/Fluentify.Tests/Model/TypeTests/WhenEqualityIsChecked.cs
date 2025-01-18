namespace Fluentify.Model.TypeTests;

public abstract class WhenEqualityIsChecked
{
    [Fact]
    public void GivenIdenticalInstancesThenTheyAreEqual()
    {
        // Arrange
        var instance1 = new Type
        {
            IsNullable = true,
            Name = "TypeName",
        };
        var instance2 = new Type
        {
            IsNullable = true,
            Name = "TypeName",
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeTrue();
    }

    [Fact]
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
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeFalse();
    }

    [Fact]
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
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenOneInstanceIsNullThenTheyAreNotEqual()
    {
        // Arrange
        var instance = new Type
        {
            Name = "TypeName",
        };

        // Act
        bool areEqual = AreEqual(instance, default);

        // Assert
        areEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenBothInstancesAreNullThenTheyAreEqual()
    {
        // Arrange
        Type? instance1 = default;
        Type? instance2 = default;

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeTrue();
    }

    [Fact]
    public void GivenSameReferenceThenTheyAreEqual()
    {
        // Arrange
        var instance = new Type
        {
            Name = "TypeName",
        };

        // Act
        bool areEqual = AreEqual(instance, instance);

        // Assert
        areEqual.ShouldBeTrue();
    }

    private protected abstract bool AreEqual(Type? instance1, Type? instance2);
}