namespace Fluentify.Model;

/// <summary>
/// Denotes the pattern type to be used when generating an extension for a <see cref="Kind"/> associated with a given <see cref="Property"/>.
/// </summary>
internal enum Pattern
{
    /// <summary>
    /// Denotes that the <see cref="Kind"/> is to be treated as an Array.
    /// The extension generated for the type will allow for members to be added to the list.
    /// </summary>
    Array,

    /// <summary>
    /// Denotes that the <see cref="Kind"/> is to be treated as type that implements <see cref="ICollection{T}"/>.
    /// The extension generated for the type will allow for members to be added to the list.
    /// </summary>
    Collection,

    /// <summary>
    /// Denotes that the <see cref="Kind"/> is to be treated as type that implements <see cref="IEnumerable{T}"/>.
    /// The extension generated for the type will allow for members to be added to the list.
    /// </summary>
    Enumerable,

    /// <summary>
    /// Denotes that the <see cref="Kind"/> is to be treated as a scalar type that can be set directly.
    /// </summary>
    Scalar,
}