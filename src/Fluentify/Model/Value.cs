namespace Fluentify.Model;

/// <summary>
/// The definition of the <see cref="Value"/> type, serving as a base for any type that is deemed equal based on its properties.
/// It is effectively a .NET Standard 2.0 version of a record.
/// </summary>
/// <typeparam name="TSelf">The derived type.</typeparam>
internal abstract class Value<TSelf>
    : IEquatable<TSelf>
    where TSelf : Value<TSelf>
{
    /// <summary>
    /// Determines whether two specified instances of <see cref="Value{TSelf}"/> are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns>true if <paramref name="left"/> and <paramref name="right"/> represent the same value; otherwise, false.</returns>
    public static bool operator ==(Value<TSelf>? left, Value<TSelf>? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="Value{TSelf}"/> are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns>true if <paramref name="left"/> and <paramref name="right"/> do not represent the same value; otherwise, false.</returns>
    public static bool operator !=(Value<TSelf>? left, Value<TSelf>? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="Value{TSelf}"/> instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>true if the specified object is equal to the current instance; otherwise, false.</returns>
    /// <remarks>
    /// This method overrides <see cref="object.Equals(object)"/> to provide a way to compare two <see cref="Value{TSelf}"/> instances.
    /// </remarks>
    public override bool Equals(object? obj)
    {
        return Equals(obj as TSelf);
    }

    /// <summary>
    /// Indicates whether the current <see cref="TSelf"/> instance is equal to another <see cref="TSelf"/> instance.
    /// </summary>
    /// <param name="other">An instance of <see cref="TSelf"/> to compare with this instance.</param>
    /// <returns>true if the current instance is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
    /// <remarks>
    /// This method implements the <see cref="IEquatable{T}"/> interface and provides a type-safe way to compare two <see cref="TSelf"/> instances.
    /// </remarks>
    public bool Equals(TSelf? other)
    {
        return Equals(this as TSelf, other);
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current <see cref="Subject"/> instance.</returns>
    /// <remarks>
    /// The hash code is calculated based on the values.
    /// This implementation is suitable for use in hashing algorithms and data structures like a hash table.
    /// </remarks>
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            IEnumerable<object> values = GetProperties();

            foreach (object value in values)
            {
                hash = (hash * 23) + value.GetHashCode();
            }

            return hash;
        }
    }

    /// <summary>
    /// Allows the derived class to nominate the properties to be considered by <see cref="GetHashCode"/>.
    /// </summary>
    /// <returns>The list of properties nominated by the derived class.</returns>

    protected abstract IEnumerable<object> GetProperties();

    private static bool Equals(TSelf? left, TSelf? right)
    {
        if (left is null)
        {
            return right is null;
        }

        if (right is null)
        {
            return left is null;
        }

        if (ReferenceEquals(left, right))
        {
            return true;
        }

        return left.GetHashCode() == right.GetHashCode();
    }
}