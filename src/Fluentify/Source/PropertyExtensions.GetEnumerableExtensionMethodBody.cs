namespace Fluentify.Source;

using System;
using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string? GetEnumerableExtensionMethodBody(this Property property, Func<Property, string?> scalar)
    {
        if (property.Kind.Pattern != Pattern.Enumerable)
        {
            return default;
        }

        string? body = scalar(property);

        if (body is null)
        {
            return default;
        }

        Kind kind = property.Kind;

        string initialization = ImmutableInitialization.GetInitialization(kind, property)
            ?? $$"""
                {{kind}} value = values;

                if (subject.{{property.Name}} != null)
                {
                    value = subject.{{property.Name}}
                        .Union(values)
                        .ToArray();
                }
                """;

        return $$"""
            {{initialization}}

            {{body}}
            """;
    }

    private sealed record ImmutableInitialization(string Name, string Method, bool UsesDefaultCheck = false)
    {
        private const string Namespace = "global::System.Collections.Immutable.";

        public static string? GetInitialization(Kind kind, Property property)
        {
            foreach (ImmutableInitialization initialization in GetCandidates())
            {
                if (initialization.IsMatch(kind.Type.Name))
                {
                    return initialization.Create(kind, property.Name);
                }
            }

            return default;
        }

        private static ImmutableInitialization[] GetCandidates()
        {
            return new ImmutableInitialization[]
            {
                new("ImmutableArray", "AddRange", UsesDefaultCheck: true),
                new("ImmutableList", "AddRange"),
                new("ImmutableHashSet", "Union"),
                new("ImmutableSortedSet", "Union"),
            };
        }

        private string Create(Kind kind, string propertyName)
        {
            return $$"""
                {{kind}} value = {{Namespace}}{{Name}}.CreateRange(values);

                if ({{GetSubjectCheck(propertyName)}})
                {
                    value = subject.{{propertyName}}.{{Method}}(values);
                }
                """;
        }

        private string GetSubjectCheck(string propertyName)
        {
            if (UsesDefaultCheck)
            {
                return $$"!subject.{{propertyName}}.IsDefault";
            }

            return $$"subject.{{propertyName}} != null";
        }

        private bool IsMatch(string type)
        {
            return type.StartsWith($"{Namespace}{Name}", StringComparison.Ordinal);
        }
    }
}
