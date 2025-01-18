namespace Fluentify.Model.NestingTests;

public abstract class WhenInequalityIsChecked
{
    [Fact]
    public void GivenIdenticalInstancesThenTheyAreDeemedEqual()
    {
        // Arrange
        var instance1 = new Nesting
        {
            Declaration = "partial class",
            Name = "Simple",
            Qualification = "Complex",
        };

        var instance2 = new Nesting
        {
            Declaration = "partial class",
            Name = "Simple",
            Qualification = "Complex",
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
        var instance1 = new Nesting
        {
            Declaration = "partial class",
            Name = "Simple1",
            Qualification = "Complex",
        };

        var instance2 = new Nesting
        {
            Declaration = "partial class",
            Name = "Simple2",
            Qualification = "Complex",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeTrue();
    }

    [Fact]
    public void GivenADifferentQualificationThenTheyAreNotDeemedEqual()
    {
        // Arrange
        var instance1 = new Nesting
        {
            Declaration = "partial class",
            Name = "Simple",
            Qualification = "Complex1",
        };

        var instance2 = new Nesting
        {
            Declaration = "partial class",
            Name = "Simple",
            Qualification = "Complex2",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeTrue();
    }

    [Fact]
    public void GivenDifferentTypeThenTheyAreDeemedNotEqual()
    {
        // Arrange
        var instance1 = new Nesting
        {
            Declaration = "partial class",
            Name = "Simple",
            Qualification = "Complex",
        };

        var instance2 = new Nesting
        {
            Declaration = "partial struct",
            Name = "Simple",
            Qualification = "Complex",
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
        var instance = new Nesting
        {
            Declaration = "partial class",
            Name = "Simple",
            Qualification = "Complex",
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
        Nesting? instance1 = default;
        Nesting? instance2 = default;

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenSameReferenceThenTheyAreDeemedEqual()
    {
        // Arrange
        var instance1 = new Nesting
        {
            Declaration = "partial class",
            Name = "Simple",
            Qualification = "Complex",
        };

        Nesting instance2 = instance1;

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeFalse();
    }

    private protected abstract bool AreNotEqual(Nesting? instance1, Nesting? instance2);
}