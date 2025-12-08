namespace Fluentify.Snippets;

public static partial class Records
{
    public const string GlobalContent = """
        using System.Collections.Generic;
        using Fluentify;

        [Fluentify]
        public sealed partial record Global(int Age, string Name, IReadOnlyList<object>? Attributes = default);
        """;

    public static readonly Declared Global;

    public static readonly Generated GlobalConstructor = new(
        GlobalConstructorContent,
        typeof(RecordGenerator),
        "Global.ctor");

    public static readonly Generated GlobalWithAgeExtensions = new(
        GlobalWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "GlobalExtensions.WithAge");

    public static readonly Generated GlobalWithAttributesExtensions = new(
        GlobalWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "GlobalExtensions.WithAttributes");

    public static readonly Generated GlobalWithNameExtensions = new(
        GlobalWithNameExtensionsContent,
        typeof(RecordGenerator),
        "GlobalExtensions.WithName");

    private const string GlobalConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        using System.Diagnostics.CodeAnalysis;

        partial record Global
        {
            #pragma warning disable CS8604

            #if NET7_0_OR_GREATER
            [SetsRequiredMembers]
            #endif
            public Global()
                : this(default(int), default(string), default(global::System.Collections.Generic.IReadOnlyList<object>?))
            {
            }

            #pragma warning restore CS8604
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string GlobalWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        using System;
        using System.Collections.Generic;
        using System.Linq;
        using Fluentify.Internal;

        public static partial class GlobalExtensions
        {
            public static global::Global WithAge(
                this global::Global subject,
                int value)
            {
                subject.ThrowIfNull("subject");

                return subject with
                {
                    Age = value,
                };
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string GlobalWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        using System;
        using System.Collections.Generic;
        using System.Linq;
        using Fluentify.Internal;

        public static partial class GlobalExtensions
        {
            public static global::Global WithAttributes(
                this global::Global subject,
                params object[] values)
            {
                subject.ThrowIfNull("subject");

                global::System.Collections.Generic.IReadOnlyList<object>? value = values;

                if (subject.Attributes != null)
                {
                    value = subject.Attributes
                        .Union(values)
                        .ToArray();
                }

                return subject with
                {
                    Attributes = value,
                };
            }

            public static global::Global WithAttributes(
                this global::Global subject,
                Func<object, object> builder)
            {
                subject.ThrowIfNull("subject");
                builder.ThrowIfNull("builder");

                var instance = subject.Attributes?.FirstOrDefault();

                if (instance is null)
                {
                    instance = new object();
                }

                instance = builder(instance);

                return subject.WithAttributes(instance);
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string GlobalWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        using System;
        using System.Collections.Generic;
        using System.Linq;
        using Fluentify.Internal;

        public static partial class GlobalExtensions
        {
            public static global::Global WithName(
                this global::Global subject,
                string value)
            {
                subject.ThrowIfNull("subject");

                return subject with
                {
                    Name = value,
                };
            }
        }
        
        #pragma warning restore CS8625
        #nullable restore
        """;
}