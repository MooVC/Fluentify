namespace Fluentify.Source;

using System;
using System.Collections.Generic;
using System.Linq;
using Fluentify.Model;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="Subject"/>.
/// </summary>
internal static partial class SubjectExtensions
{
    /// <summary>
    /// Gets the source code needed to add the With extension to the <paramref name="subject"/>.
    /// </summary>
    /// <param name="subject">The <see cref="Subject"/> to which the With extension is to be added.</param>
    /// <returns>
    /// The source code needed to add the With extension to the <paramref name="subject"/>.
    /// </returns>
    public static string GetWithExtensions(this Subject subject)
    {
        Metadata metadata = subject.ToMetadata();
        IReadOnlyList<Property> properties = subject.Properties
            .OrderBy(property => property.Name)
            .ToArray();

        string type = subject.Type.ToString();
        string constraints = string.Join("\r\n", metadata.Constraints);
        string methodName = string.Concat("With", metadata.Parameters);
        string accessibility = subject.Accessibility < Accessibility.Public
            ? "internal"
            : "public";

        string parameters = string.Join(
            ",\r\n    ",
            properties.Select(property =>
            {
                string name = ToParameterName(property.Name);
                return $"Func<{property.Kind}> {name} = default";
            }));

        string values = string.Join(
            "\r\n",
            properties.Select(property =>
            {
                string name = ToParameterName(property.Name);
                string valueName = $"{name}Value";
                return $"var {valueName} = ReferenceEquals({name}, null) ? subject.{property.Name} : {name}();";
            }));

        string initializers = string.Join(
            "\r\n",
            properties.Select(property =>
            {
                string name = ToParameterName(property.Name);
                return $"{property.Name} = {name}Value,";
            }));

        string signature = $$"""
            internal static {{type}} {{methodName}}(
                this {{type}} subject,
                {{parameters}})
            """;

        if (!string.IsNullOrWhiteSpace(constraints))
        {
            signature = $$"""
                {{signature}}
                    {{constraints.Indent()}}
                """;
        }

        string method = $$"""
            {{signature}}
            {
                subject.ThrowIfNull("subject");

                {{values.Indent()}}

                return new {{type}}
                {
                    {{initializers.Indent()}}
                };
            }
            """;

        return $$"""
            using System;
            using Fluentify.Internal;

            {{accessibility}} static partial class {{subject.Name}}Extensions
            {
                {{method.Indent()}}
            }
            """;
    }

    private static string ToParameterName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        return string.Concat(char.ToLowerInvariant(name[0]), name.AsSpan(1));
    }
}