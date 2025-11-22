namespace Fluentify.Console.Class.SkipAutoInstantiation;

using System.Collections.Generic;

/// <summary>
/// Demonstrates the SkipAutoInstantiation attribute preventing builder extensions for collection members.
/// </summary>
[Fluentify]
internal sealed class CollectionExample
{
    /// <summary>
    /// Gets the dependencies that should not be automatically instantiated via builder extensions.
    /// </summary>
    public List<CollectionExample.DependencySettings> Dependencies { get; init; } = new();

    /// <summary>
    /// Represents a dependency type that is otherwise buildable.
    /// </summary>
    [SkipAutoInstantiation]
    internal sealed class DependencySettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencySettings"/> class.
        /// </summary>
        public DependencySettings()
        {
        }

        /// <summary>
        /// Gets or sets the name of the dependency.
        /// </summary>
        /// <value>
        /// The name of the dependency.
        /// </value>
        public string Name { get; set; } = string.Empty;
    }
}