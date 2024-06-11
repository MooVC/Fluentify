namespace Fluentify.Source.SubjectExtensionsTests;

using Fluentify.Model;

public sealed class WhenGetConstructorIsCalled
{
    [Fact]
    public void GivenSubjectWithNoPropertiesThenReturnsDefaultConstructor()
    {
        // Arrange
        const string Expected = """
            partial record TestClass
            {
                public TestClass()
                    : this()
                {
                }
            }
            """;

        var subject = new Subject
        {
            Name = "TestClass",
            Properties = [],
        };

        // Act
        string result = subject.GetConstructor();

        // Assert
        _ = result.Should().Be(Expected);
    }

    [Fact]
    public void GivenSubjectWithPropertiesThenReturnsConstructorWithDefaultInitializers()
    {
        // Arrange
        const string Expected = """
            partial record TestClass
            {
                public TestClass()
                    : this(default, default)
                {
                }
            }
            """;

        var subject = new Subject
        {
            Name = "TestClass",
            Properties =
            [
                new Property
                {
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "string",
                        },
                    },
                    Name = "Property1",
                },
                new Property
                {
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "int",
                        },
                    },
                    Name = "Property2",
                },
            ],
        };

        // Act
        string result = subject.GetConstructor();

        // Assert
        _ = result.Should().Be(Expected);
    }
}