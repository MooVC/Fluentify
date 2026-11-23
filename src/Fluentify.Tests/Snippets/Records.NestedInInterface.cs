namespace Fluentify.Snippets;

public static partial class Records
{
    public const string NestedInInterfaceContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            public partial interface Outter
            {
                [Fluentify]
                public sealed partial record NestedInInterface(int Age, string Name, IReadOnlyList<object>? Attributes = default);
            }
        }
        """;

    public static readonly Declared NestedInInterface;

    public static readonly Generated NestedInInterfaceConstructor = new(
        NestedInInterfaceConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInInterface.ctor");

    public static readonly Generated NestedInInterfaceWithAgeExtensions = new(
        NestedInInterfaceWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInInterfaceExtensions.WithAge");

    public static readonly Generated NestedInInterfaceWithAttributesExtensions = new(
        NestedInInterfaceWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInInterfaceExtensions.WithAttributes");

    public static readonly Generated NestedInInterfaceWithNameExtensions = new(
        NestedInInterfaceWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInInterfaceExtensions.WithName");

    private const string NestedInInterfaceConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            partial interface Outter
            {
                partial record NestedInInterface
                {
                    #pragma warning disable CS8604

                    #if NET7_0_OR_GREATER
                    [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
                    #endif
                    public NestedInInterface()
                        : this(default(int), default(string), default(global::System.Collections.Generic.IReadOnlyList<object>?))
                    {
                    }

                    #pragma warning restore CS8604
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string NestedInInterfaceWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInInterfaceExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInInterface WithAge(this global::Fluentify.Records.Testing.Outter.NestedInInterface subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return subject with
                    {
                        Age = value,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string NestedInInterfaceWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInInterfaceExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInInterface WithAttributes(this global::Fluentify.Records.Testing.Outter.NestedInInterface subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Records.Testing.Outter.NestedInInterface WithAttributes(this global::Fluentify.Records.Testing.Outter.NestedInInterface subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.Outter.NestedInInterface WithAttributes(this global::Fluentify.Records.Testing.Outter.NestedInInterface subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.Outter.NestedInInterface WithAttributes(this global::Fluentify.Records.Testing.Outter.NestedInInterface subject, params object[] values)
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
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string NestedInInterfaceWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInInterfaceExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInInterface WithName(this global::Fluentify.Records.Testing.Outter.NestedInInterface subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return subject with
                    {
                        Name = value,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;
}