namespace Fluentify.Model;

using Fluentify.Syntax;
using Graphify;
using Microsoft.CodeAnalysis;
using Valuify;

/// <summary>
/// The definition of the <see cref="Subject"/> type, which is used to capture information relating to a subject
/// upon which the Fluentify attribute has been placed.
/// </summary>
[Valuify]
[Graphify(Mode = Modes.Synchronous, PropertyPrefix = "_")]
internal sealed partial class Subject
{
    /// <summary>
    /// Gets or sets the declared accessibility modifier for the subject.
    /// </summary>
    /// <value>
    /// The declared accessibility modifier for the subject.
    /// </value>
    [Traverse(Scope = TraverseScope.None)]
    public Accessibility Accessibility { get; set; }

    /// <summary>
    /// Gets or sets the generic parameters associated with the subject.
    /// </summary>
    /// <value>
    /// The generic parameters associated with the subject.
    /// </value>
    [Traverse(Scope = TraverseScope.None)]
    public IReadOnlyList<Generic> Generics { get; set; } = [];

    /// <summary>
    /// Gets a value indicating whether or not the type upon which the Fluentify attribute has been placed has a default constructor.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the type upon which the Fluentify attribute has been placed has a default constructor.
    /// </value>
    [Traverse(Scope = TraverseScope.None)]
    public bool HasDefaultConstructor => Type.IsBuildable;

    /// <summary>
    /// Gets or sets a value indicating whether or not the type upon which the Fluentify attribute has been placed is considered a candidate
    /// for partial content.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the type upon which the Fluentify attribute has been placed is considered a candidate
    /// for partial content.
    /// </value>
    [Traverse(Scope = TraverseScope.None)]
    public bool IsPartial { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the subject is a record.
    /// </summary>
    [Traverse(Scope = TraverseScope.None)]
    public bool IsRecord { get; set; }

    /// <summary>
    /// Gets or sets the name of the type upon which the Fluentify attribute has been placed.
    /// </summary>
    /// <value>
    /// The name of the type upon which the Fluentify attribute has been placed.
    /// </value>
    [Traverse(Scope = TraverseScope.None)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the namespace for the type upon which the Fluentify attribute has been placed.
    /// </summary>
    /// <value>
    /// The namespace for the type upon which the Fluentify attribute has been placed.
    /// </value>
    [Traverse(Scope = TraverseScope.None)]
    public string Namespace { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the declarations associated with the parent types in order of declaration.
    /// </summary>
    /// <value>
    /// The declarations associated with the parent types in order of declaration.
    /// </value>
    [Traverse(Scope = TraverseScope.None)]
    public IReadOnlyList<Nesting> Nesting { get; set; } = [];

    /// <summary>
    /// Gets or sets the properties associated with the subject.
    /// </summary>
    /// <value>
    /// The properties associated with the subject.
    /// </value>
    [Traverse(Scope = TraverseScope.Property)]
    public IReadOnlyList<Property> Properties { get; set; } = [];

    /// <summary>
    /// Gets or sets a value indicating whether nullable reference type annotations are supported for generated code.
    /// </summary>
    /// <value>
    /// A value indicating whether nullable reference type annotations are supported for generated code.
    /// </value>
    [Traverse(Scope = TraverseScope.None)]
    public bool SupportsNullableReferenceTypes { get; set; }

    /// <summary>
    /// Gets or sets the data type of the Subject.
    /// </summary>
    /// <value>
    /// The data type of the Subject.
    /// </value>
    [Traverse(Scope = TraverseScope.None)]
    public Type Type { get; set; } = Type.Unspecified;

    /// <inheritdoc/>
    public override string ToString()
    {
        string parameters = Generics.ToParameters();

        return string.Concat(Name, parameters);
    }
}