namespace Fluentify.Model;

using System.Collections.Generic;

/// <summary>
/// The definition of the <see cref="Type"/> type, which is used to capture information relating to a data type.
/// </summary>
internal sealed class Type
    : Value<Type>
{
    /// <summary>
    /// Denotes a Type that has not been configured.
    /// </summary>
    public static readonly Type Unspecified = new();

    /// <summary>
    /// Gets or sets a value indicating whether or not the type adheres to the new() constraint.
    /// </summary>
    /// <value>
    /// The value indicating whether or not the type adheres to the new() constraint.
    /// </value>
    public bool IsBuildable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the property is deemed to be nullable.
    /// </summary>
    /// <value>
    /// The value indicating whether or not the property is deemed to be nullable.
    /// </value>
    public bool IsNullable { get; set; }

    /// <summary>
    /// Gets or sets the fully qualified name of the type.
    /// </summary>
    /// <value>
    /// The fully qualified name of the type.
    /// </value>
    public string Name { get; set; } = string.Empty;

    /// <inheritdoc/>
    public override string ToString()
    {
        if (IsNullable && !Name.EndsWith("?"))
        {
            return $"{Name}?";
        }

        return Name;
    }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetProperties()
    {
        yield return IsBuildable;
        yield return IsNullable;
        yield return Name;
    }
}