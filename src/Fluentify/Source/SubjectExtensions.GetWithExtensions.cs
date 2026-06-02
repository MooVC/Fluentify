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

        string accessibility = subject.Accessibility < Accessibility.Public
            ? "internal"
            : "public";

        IReadOnlyList<Property> properties = [.. subject.Properties.OrderBy(property => property.Name)];
        string constraints = string.Join("\r\n", metadata.Constraints);
        string extensionName = subject.GetExtensionClassName();
        string initializers = BuildInitializers(properties);
        string methodName = string.Concat("With", metadata.Parameters);
        string parameters = BuildParameters(properties);
        string type = subject.Type.ToString(subject.SupportsNullableReferenceTypes);
        string values = BuildValues(properties);

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

            {{accessibility}} static partial class {{extensionName}}
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
        var checks = new StringBuilder();
        var values = new StringBuilder();

        foreach (Property property in properties)
        {
            string name = ToParameterName(property.Name);
            string valueName = $"{name}Value";
            string parameterName = name.TrimStart('@');
            string check = property.GetValueNullCheck(valueName, parameterName);

            values = values.AppendLine($"var {valueName} = ReferenceEquals({name}, null) ? subject.{property.Name} : {name}();");

            if (!string.IsNullOrEmpty(check))
            {
                checks = checks.AppendLine(check);
            }
        }

        string assignments = values.ToString().TrimEnd();
        string assertions = checks.ToString().TrimEnd();

        if (string.IsNullOrEmpty(assertions))
        {
            return assignments;
        }

        return $$"""
            {{assignments}}

            {{assertions}}
            """;
    }

    private static string ToParameterName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        name = string.Concat(char.ToLowerInvariant(name[0]), name.Substring(1));

        if (string.Equals(name, "subject", StringComparison.Ordinal))
        {
            return "subject1";
        }

        if (Keywords.IsReserved(name))
        {
            return string.Concat("@", name);
        }

        return name;
    }
}