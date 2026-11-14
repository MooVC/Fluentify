namespace Fluentify.Syntax;

using Fluentify.Model;

using static Fluentify.Syntax.PropertyExtensions_Resources;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string? GetArrayExtensionMethodBody(this Property property, Func<Property, string?> scalar)
    {
        if (property.Kind.Pattern != Pattern.Array)
        {
            return default;
        }

        string? body = scalar(property);

        if (body is null)
        {
            return default;
        }

        Kind kind = property.Kind;

        return string.Format(ArrayMethodBody, GetParameterNullCheck("values", "values"), kind, property.Name, kind.Member, body);
    }
}