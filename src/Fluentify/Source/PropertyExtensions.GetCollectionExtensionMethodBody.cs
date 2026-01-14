namespace Fluentify.Source;

using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string? GetCollectionExtensionMethodBody(this Property property, Func<Property, string?> scalar)
    {
        if (property.Kind.Pattern != Pattern.Collection)
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
            {{kind}} value = new {{kind.ToString(false)}}();

            if (subject.{{property.Name}} != null)
            {
                foreach (var element in subject.{{property.Name}})
                {
                    value.Add(element);
                }
            }

            foreach (var element in values)
            {
                value.Add(element);
            }

            {{body}}
            """;
    }
}