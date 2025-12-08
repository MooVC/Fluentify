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
        string instance = property.Kind.Pattern == Pattern.Scalar
            ? $"subject.{property.Name}"
            : $"subject.{property.Name}?.FirstOrDefault()";

        string creation = type.IsBuildable
            ? $$"""
                if (instance is null)
                {
                    instance = new {{type.Name}}();
                }
                """
            : $$"""
                if (instance is null)
                {
                    throw new NotSupportedException("The existing value for {{property.Name}} is null and cannot be created.");
                }
                """;

        return $$"""
            builder.ThrowIfNull("builder");

            var instance = {{instance}};

            {{creation}}

            instance = builder(instance);

            return subject.{{property.Descriptor}}(instance);
            """;
    }
}