namespace Fluentify.StringExtensionsTests;

public sealed class WhenIndentIsCalled
{
    [Fact]
    public void GivenEmptyStringThenReturnsEmptyString()
    {
        // Arrange
        string input = string.Empty;

        // Act
        string result = input.Indent();

        // Assert
        _ = result.Should().Be(string.Empty);
    }

    [Fact]
    public void GivenSingleLineStringThenReturnsSameString()
    {
        // Arrange
        string input = "This is a test.";

        // Act
        string result = input.Indent();

        // Assert
        _ = result.Should().Be(input);
    }

    [Fact]
    public void GivenMultilineStringThenIndentsNonBlankLinesAfterSkip()
    {
        // Arrange
        string input = """
            Line1
            Line2

            Line4
            """;
        string expected = """
            Line1
                Line2

                Line4
            """;

        // Act
        string result = input.Indent();

        // Assert
        _ = result.Should().Be(expected);
    }

    [Fact]
    public void GivenMultilineStringWithDifferentWhitespaceThenIndentsWithWhitespace()
    {
        // Arrange
        string input = """
            Line1
            Line2

            Line4
            """;
        string expected = """
            Line1
            >>Line2

            >>Line4
            """;

        // Act
        string result = input.Indent(whitespace: ">>");

        // Assert
        _ = result.Should().Be(expected);
    }

    [Fact]
    public void GivenMultilineStringWithSkipThenSkipsLinesCorrectly()
    {
        // Arrange
        string input = """
            Line1
            Line2
            Line3
            Line4
            """;
        string expected = """
            Line1
            Line2
                Line3
                Line4
            """;

        // Act
        string result = input.Indent(skip: 2);

        // Assert
        _ = result.Should().Be(expected);
    }
}