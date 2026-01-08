namespace Fluentify.Model.TypeTests;

using Fluentify.Model;

public sealed class WhenToStringWithIncludeNullabilityIsCalled
{
    private const string NullableName = "Demo.Type?";
    private const string NonNullableName = "Demo.Type";

    [Fact]
    public void GivenIncludeNullabilityThenNullabilityIsIncluded()
    {
        // Arrange
        var type = new Type
        {
            IsNullable = true,
            Name = NonNullableName,
        };

        // Act
        string result = type.ToString(includeNullability: true);

        // Assert
        result.ShouldBe(NullableName);
    }

    [Fact]
    public void GivenExcludeNullabilityThenAnnotationIsRemoved()
    {
        // Arrange
        var type = new Type
        {
            IsNullable = true,
            Name = NullableName,
        };

        // Act
        string result = type.ToString(includeNullability: false);

        // Assert
        result.ShouldBe(NonNullableName);
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
        string result = type.ToString(includeNullability: false);

        // Assert
        result.ShouldBe(NonNullableName);
    }
}
