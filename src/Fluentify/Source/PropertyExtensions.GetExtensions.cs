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
    /// <param name="scalar">A function that enables the generation of the source associated with a scalar transform.</param>
    /// <returns>
    /// The source code needed to support the extensions relating the <paramref name="property"/> in the context of <paramref name="metadata"/>.
    /// </returns>
    public static string GetExtensions(this Property property, ref Metadata metadata, Func<Property, string?> scalar)
    {
        string accessibility = metadata.Subject.Accessibility < Accessibility.Public || property.Accessibility < Accessibility.Public
            ? "internal"
            : "public";

        string methods = property.GetExtensionMethods(ref metadata, scalar);

        return $$"""
            using System;

            {{accessibility}} static partial class {{metadata.Subject.Name}}Extensions
            {
                {{methods.Indent()}}
            }
            """;
    }

    private static string GetExtensionMethods(this Property property, ref Metadata metadata, Func<Property, string?> scalar)
    {
        string parameter = property.Kind.ToString();
        string member = property.Kind.Member.ToString();

        Type type = property.Kind.Pattern == Pattern.Scalar
            ? property.Kind.Type
            : property.Kind.Member;

        (string? Body, string Parameter)[] extensions =
        [
            (property.GetArrayExtensionMethodBody(scalar), $"params {member}[] values"),
            (property.GetScalarDelegateExtensionMethodBody(type), $"global::Fluentify.Builder<{type.Name}> builder"),
            (property.GetScalarExtensionMethodBody(scalar), $"{parameter} value"),
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
            string signature = $"public static {type} {method}(this {type} subject, {parameter})";

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
}