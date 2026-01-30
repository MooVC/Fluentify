namespace Fluentify.Source.KeywordsTests;

using System.Text;

public sealed class WhenIsReservedIsCalled
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GivenNullEmptyOrWhitespaceWhenStringThenReturnsFalse(string? value)
    {
        // Arrange & Act
        bool result = value!.IsReserved();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenAReservedKeywordWhenStringThenReturnsTrue()
    {
        // Arrange
        int element = Random.Shared.Next(Keywords.Reserved.Count);
        string keyword = Keywords.Reserved.ElementAt(element);

        // Act
        bool result = keyword.IsReserved();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenAtPrefixedReservedKeywordWhenStringThenReturnsFalse()
    {
        // Arrange
        int element = Random.Shared.Next(Keywords.Reserved.Count);
        string keyword = Keywords.Reserved.ElementAt(element);

        // Act
        bool result = $"@{keyword}".IsReserved();

        // Assert
        result.ShouldBeFalse();
    }

    [Theory]
    [InlineData(" class")]
    [InlineData("class ")]
    [InlineData("\tclass")]
    [InlineData("class\t")]
    [InlineData("\nclass")]
    [InlineData("class\n")]
    public void GivenLeadingOrTrailingWhitespaceWhenStringThenReturnsFalse(string value)
    {
        // Arrange & Act
        bool result = value.IsReserved();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenNullWhenStringBuilderThenReturnsFalse()
    {
        // Arrange
        StringBuilder? builder = default;

        // Act
        bool result = builder!.IsReserved();

        // Assert
        result.ShouldBeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void GivenEmptyOrWhitespaceWhenStringBuilderThenReturnsFalse(string value)
    {
        // Arrange
        var builder = new StringBuilder(value);

        // Act
        bool result = builder.IsReserved();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenAReservedKeywordWhenStringBuilderThenReturnsTrue()
    {
        // Arrange
        int element = Random.Shared.Next(Keywords.Reserved.Count);
        string keyword = Keywords.Reserved.ElementAt(element);
        var builder = new StringBuilder(keyword);

        // Act
        bool result = builder.IsReserved();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenAtPrefixedReservedKeywordWhenStringBuilderThenReturnsFalse()
    {
        // Arrange
        int element = Random.Shared.Next(Keywords.Reserved.Count);
        string keyword = Keywords.Reserved.ElementAt(element);
        var builder = new StringBuilder($"@{keyword}");

        // Act
        bool result = builder.IsReserved();

        // Assert
        result.ShouldBeFalse();
    }

    [Theory]
    [InlineData(" class")]
    [InlineData("class ")]
    [InlineData("\tclass")]
    [InlineData("class\t")]
    [InlineData("\nclass")]
    [InlineData("class\n")]
    public void GivenLeadingOrTrailingWhitespaceWhenStringBuilderThenReturnsFalse(string value)
    {
        // Arrange
        var builder = new StringBuilder(value);

        // Act
        bool result = builder.IsReserved();

        // Assert
        result.ShouldBeFalse();
    }
}