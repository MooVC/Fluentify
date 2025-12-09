namespace Fluentify.Source;

using System;
using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string? GetScalarDelegateExtensionMethodBody(this Property property, Type type)
    {
        if (type.IsValueType || (type.IsFrameworkType && !type.IsBuildable))
        {
            return default;
        }

        if (property.Kind.Pattern != Pattern.Scalar)
        {
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

        string instance = $"var instance = subject.{property.Name};";

        string initialization = type.IsBuildable
            ? $$"""
                if (instance is null)
                {
                    instance = new {{type.Name}}();
                }
                """
            : """
                if (instance is null)
                {
                    throw new NotSupportedException();
                }
                """;

        return $$"""
            builder.ThrowIfNull("builder");

            {{instance}}

            {{initialization}}

            instance = builder(instance);

            return subject.{{property.Descriptor}}(instance);
            """;
    }
}
