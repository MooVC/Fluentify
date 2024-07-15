namespace Fluentify.Console.Record.Example.Simple.MyServiceBuilderTests;

public sealed class WhenMyServiceIsBuilt
{
    [Fact]
    public void GivenRequiredValuesThenTheInstanceIsBuilt()
    {
        // Arrange
        const string connectionString = "The String";
        const int timeout = 30;

        // Act
        MyService service = MyServiceBuilder
            .Empty
            .ConnectsTo(connectionString)
            .Waits(timeout)
            .Build();

        // Assert
        _ = service.Should().NotBeNull();
        _ = service.ConnectionString.Should().Be(connectionString);
        _ = service.Timeout.TotalSeconds.Should().Be(timeout);
    }
}