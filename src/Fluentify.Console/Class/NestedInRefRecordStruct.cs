namespace Fluentify.Console.Class;

/// <summary>
/// A readonly record struct that demonstrates the libraries use on a nested class.
/// </summary>
internal readonly record struct NestedInRefRecordStruct
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