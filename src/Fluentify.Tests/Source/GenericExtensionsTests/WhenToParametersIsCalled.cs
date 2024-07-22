namespace Fluentify.Source.GenericExtensionsTests;

using System.Collections.Generic;
using FluentAssertions;
using Fluentify.Model;
using Fluentify.Source;
using Xunit;

public sealed class WhenToParametersIsCalled
{
    [Fact]
    public void GivenEmptyListWhenCalledThenReturnsEmptyString()
    {
        // Arrange
        IReadOnlyList<Generic> empty = [];

        // Act
        string actual = empty.ToParameters();

        // Assert
        _ = actual.Should().BeEmpty();
    }

    [Fact]
    public void GivenSingleGenericWhenCalledThenReturnsSingleBracketedParameter()
    {
        // Arrange
        IReadOnlyList<Generic> single = [new() { Name = "T" }];
        const string expected = "<T>";

        // Act
        string actual = single.ToParameters();

        // Assert
        _ = actual.Should().Be(expected);
    }

    [Fact]
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
        _ = actual.Should().Be(expected);
    }
}