namespace Fluentify.Syntax.GenericExtensionsTests;

using System.Collections.Generic;
using Fluentify.Model;

public sealed class WhenToParametersIsCalled
{
    [Test]
    public void GivenEmptyListWhenCalledThenReturnsEmptyString()
    {
        // Arrange
        IReadOnlyList<Generic> empty = [];

        // Act
        string actual = empty.ToParameters();

        // Assert
        actual.ShouldBeEmpty();
    }

    [Test]
    public void GivenMultipleGenericsWhenCalledThenReturnsCommaSeparatedBracketedParameters()
    {
        // Arrange
        IReadOnlyList<Generic> multiple =
        [
            new() { Name = "T" },
            new() { Name = "U" },
            new() { Name = "V" },
        ];
        const string expected = "<T, U, V>";

        // Act
        string actual = multiple.ToParameters();

        // Assert
        actual.ShouldBe(expected);
    }

    [Test]
    public void GivenSingleGenericWhenCalledThenReturnsSingleBracketedParameter()
    {
        // Arrange
        IReadOnlyList<Generic> single = [new() { Name = "T" }];
        const string expected = "<T>";

        // Act
        string actual = single.ToParameters();

        // Assert
        actual.ShouldBe(expected);
    }
}