namespace Fluentify.Model.TypeTests;

using Fluentify.Model;

public sealed class WhenToStringIsCalled
{
    private const string NullableName = "Demo.Type?";
    private const string NonNullableName = "Demo.Type";

    [Fact]
    public void GivenNullableTypeNameWithoutAnnotationThenAnnotationIsAppended()
    {
        // Arrange
        var type = new Type
        {
            IsNullable = true,
            Name = NonNullableName,
        };

        // Act
        string result = type.ToString();

        // Assert
        result.ShouldBe(NullableName);
    }

    [Fact]
    public void GivenNullableTypeNameWithAnnotationThenNameIsReturned()
    {
        // Arrange
        var type = new Type
        {
            IsNullable = true,
            Name = NullableName,
        };

        // Act
        string result = type.ToString();

        // Assert
        result.ShouldBe(NullableName);
    }

    [Fact]
    public void GivenNonNullableTypeThenNameIsReturned()
    {
        // Arrange
        var type = new Type
        {
            IsNullable = false,
            Name = NonNullableName,
        };

        // Act
        string result = type.ToString();

        // Assert
        result.ShouldBe(NonNullableName);
    }
}