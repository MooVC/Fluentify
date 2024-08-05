namespace Fluentify.Source;

using Fluentify.Model;

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

        return $$"""
            {{kind}} value = values;

            if (subject.{{property.Name}} != null)
            {
                value = new {{kind.Member}}[value.Length + subject.{{property.Name}}.Length];

                subject.{{property.Name}}.CopyTo(value, 0);
                values.CopyTo(value, subject.{{property.Name}}.Length);
            }

            {{body}}
            """;
    }
}