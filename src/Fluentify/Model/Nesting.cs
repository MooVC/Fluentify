namespace Fluentify.Model;

using System.Collections.Generic;
using Valuify;

/// <summary>
/// The definition of the <see cref="Nesting"/> type, which is used to capture information relating to type within which a subject is defined.
/// </summary>
[Valuify]
internal sealed partial class Nesting
{
    /// <summary>Gets or sets the kind associated with the type, be it a class, record etc.</summary>
    /// <value>The kind associated with the type, be it a class, record etc.</value>
    public string Declaration { get; set; } = string.Empty;

    /// <summary>Gets or sets the name of the type.</summary>
    /// <value>The name of the type.</value>
    public string Name { get; set; } = string.Empty;

    /// <summary>Gets or sets the qualified name of the type, which includes any generic arguments.</summary>
    /// <value>The qualified name of the type, which includes any generic arguments.</value>
    public string Qualification { get; set; } = string.Empty;
}