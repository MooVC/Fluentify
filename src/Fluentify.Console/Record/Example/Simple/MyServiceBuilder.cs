namespace Fluentify.Console.Record.Example.Simple;

[Fluentify]
public partial record MyServiceBuilder(
    [Descriptor("ConnectsTo")] string ConnectionString,
    [Descriptor("Waits")] int Timeout)
{
    public static MyServiceBuilder Empty => new();

    public MyService Build()
    {
        return new MyService(ConnectionString, TimeSpan.FromSeconds(Timeout));
    }
}