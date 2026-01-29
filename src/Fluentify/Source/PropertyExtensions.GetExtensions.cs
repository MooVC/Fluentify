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
        bool isInternal = metadata.Subject.Accessibility < Accessibility.Public;

        string accessibility = isInternal
            ? "internal"
            : "public";

        string methods = property.GetExtensionMethods(ref metadata, scalar, isInternal);

        if (string.IsNullOrEmpty(methods))
        {
            return string.Empty;
        }

        return $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            {{accessibility}} static partial class {{metadata.Subject.Name}}Extensions
            {
                {{methods.Indent()}}
            }
            """;
    }

    private static string GetExtensionMethods(this Property property, ref Metadata metadata, Func<Property, string?> scalar, bool isInternal)
    {
        string parameter = property.Kind.ToString();
        string member = property.Kind.Member.ToString();

        Type type = property.Kind.Pattern == Pattern.Scalar
            ? property.Kind.Type
            : property.Kind.Member;

        (string? Body, string Parameter)[] extensions =
        [
            (property.GetArrayExtensionMethodBody(scalar), $"params {member}[] values"),
            (property.GetCollectionExtensionMethodBody(scalar), $"params {member}[] values"),
            (property.GetEnumerableExtensionMethodBody(scalar), $"params {member}[] values"),
            (property.GetDelegateExtensionMethodBody(type), $"Func<{type.Name}, {type.Name}> builder"),
            (property.GetScalarExtensionMethodBody(scalar), $"{parameter} value"),
        ];

        return property.GetExtensionMethods(extensions, ref metadata, isInternal);
    }

    private static string GetExtensionMethods(
        this Property property,
        (string? Body, string Parameter)[] extensions,
        ref Metadata metadata,
        bool isInternal)
    {
        string constraints = string.Join("\r\n", metadata.Constraints);
        string method = string.Concat(property.Descriptor, metadata.Parameters);
        string type = metadata.Subject.Type.ToString();

        string accessibility = isInternal || property.Accessibility < Accessibility.Public || property.IsHidden
            ? "internal"
            : "public";

        string GetMethod(string body, string parameter)
        {
            string signature = $"""
                {accessibility} static {type} {method}(
                    this {type} subject,
                    {parameter})
                """;

            if (!string.IsNullOrWhiteSpace(constraints))
            {
                signature = $$"""
                    {{signature}}
                        {{constraints.Indent()}}
                    """;
            }

            return $$"""
                {{signature}}
                {
                    subject.ThrowIfNull("subject");

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