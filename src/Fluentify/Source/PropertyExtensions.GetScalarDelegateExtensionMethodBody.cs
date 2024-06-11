namespace Fluentify.Source;

using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string? GetScalarDelegateExtensionMethodBody(this Property property, Type type)
    {
        if (type.IsBuildable)
        {
            return $$"""
                ArgumentNullException.ThrowIfNull(builder);

                var instance = new {{type.Name}}();

                instance = builder(instance);

                return subject.{{property.Descriptor}}(instance);
                """;
        }

        return default;
    }
}