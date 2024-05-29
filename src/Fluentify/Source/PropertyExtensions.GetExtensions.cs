namespace Fluentify.Source;

using Fluentify.Model;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    /// <summary>
    /// Gets the source code needed to add Fluent extensions relating to the <paramref name="property"/> in the context of the subject
    /// encapsulated by <paramref name="metadata"/>.
    /// </summary>
    /// <param name="property">The <see cref="Property"/> for which the extensions are to be generated.</param>
    /// <param name="metadata">Information relating to the subject to which the <paramref name="property"/> belongs.</param>
    /// <returns>
    /// The source code needed to support the extensions relating the <paramref name="property"/> in the context of <paramref name="metadata"/>.
    /// </returns>
    public static string GetExtensions(this Property property, ref Metadata metadata)
    {
        string accessibility = metadata.Subject.Accessibility < Accessibility.Public || property.Accessibility < Accessibility.Public
            ? "internal"
            : "public";

        string methods = property.GetExtensionMethods(ref metadata);

        return $$"""
            using System;

            {{accessibility}} static partial class {{metadata.Subject.Name}}Extensions
            {
                {{methods.Indent()}}
            }
            """;
    }

    private static string GetExtensionMethods(this Property property, ref Metadata metadata)
    {
        string nullability = property.IsNullable && !property.Type.EndsWith("?")
            ? "?"
            : string.Empty;

        string parameter = string.Concat(property.Type, nullability);

        (string? Body, string Parameter)[] extensions =
        [
            (property.GetScalarExtensionMethodBody(), parameter),
            (property.GetScalarDelegateExtensionMethodBody(), $"global::Fluentify.Builder<{parameter}>"),
        ];

        return property.GetExtensionMethods(extensions, ref metadata);
    }

    private static string GetExtensionMethods(this Property property, (string? Body, string Parameter)[] extensions, ref Metadata metadata)
    {
        string constraints = string.Join("\r\n", metadata.Constraints);
        string method = string.Concat(property.Descriptor, metadata.Parameters);
        string type = metadata.Type;

        string GetMethod(string body, string parameter)
        {
            string signature = $"public static {type} {method}(this {type} subject, {parameter} value)";

            if (!string.IsNullOrWhiteSpace(constraints))
            {
                signature = $$"""
                    {{signature}}
                        {{constraints}}
                    """;
            }

            return $$"""
                {{signature}}
                {
                    ArgumentNullException.ThrowIfNull(subject);

                    {{body.Indent()}}
                }
                """;
        }

        string[] methods = extensions
            .Where(method => method.Body is not null)
            .Select(method => GetMethod(method.Body!, method.Parameter))
            .ToArray();

        return string.Join("\r\n\r\n", methods);
    }

    private static string GetScalarExtensionMethodBody(this Property property)
    {
        return $$"""
            return subject with
            {
                {{property.Name}} = value,
            };
            """;
    }

    private static string? GetScalarDelegateExtensionMethodBody(this Property property)
    {
        if (property.IsBuildable)
        {
            return $$"""
            var instance = new {{property.Type}}();

            return subject.{{property.Descriptor}}(instance);
            """;
        }

        return default;
    }
}