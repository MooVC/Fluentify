namespace Fluentify.Source;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
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
        var metadata = subject.ToMetadata();

        IReadOnlyList<Property> properties = [.. subject.Properties.OrderBy(property => property.Name)];
        string type = subject.Type.ToString();
        string constraints = string.Join("\r\n", metadata.Constraints);
        string methodName = string.Concat("With", metadata.Parameters);

        string accessibility = subject.Accessibility < Accessibility.Public
            ? "internal"
            : "public";

        string parameters = BuildParameters(properties);
        string values = BuildValues(properties);
        string initializers = BuildInitializers(properties);

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
                    {{initializers.Indent(times: 2)}}
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

    [SuppressMessage("Minor Code Smell", "S3267:Loops should be simplified with \"LINQ\" expressions", Justification = "False positive.")]
    private static string BuildInitializers(IReadOnlyList<Property> properties)
    {
        var initializers = new StringBuilder();

        foreach (Property property in properties)
        {
            string name = ToParameterName(property.Name);

            initializers = initializers.AppendLine($"{property.Name} = {name}Value,");
        }

        return initializers.ToString().TrimEnd();
    }

    private static string BuildParameters(IReadOnlyList<Property> properties)
    {
        var parameters = new StringBuilder();

        for (int index = 0; index < properties.Count; index++)
        {
            Property property = properties[index];
            string name = ToParameterName(property.Name);

            if (index > 0)
            {
                parameters = parameters.AppendLine(",");
                parameters = parameters.Append("    ");
            }

            parameters = parameters.Append($"Func<{property.Kind}> {name} = default");
        }

        return parameters.ToString();
    }

    [SuppressMessage("Minor Code Smell", "S3267:Loops should be simplified with \"LINQ\" expressions", Justification = "False positive.")]
    private static string BuildValues(IReadOnlyList<Property> properties)
    {
        var values = new StringBuilder();

        foreach (Property property in properties)
        {
            string name = ToParameterName(property.Name);
            string valueName = $"{name}Value";
            values = values.AppendLine($"var {valueName} = ReferenceEquals({name}, null) ? subject.{property.Name} : {name}();");
        }

        return values.ToString().TrimEnd();
    }

    private static string ToParameterName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        name = string.Concat(char.ToLowerInvariant(name[0]), name.Substring(1));

        if (Keywords.IsReserved(name))
        {
            return string.Concat("@", name);
        }

        return name;
    }
}