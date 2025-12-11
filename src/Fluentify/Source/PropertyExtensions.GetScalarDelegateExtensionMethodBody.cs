namespace Fluentify.Source;

using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string? GetScalarDelegateExtensionMethodBody(this Property property, Type type)
    {
        string instance = $"var instance = subject.{property.Name};";

        string buildable = $$"""
            if (instance is null)
            {
                instance = new {{type.Name}}();
            }
            """;

        const string nonBuildable = """
            if (instance is null)
            {
                throw new NotSupportedException();
            }
            """;

        string initialization = type.IsBuildable
            ? buildable
            : nonBuildable;

        return $$"""
            builder.ThrowIfNull("builder");

            {{instance}}

            {{initialization}}

            instance = builder(instance);

            return subject.{{property.Descriptor}}(instance);
            """;
    }
}