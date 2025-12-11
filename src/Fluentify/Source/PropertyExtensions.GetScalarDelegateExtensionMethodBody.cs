namespace Fluentify.Source;

using System.Text;
using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string? GetScalarDelegateExtensionMethodBody(this Property property, Type type)
    {
        var builder = new StringBuilder("builder.ThrowIfNull(\"builder\");");

        builder = builder
            .AppendLine()
            .AppendLine()
            .AppendLine($"var instance = subject.{property.Name};")
            .AppendLine();

        if (!type.IsValueType)
        {
            string buildable = $$"""
                if (instance != null)
                {
                    instance = new {{type.Name}}();
                }
                """;

            const string nonBuildable = """
                if (instance != null)
                {
                    throw new NotSupportedException();
                }
                """;

            builder = type.IsBuildable
                ? builder.AppendLine(buildable)
                : builder.AppendLine(nonBuildable);

            builder = builder.AppendLine();
        }

        builder = builder
            .AppendLine("instance = builder(instance);")
            .AppendLine()
            .AppendLine($"return subject.{property.Descriptor}(instance);");

        return builder.ToString();
    }
}