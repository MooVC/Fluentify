namespace Fluentify.StringExtensionsTests;

public sealed class WhenIndentIsCalled
{
    [Test]
    public void GivenEmptyStringThenReturnsEmptyString()
    {
        // Arrange
        string input = string.Empty;

        // Act
        string result = input.Indent();

        // Assert
        result.ShouldBe(string.Empty);
    }

    [Test]
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
        result.ShouldBe(expected);
    }

    [Test]
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
        result.ShouldBe(expected);
    }

    [Test]
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
        result.ShouldBe(expected);
    }

    [Test]
    public void GivenSingleLineStringThenReturnsSameString()
    {
        // Arrange
        string input = "This is a test.";

        // Act
        string result = input.Indent();

        // Assert
        result.ShouldBe(input);
    }
}