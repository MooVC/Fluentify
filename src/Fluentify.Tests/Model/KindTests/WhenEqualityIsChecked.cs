namespace Fluentify.Model.KindTests;

public abstract class WhenEqualityIsChecked
{
    [Test]
    public void GivenBothInstancesAreNullThenTheyAreEqual()
    {
        // Arrange
        Kind? instance1 = default;
        Kind? instance2 = default;

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentMemberThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Kind
        {
            Member = new()
            {
                Name = "string",
            },
            Type = new()
            {
                Name = "string[]",
            },
        };
        var instance2 = new Kind
        {
            Member = new()
            {
                Name = "int",
            },
            Type = new()
            {
                Name = "string[]",
            },
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeFalse();
    }

    [Test]
    public void GivenDifferentPatternThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Kind
        {
            Member = new()
            {
                Name = "string",
            },
            Pattern = Pattern.Array,
            Type = new()
            {
                Name = "string[]",
            },
        };
        var instance2 = new Kind
        {
            Member = new()
            {
                Name = "string",
            },
            Pattern = Pattern.Scalar,
            Type = new()
            {
                Name = "string[]",
            },
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeFalse();
    }

    [Test]
    public void GivenDifferentTypeThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Kind
        {
            Member = new()
            {
                Name = "string",
            },
            Pattern = Pattern.Array,
            Type = new()
            {
                Name = "string[]",
            },
        };
        var instance2 = new Kind
        {
            Member = new()
            {
                Name = "string",
            },
            Pattern = Pattern.Array,
            Type = new()
            {
                Name = "IEnumerable<string>",
            },
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeFalse();
    }

    [Test]
    public void GivenIdenticalInstancesThenTheyAreEqual()
    {
        // Arrange
        var instance1 = new Kind
        {
            Member = new()
            {
                Name = "string",
            },
            Pattern = Pattern.Array,
            Type = new()
            {
                Name = "string[]",
            },
        };
        var instance2 = new Kind
        {
            Member = new()
            {
                Name = "string",
            },
            Pattern = Pattern.Array,
            Type = new()
            {
                Name = "string[]",
            },
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeTrue();
    }

    [Test]
    public void GivenOneInstanceIsNullThenTheyAreNotEqual()
    {
        // Arrange
        var instance = new Kind
        {
            Member = new()
            {
                Name = "string",
            },
            Pattern = Pattern.Array,
            Type = new()
            {
                Name = "string[]",
            },
        };

        // Act
        bool areEqual = AreEqual(instance, default);

        // Assert
        areEqual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameReferenceThenTheyAreEqual()
    {
        // Arrange
        var instance = new Kind
        {
            Member = new()
            {
                Name = "string",
            },
            Pattern = Pattern.Array,
            Type = new()
            {
                Name = "string[]",
            },
        };

        // Act
        bool areEqual = AreEqual(instance, instance);

        // Assert
        areEqual.ShouldBeTrue();
    }

    private protected abstract bool AreEqual(Kind? instance1, Kind? instance2);
}