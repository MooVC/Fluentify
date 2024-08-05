#pragma warning disable SA1200 // Using directives should be placed correctly
using System.Diagnostics.CodeAnalysis;
using Fluentify;
#pragma warning restore SA1200 // Using directives should be placed correctly

/// <summary>
/// A class that demonstrates the libraries use without generics.
/// </summary>
/// <param name="Age">The first property to be subject to the extension generator.</param>
/// <param name="Name">The second property to be subject to the extension generator.</param>
/// <param name="Attributes">The third property to be subject to the extension generator.</param>
[Fluentify]
[SuppressMessage("Major Bug", "S3903:Types should be defined in named namespaces", Justification = "The class is intended to demonstrate use in the global namespace.")]
internal sealed partial record GlobalRecord(int Age, string Name, IReadOnlyList<object>? Attributes = default);