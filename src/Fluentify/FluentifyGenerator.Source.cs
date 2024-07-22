namespace Fluentify;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides for the definition of source that will be added to the <see cref="SourceProductionContext"/> of the generator.
/// </summary>
/// <typeparam name="T">The type of syntax to which the generator applies.</typeparam>
public partial class FluentifyGenerator<T>
{
    /// <summary>
    /// The definition for source that will be added to the <see cref="SourceProductionContext"/> of the generator.
    /// </summary>
    internal struct Source
    {
        /// <summary>
        /// Gets or sets the source code to be added to the <see cref="SourceProductionContext"/>.
        /// </summary>
        /// <value>
        /// The source code to be added to the <see cref="SourceProductionContext"/>.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the unique name associated with the source code within the <see cref="SourceProductionContext"/>.
        /// </summary>
        /// <value>
        /// The unique name associated with the source code within the <see cref="SourceProductionContext"/>.
        /// </value>
        public string Hint { get; set; }
    }
}