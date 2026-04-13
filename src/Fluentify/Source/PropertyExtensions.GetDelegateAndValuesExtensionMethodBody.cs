namespace Fluentify.Source;

using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string? GetDelegateAndValuesExtensionMethodBody(this Property property, Type type)
    {
        if (property.Kind.Pattern == Pattern.Scalar || type.IsFrameworkType || !type.IsBuildable)
        {
            return default;
        }

        return $$"""
            builder.ThrowIfNull("builder");

            foreach (var value in values)
            {
                subject = subject.{{property.Descriptor}}(value, builder);
            }

            return subject;
            """;
    }
}