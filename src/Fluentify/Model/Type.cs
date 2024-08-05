namespace Fluentify.Model;

using System.Collections.Generic;

/// <summary>
/// The definition of the <see cref="Type"/> type, which is used to capture information relating to a data type.
/// </summary>
internal sealed class Type
    : Value<Type>
{
    /// <summary>
    /// The alias for the <see cref="bool"/> type.
    /// </summary>
    public const string BooleanAlias = "bool";

    /// <summary>
    /// The annotation used to denote if a type is deemed to be nullable.
    /// </summary>
    public const string NullableAnnotation = "?";

    /// <summary>
    /// Denotes a Type that has not been configured.
    /// </summary>
    public static readonly Type Unspecified = new();

    /// <summary>
    /// Gets a value indicating whether or not the type is a boolean.
    /// </summary>
    /// <value>
    /// The value indicating whether or not the type is a boolean.
    /// </value>
    public bool IsBoolean => Name.Length <= 5 && Name.StartsWith(BooleanAlias, StringComparison.InvariantCulture);

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

    /// <inheritdoc/>
    protected override IEnumerable<object> GetProperties()
    {
        yield return IsBuildable;
        yield return IsNullable;
        yield return Name;
    }
}