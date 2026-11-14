namespace Fluentify.Syntax.KeywordsTests;

using System.Text;

public sealed class WhenIsReservedIsCalled
{
    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
    [Arguments("")]
    [Arguments("   ")]
    public void GivenEmptyOrWhitespaceWhenStringBuilderThenReturnsFalse(string value)
    {
        // Arrange
        var builder = new StringBuilder(value);

        // Act
        bool result = builder.IsReserved();

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    [Arguments(" class")]
    [Arguments("class ")]
    [Arguments("\tclass")]
    [Arguments("class\t")]
    [Arguments("\nclass")]
    [Arguments("class\n")]
    public void GivenLeadingOrTrailingWhitespaceWhenStringBuilderThenReturnsFalse(string value)
    {
        // Arrange
        var builder = new StringBuilder(value);

        // Act
        bool result = builder.IsReserved();

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    [Arguments(" class")]
    [Arguments("class ")]
    [Arguments("\tclass")]
    [Arguments("class\t")]
    [Arguments("\nclass")]
    [Arguments("class\n")]
    public void GivenLeadingOrTrailingWhitespaceWhenStringThenReturnsFalse(string value)
    {
        // Arrange & Act
        bool result = value.IsReserved();

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    [Arguments((string?)null)]
    [Arguments("")]
    [Arguments("   ")]
    public void GivenNullEmptyOrWhitespaceWhenStringThenReturnsFalse(string? value)
    {
        // Arrange & Act
        bool result = value!.IsReserved();

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void GivenNullWhenStringBuilderThenReturnsFalse()
    {
        // Arrange
        StringBuilder? builder = default;

        // Act
        bool result = builder!.IsReserved();

        // Assert
        result.ShouldBeFalse();
    }
}