namespace Fluentify.Source.SubjectExtensionsTests;

using Fluentify.Model;

public sealed class WhenGetConstructorIsCalled
{
    [Fact]
    public void GivenSubjectWithNoPropertiesThenReturnsDefaultConstructor()
    {
        // Arrange
        const string Expected = """
            using System.Diagnostics.CodeAnalysis;

            partial record TestClass
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public TestClass()
                    : this()
                {
                }

                #pragma warning restore CS8604
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
        result.ShouldBe(Expected);
    }

    [Fact]
    public void GivenSubjectWithPropertiesThenReturnsConstructorWithDefaultInitializers()
    {
        // Arrange
        const string Expected = """
            using System.Diagnostics.CodeAnalysis;
            
            partial record TestClass
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public TestClass()
                    : this(default(string), default(int))
                {
                }

                #pragma warning restore CS8604
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
        result.ShouldBe(Expected);
    }
}