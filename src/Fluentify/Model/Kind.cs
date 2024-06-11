namespace Fluentify.Model;

using System.Collections.Generic;

/// <summary>
/// The definition of the <see cref="Kind"/> type, which is used to capture information relating to a the data type.
/// </summary>
internal sealed class Kind
    : Value<Kind>
{
    /// <summary>
    /// Denotes a Kind that has not been configured.
    /// </summary>
    public static readonly Kind Unspecified = new();

    /// <summary>
    /// Gets or sets the data type associated with member of the <see cref="Type"/> when the <see cref="Pattern"/>
    /// is anything other than <see cref="Pattern.Scalar"/>.
    /// </summary>
    /// <value>
    /// The data type associated with member of the <see cref="Type"/> when the <see cref="Pattern"/>
    /// is anything other than <see cref="Pattern.Scalar"/>.
    /// </value>
    public Type Member { get; set; } = Type.Unspecified;

    /// <summary>
    /// Gets or sets the pattern associated with the data type represented by the <see cref="Kind"/>.
    /// </summary>
    /// <value>
    /// The pattern associated with the data type represented by the <see cref="Kind"/>.
    /// </value>
    public Pattern Pattern { get; set; } = Pattern.Scalar;

    /// <summary>
    /// Gets or sets the data type associated with the kind.
    /// </summary>
    /// <value>
    /// The data type associated with the kind.
    /// </value>
    public Type Type { get; set; } = Type.Unspecified;

    /// <inheritdoc/>
    public override string ToString()
    {
        return Type.ToString();
    }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetProperties()
    {
        yield return Member;
        yield return Pattern;
        yield return Type;
    }
}