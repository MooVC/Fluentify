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

        string initialization = Initialization.GetInitialization(kind, property)
            ?? GetStandardInitialization(kind, property);

        return $$"""
            {{initialization}}

            {{body}}
            """;
    }

    private static string GetStandardInitialization(Kind kind, Property property)
    {
        return $$"""
            {{kind}} value = values;

            if (subject.{{property.Name}} != null)
            {
                value = subject.{{property.Name}}
                    .Union(values)
                    .ToArray();
            }
            """;
    }

    private sealed class Initialization
    {
        private const string Namespace = "global::System.Collections.Immutable.";

        private static readonly Initialization[] _candidates =
        [
            new("ImmutableArray", "AddRange", usesDefaultCheck: true),
            new("ImmutableList", "AddRange"),
            new("ImmutableHashSet", "Union"),
            new("ImmutableSortedSet", "Union"),
        ];

        private Initialization(string name, string method, bool usesDefaultCheck = false)
        {
            Name = name;
            Method = method;
            UsesDefaultCheck = usesDefaultCheck;
        }

        public string Name { get; }

        public string Method { get; }

        public bool UsesDefaultCheck { get; }

        public static string? GetInitialization(Kind kind, Property property)
        {
            Initialization? candidate = _candidates.FirstOrDefault(initialization => initialization.IsMatch(kind.Type.Name));

            if (candidate is not null)
            {
                return candidate.Create(kind, property.Name);
            }

            return default;
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
                return $"!subject.{propertyName}.IsDefault";
            }

            return $"subject.{propertyName} != null";
        }

        private bool IsMatch(string type)
        {
            return type.StartsWith($"{Namespace}{Name}", StringComparison.Ordinal);
        }
    }
}