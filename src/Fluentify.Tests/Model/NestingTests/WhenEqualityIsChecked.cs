namespace Fluentify.Model.NestingTests;

public abstract class WhenEqualityIsChecked
{
    [Fact]
    public void GivenIdenticalInstancesThenTheyAreDeemedEqual()
    {
        // Arrange
        var instance1 = new Nesting
        {
            Declaration = "partial class",
            Generics = [new Generic { Name = "T" }],
            Name = "Simple",
            Qualification = "Complex",
        };

        var instance2 = new Nesting
        {
            Declaration = "partial class",
            Generics = [new Generic { Name = "T" }],
            Name = "Simple",
            Qualification = "Complex",
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeTrue();
    }

    [Fact]
    public void GivenDifferentDeclarationThenTheyAreDeemedNotEqual()
    {
        // Arrange
        var instance1 = new Nesting
        {
            Declaration = "partial class",
            Generics = [new Generic { Name = "T" }],
            Name = "Simple",
            Qualification = "Complex",
        };

        var instance2 = new Nesting
        {
            Declaration = "struct",
            Generics = [new Generic { Name = "T" }],
            Name = "Simple",
            Qualification = "Complex",
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenADifferentGenericsThenTheyAreNotDeemedEqual()
    {
        // Arrange
        var instance1 = new Nesting
        {
            Declaration = "partial class",
            Generics = [new Generic { Name = "T1" }],
            Name = "Simple",
            Qualification = "Complex",
        };

        var instance2 = new Nesting
        {
            Declaration = "partial class",
            Generics = [new Generic { Name = "T2" }],
            Name = "Simple",
            Qualification = "Complex",
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenADifferentNameThenTheyAreNotDeemedEqual()
    {
        // Arrange
        var instance1 = new Nesting
        {
            Declaration = "partial class",
            Generics = [new Generic { Name = "T" }],
            Name = "Simple1",
            Qualification = "Complex",
        };

        var instance2 = new Nesting
        {
            Declaration = "partial class",
            Generics = [new Generic { Name = "T" }],
            Name = "Simple2",
            Qualification = "Complex",
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenADifferentQualificationThenTheyAreNotDeemedEqual()
    {
        // Arrange
        var instance1 = new Nesting
        {
            Declaration = "partial class",
            Generics = [new Generic { Name = "T" }],
            Name = "Simple",
            Qualification = "Complex1",
        };

        var instance2 = new Nesting
        {
            Declaration = "partial class",
            Generics = [new Generic { Name = "T" }],
            Name = "Simple",
            Qualification = "Complex2",
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenOneInstanceIsNullThenTheyAreDeemedNotEqual()
    {
        // Arrange
        var instance = new Nesting
        {
            Declaration = "partial class",
            Generics = [new Generic { Name = "T" }],
            Name = "Simple",
            Qualification = "Complex",
        };

        // Act
        bool areEqual = AreEqual(instance, default);

        // Assert
        areEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenBothInstancesAreNullThenTheyAreDeemedEqual()
    {
        // Arrange
        Nesting? instance1 = default;
        Nesting? instance2 = default;

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeTrue();
    }

    [Fact]
    public void GivenSameReferenceThenTheyAreDeemedEqual()
    {
        // Arrange
        var instance1 = new Nesting
        {
            Declaration = "partial class",
            Generics = [new Generic { Name = "T" }],
            Name = "Simple",
            Qualification = "Complex",
        };

        Nesting instance2 = instance1;

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        areEqual.ShouldBeTrue();
    }

    private protected abstract bool AreEqual(Nesting? instance1, Nesting? instance2);
}