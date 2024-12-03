namespace Fluentify.Console.Record;

/// <summary>
/// A record that demonstrates the libraries on a nested record.
/// </summary>
internal sealed partial class NestedInClass
{
    /// <summary>
    /// A record that demonstrates the libraries on a nested record.
    /// </summary>
    /// <param name="Age">The first property to be subject to the extension generator.</param>
    /// <param name="Name">The second property to be subject to the extension generator.</param>
    /// <param name="Attributes">The third property to be subject to the extension generator.</param>
    [Fluentify]
    internal sealed partial record Simple(int Age, string Name, IReadOnlyList<object>? Attributes = default);
}