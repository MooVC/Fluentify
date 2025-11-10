namespace Fluentify.Console.Class.SkipAutoInstantiation;

/// <summary>
/// Demonstrates the SkipAutoInstantiation attribute preventing builder extensions.
/// </summary>
[Fluentify]
internal sealed class Example
{
    /// <summary>
    /// Gets the name of the example instance.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the dependency that should not be automatically instantiated via builder extensions.
    /// </summary>
    [SkipAutoInstantiation]
    public DependencySettings Dependency { get; init; } = new DependencySettings();

    /// <summary>
    /// Represents a dependency type that is otherwise buildable.
    /// </summary>
    internal sealed class DependencySettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencySettings"/> class.
        /// </summary>
        public DependencySettings()
        {
        }

        /// <summary>
        /// Gets or sets the connection string for the dependency.
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
    }
}