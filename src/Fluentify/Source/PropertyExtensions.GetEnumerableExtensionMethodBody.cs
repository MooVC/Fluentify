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
        var initialization = Initialization.GetInitialization(kind.Type.Name);

        return $$"""
            values.ThrowIfNull("values");

            {{initialization.Create(kind, property.Name)}}

            {{body}}
            """;
    }

    private sealed class Initialization
    {
        private const string CollectionInterface = "global::System.Collections.Generic.ICollection<";
        private const string ImmutableArray = "global::System.Collections.Immutable.ImmutableArray";
        private const string ImmutableHashSet = "global::System.Collections.Immutable.ImmutableHashSet";
        private const string ImmutableList = "global::System.Collections.Immutable.ImmutableList";
        private const string ImmutableSortedSet = "global::System.Collections.Immutable.ImmutableSortedSet";
        private const string ListInterface = "global::System.Collections.Generic.IList<";

        private static readonly Initialization _default = new(matchPrefix: string.Empty, initialization: "values", merge: "ToArray");

        private static readonly Initialization[] _candidates =
        [
            new(CollectionInterface, "values.ToList()", "ToList"),
            new(ImmutableArray, "AddRange", usesDefaultCheck: true),
            new(ImmutableHashSet, "Union"),
            new(ImmutableList, "AddRange"),
            new(ImmutableSortedSet, "Union"),
            new(ListInterface, "values.ToList()", "ToList"),
        ];

        private Initialization(string matchPrefix, string initialization, string merge)
        {
            MatchPrefix = matchPrefix;
            InitializationValue = initialization;
            Merge = merge;
        }

        private Initialization(string immutableTypeName, string method, bool usesDefaultCheck = false)
        {
            MatchPrefix = $"{immutableTypeName}<";
            ImmutableTypeName = immutableTypeName;
            Method = method;
            UsesDefaultCheck = usesDefaultCheck;
        }

        private string ImmutableTypeName { get; } = string.Empty;

        private string InitializationValue { get; } = string.Empty;

        private string MatchPrefix { get; }

        private string Merge { get; } = string.Empty;

        private string Method { get; } = string.Empty;

        private bool UsesDefaultCheck { get; }

        public static Initialization GetInitialization(string type)
        {
            Initialization? initialization = _candidates.FirstOrDefault(candidate => type.StartsWith(candidate.MatchPrefix, StringComparison.Ordinal));

            if (initialization is not null)
            {
                return initialization;
            }

            return _default;
        }

        public string Create(Kind kind, string propertyName)
        {
            if (!string.IsNullOrWhiteSpace(Method))
            {
                return $$"""
                    {{kind}} value = {{ImmutableTypeName}}.CreateRange(values);

                    if ({{GetSubjectCheck(propertyName)}})
                    {
                        value = subject.{{propertyName}}.{{Method}}(values);
                    }
                    """;
            }

            return $$"""
                {{kind}} value = {{InitializationValue}};

                if (subject.{{propertyName}} != null)
                {
                    value = subject.{{propertyName}}
                        .Union(values)
                        .{{Merge}}();
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
    }
}