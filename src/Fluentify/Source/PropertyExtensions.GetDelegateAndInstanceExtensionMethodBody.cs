namespace Fluentify.Source;

using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string? GetDelegateAndInstanceExtensionMethodBody(this Property property, Type type)
    {
        if (type.IsFrameworkType)
        {
            return default;
        }

        if (property.Kind.Pattern != Pattern.Scalar && !type.IsBuildable)
        {
            return default;
        }

        return $$"""
            builder.ThrowIfNull("builder");

            instance = builder(instance);

            return subject.{{property.Descriptor}}(instance);
            """;
    }
}