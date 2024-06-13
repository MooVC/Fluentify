namespace Fluentify.Model;

using System.Reflection.Metadata;
using Fluentify.Source;
using Microsoft.CodeAnalysis;

/// <summary>
/// The definition of the <see cref="Subject"/> type, which is used to capture information relating to a subject
/// upon which the Fluentify attribute has been placed.
/// </summary>
internal sealed class Subject
    : Value<Subject>
{
    /// <summary>
    /// Gets or sets the declared accessibility modifier for the subject.
    /// </summary>
    /// <value>
    /// The declared accessibility modifier for the subject.
    /// </value>
    public Accessibility Accessibility { get; set; }

    /// <summary>
    /// Gets or sets the generic parameters associated with the subject.
    /// </summary>
    /// <value>
    /// The generic parameters associated with the subject.
    /// </value>
    public IReadOnlyList<Generic> Generics { get; set; } = [];

    /// <summary>
    /// Gets or sets a value indicating whether or not the class upon which the Fluentify attribute has been placed has a default constructor.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the class upon which the Fluentify attribute has been placed has a default constructor.
    /// </value>
    public bool HasDefaultConstructor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the class upon which the Fluentify attribute has been placed is marked as partial.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the class upon which the Fluentify attribute has been placed is marked as partial.
    /// </value>
    public bool IsPartial { get; set; }

    /// <summary>
    /// Gets or sets the name of the class upon which the Fluentify attribute has been placed.
    /// </summary>
    /// <value>
    /// The name of the class upon which the Fluentify attribute has been placed.
    /// </value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the namespace for the class upon which the Fluentify attribute has been placed.
    /// </summary>
    /// <value>
    /// The namespace for the class upon which the Fluentify attribute has been placed.
    /// </value>
    public string Namespace { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the properties associated with the subject.
    /// </summary>
    /// <value>
    /// The properties associated with the subject.
    /// </value>
    public IReadOnlyList<Property> Properties { get; set; } = [];

    /// <inheritdoc/>
    public override string ToString()
    {
        string parameters = Generics.ToParameters();

        return string.Concat(Name, parameters);
    }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetProperties()
    {
        IEnumerable<object> values = Generics
            .Cast<object>()
            .Union(Properties);

        foreach (object value in values)
        {
            yield return value;
        }

        yield return Accessibility;
        yield return HasDefaultConstructor;
        yield return IsPartial;
        yield return Name;
        yield return Namespace;
    }
}