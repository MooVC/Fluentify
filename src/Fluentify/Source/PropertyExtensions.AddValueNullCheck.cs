namespace Fluentify.Source;

using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    /// <summary>
    /// Adds the value null check to the <paramref name="body"/> when the <paramref name="property"/> requires it.
    /// </summary>
    /// <param name="property">The property associated with the generated assignment.</param>
    /// <param name="body">The generated body to which the null check is to be added.</param>
    /// <param name="expression">The expression to be checked for null.</param>
    /// <param name="parameterName">The parameter name to include in the exception.</param>
    /// <returns>
    /// The generated body with a value null check when required, otherwise the original <paramref name="body"/>.
    /// </returns>
    internal static string AddValueNullCheck(this Property property, string body, string expression, string parameterName)
    {
        string nullCheck = property.GetValueNullCheck(expression, parameterName);

        if (string.IsNullOrWhiteSpace(nullCheck))
        {
            return body;
        }

        return $$"""
            {{nullCheck}}

            {{body}}
            """;
    }

    /// <summary>
    /// Gets the value null check for the <paramref name="property"/> when one is required.
    /// </summary>
    /// <param name="property">The property associated with the generated assignment.</param>
    /// <param name="expression">The expression to be checked for null.</param>
    /// <param name="parameterName">The parameter name to include in the exception.</param>
    /// <returns>
    /// The generated value null check when required, otherwise an empty string.
    /// </returns>
    internal static string GetValueNullCheck(this Property property, string expression, string parameterName)
    {
        if (!property.ThrowsWhenValueIsNull)
        {
            return string.Empty;
        }

        return $"""{expression}.ThrowIfNull("{parameterName}");""";
    }
}