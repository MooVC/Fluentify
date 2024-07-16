namespace Fluentify.Console.Record.Example.Simple;

/// <summary>
/// A recreation of the example provided in the README.md.
/// </summary>
public class MyService
{
    /// <summary>
    /// Creates a new instance of <see cref="MyService"/>.
    /// </summary>
    /// <param name="connectionString">A sample connection string, a common requirement in services that often benefit from the Fluent Builder pattern.</param>
    /// <param name="timeout">A timeout associated with the connection.</param>
    public MyService(string connectionString, TimeSpan timeout)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
        ArgumentOutOfRangeException.ThrowIfLessThan(timeout.TotalSeconds, 1);

        ConnectionString = connectionString;
        Timeout = timeout;
    }

    /// <summary>
    /// Gets a sample connection string, a common requirement in services that often benefit from the Fluent Builder pattern.
    /// </summary>
    /// <value>
    /// A sample connection string, a common requirement in services that often benefit from the Fluent Builder pattern.
    /// </value>
    public string ConnectionString { get; }

    /// <summary>
    /// Gets a timeout associated with the connection.
    /// </summary>
    /// <value>
    /// A timeout associated with the connection.
    /// </value>
    public TimeSpan Timeout { get; }
}