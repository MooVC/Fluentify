namespace Fluentify.Console.Class;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// An interface that demonstrates the libraries use on a nested class.
/// </summary>
[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1302:Interface names should begin with I", Justification = "The name is appropriate in this context.")]
[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "The name is appropriate in this context.")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "The name is appropriate in this context.")]
internal interface NestedInInterface
{
    /// <summary>
    /// A class that demonstrates the libraries use on a nested class.
    /// </summary>
    [Fluentify]
    internal sealed class Simple
    {
        /// <summary>
        /// Gets the first property to be subject to the extension generator.
        /// </summary>
        /// <value>
        /// The first property to be subject to the extension generator.
        /// </value>
        public int Age { get; init; }

        /// <summary>
        /// Gets the second property to be subject to the extension generator.
        /// </summary>
        /// <value>
        /// The second property to be subject to the extension generator.
        /// </value>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Gets the third property to be subject to the extension generator.
        /// </summary>
        /// <value>
        /// The third property to be subject to the extension generator.
        /// </value>
        public IReadOnlyList<object>? Attributes { get; init; }
    }
}