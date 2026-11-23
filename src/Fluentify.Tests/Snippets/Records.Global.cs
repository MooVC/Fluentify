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

        partial record Global
        {
            #pragma warning disable CS8604

            #if NET7_0_OR_GREATER
            [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
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

        public static partial class GlobalExtensions
        {
            public static global::Global WithAge(this global::Global subject, int value)
            {
                global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

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

        public static partial class GlobalExtensions
        {
            public static global::Global WithAttributes(this global::Global subject, global::System.Func<object, object> builder, params object[] values)
            {
                global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");
                global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                foreach (var value in values)
                {
                    subject = subject.WithAttributes(value, builder);
                }

                return subject;
            }

            public static global::Global WithAttributes(this global::Global subject, object instance, global::System.Func<object, object> builder)
            {
                global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                instance = builder(instance);

                return subject.WithAttributes(instance);
            }

            public static global::Global WithAttributes(this global::Global subject, global::System.Func<object, object> builder)
            {
                global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                var instance = new object();

                instance = builder(instance);

                return subject.WithAttributes(instance);
            }

            public static global::Global WithAttributes(this global::Global subject, params object[] values)
            {
                global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                global::System.Collections.Generic.IReadOnlyList<object>? value = values;

                if (subject.Attributes != null)
                {
                    value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                }

                return subject with
                {
                    Attributes = value,
                };
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string GlobalWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        public static partial class GlobalExtensions
        {
            public static global::Global WithName(this global::Global subject, string value)
            {
                global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

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