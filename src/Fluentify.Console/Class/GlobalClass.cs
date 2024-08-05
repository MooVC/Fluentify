#pragma warning disable SA1200 // Using directives should be placed correctly
using System.Diagnostics.CodeAnalysis;
using Fluentify;
#pragma warning restore SA1200 // Using directives should be placed correctly

/// <summary>
/// A class that demonstrates the libraries use without generics.
/// </summary>
[Fluentify]
[SuppressMessage("Major Bug", "S3903:Types should be defined in named namespaces", Justification = "The class is intended to demonstrate use in the global namespace.")]
internal sealed class GlobalClass
{
    /// <summary>
    /// Gets or sets the first property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The first property to be subject to the extension generator.
    /// </value>
    public int Age { get; init; }

    /// <summary>
    /// Gets or sets the second property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The second property to be subject to the extension generator.
    /// </value>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the third property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The third property to be subject to the extension generator.
    /// </value>
    public IReadOnlyList<object>? Attributes { get; init; }
}