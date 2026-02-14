namespace Fluentify.Model;

using Valuify;

/// <summary>
/// The definition of the <see cref="Type"/> type, which is used to capture information relating to a data type.
/// </summary>
[Valuify]
internal sealed partial class Type
{
    /// <summary>
    /// The annotation used to denote if a type is deemed to be nullable.
    /// </summary>
    public const string NullableAnnotation = "?";

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
    /// Gets or sets the expression to use when initializing the type explicitly.
    /// </summary>
    /// <value>
    /// The expression to use when initializing the type explicitly.
    /// </value>
    public string Initialization { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether or not the type represents a framework defined type.
    /// </summary>
    /// <value>
    /// The value indicating whether or not the type represents a framework defined type.
    /// </value>
    public bool IsFrameworkType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the property is deemed to be nullable.
    /// </summary>
    /// <value>
    /// The value indicating whether or not the property is deemed to be nullable.
    /// </value>
    public bool IsNullable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the type is a value type.
    /// </summary>
    /// <value>
    /// The value indicating whether or not the type is a value type.
    /// </value>
    public bool IsValueType { get; set; }

    /// <summary>
    /// Gets or sets the name of the type.
    /// </summary>
    /// <value>
    /// The name of the type.
    /// </value>
    /// <remarks>
    /// When the type is a fundamental type, then the alias is typically used, otherwise the fully qualified type name is used.
    /// </remarks>
    public string Name { get; set; } = string.Empty;

    /// <inheritdoc/>
    public override string ToString()
    {
        if (IsNullable && !Name.EndsWith(NullableAnnotation))
        {
            return string.Concat(Name, NullableAnnotation);
        }

        return Name;
    }

    public string ToString(bool includeNullability)
    {
        if (includeNullability)
        {
            return ToString();
        }

        if (IsNullable && Name.EndsWith(NullableAnnotation))
        {
            return Name.Substring(0, Name.Length - 1);
        }

        return Name;
    }
}