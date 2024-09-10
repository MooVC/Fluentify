namespace Fluentify.Console.Record.Example.Simple;

/// <summary>
/// A recreation of the example provided in the README.md.
/// </summary>
/// <param name="ConnectionString">A sample connection string, a common requirement in services that often benefit from the Fluent Builder pattern.</param>
/// <param name="Timeout">A timeout associated with the connection.</param>
[Fluentify]
public partial record MyServiceBuilder([Descriptor("ConnectsTo")] string ConnectionString, [Descriptor("Waits")] int Timeout)
{
    /// <summary>
    /// Gets an initial value to facilitate ease of consumption of the Fluent API.
    /// </summary>
    /// <value>
    /// An initial value to facilitate ease of consumption of the Fluent API.
    /// </value>
    public static MyServiceBuilder Empty => new();

    /// <summary>
    /// Generates an instance of <see cref="MyService"/> based on the configured values.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="MyService"/> based on the configured values.
    /// </returns>
    public MyService Build()
    {
        return new MyService(ConnectionString, TimeSpan.FromSeconds(Timeout));
    }
}