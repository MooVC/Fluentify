namespace Fluentify.Source;

using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string? GetEnumerableExtensionMethodBody(this Property property, Func<Property, string?> scalar)
    {
        if (property.Kind.Pattern != Pattern.Enumerable)
        {
            return default;
        }

        string? body = scalar(property);

        if (body is null)
        {
            return default;
        }

        Kind kind = property.Kind;

        return $$"""
            {{kind}} value = values;

            if (subject.{{property.Name}} != null)
            {
                value = subject.{{property.Name}}
                    .Union(values)
                    .ToArray();
            }

            {{body}}
            """;
    }
}