namespace Fluentify.Source;

using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string? GetDelegateExtensionMethodBody(this Property property, Type type)
    {
        if (type.IsBcl || type.IsValueType)
        {
            return default;
        }

        if (property.Kind.Pattern == Pattern.Scalar)
        {
            return property.GetScalarDelegateExtensionMethodBody(type);
        }

        if (!type.IsBuildable)
        {
            return default;
        }

        return $$"""
            builder.ThrowIfNull("builder");

            var instance = new {{type.Name}}();

            instance = builder(instance);

            return subject.{{property.Descriptor}}(instance);
            """;
    }
}