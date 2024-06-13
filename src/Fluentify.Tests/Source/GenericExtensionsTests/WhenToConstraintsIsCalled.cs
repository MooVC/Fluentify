namespace Fluentify.Source.GenericExtensionsTests;

using System.Collections.Generic;
using FluentAssertions;
using Fluentify.Model;
using Fluentify.Source;
using Xunit;

public sealed class WhenToConstraintsIsCalled
{
    [Fact]
    public void GivenEmptyListWhenCalledThenReturnsEmptyList()
    {
        // Arrange
        IReadOnlyList<Generic> empty = [];

        // Act
        IReadOnlyList<string> actual = empty.ToConstraints();

        // Assert
        _ = actual.Should().BeEmpty();
    }

    [Fact]
    public void GivenSingleGenericWithoutConstraintsWhenCalledThenReturnsEmptyWhereClause()
    {
        // Arrange
        IReadOnlyList<Generic> single = [new() { Name = "T", Constraints = [] }];
        IReadOnlyList<string> expected = ["where T : "];

        // Act
        IReadOnlyList<string> actual = single.ToConstraints();

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GivenSingleGenericWithSingleConstraintWhenCalledThenReturnsSingleWhereClause()
    {
        // Arrange
        IReadOnlyList<Generic> single =
        [
            new() { Name = "T", Constraints = ["class"] }
        ];

        IReadOnlyList<string> expected = ["where T : class"];

        // Act
        IReadOnlyList<string> actual = single.ToConstraints();

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GivenSingleGenericWithMultipleConstraintsWhenCalledThenReturnsFormattedWhereClause()
    {
        // Arrange
        IReadOnlyList<Generic> single =
        [
            new() { Name = "T", Constraints = ["class", "new()"] }
        ];

        IReadOnlyList<string> expected = ["where T : class, new()"];

        // Act
        IReadOnlyList<string> actual = single.ToConstraints();

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GivenMultipleGenericsWithConstraintsWhenCalledThenReturnsMultipleWhereClauses()
    {
        // Arrange
        IReadOnlyList<Generic> multiple =
        [
            new() { Name = "T", Constraints = ["class"] },
            new() { Name = "U", Constraints = ["struct"] },
            new() { Name = "V", Constraints = ["IComparable", "new()"] }
        ];

        IReadOnlyList<string> expected =
        [
            "where T : class",
            "where U : struct",
            "where V : IComparable, new()",
        ];

        // Act
        IReadOnlyList<string> actual = multiple.ToConstraints();

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }
}