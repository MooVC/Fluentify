namespace Fluentify.Console.Record;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// An interface that demonstrates the libraries use on a nested record.
/// </summary>
[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1302:Interface names should begin with I", Justification = "The name is appropriate in this context.")]
[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "The name is appropriate in this context.")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "The name is appropriate in this context.")]
internal partial interface NestedInInterface
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