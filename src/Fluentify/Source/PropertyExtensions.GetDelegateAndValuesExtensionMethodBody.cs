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
            return subject
                .{{property.Descriptor}}(values)
                .{{property.Descriptor}}(builder);
            """;
    }
}