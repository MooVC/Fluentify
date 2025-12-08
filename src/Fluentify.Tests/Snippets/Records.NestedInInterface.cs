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
            using System.Diagnostics.CodeAnalysis;

            partial interface Outter
            {
                partial record NestedInInterface
                {
                    #pragma warning disable CS8604

                    #if NET7_0_OR_GREATER
                    [SetsRequiredMembers]
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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInInterfaceExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInInterface WithAge(
                    this global::Fluentify.Records.Testing.Outter.NestedInInterface subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInInterfaceExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInInterface WithAttributes(
                    this global::Fluentify.Records.Testing.Outter.NestedInInterface subject,
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

                public static global::Fluentify.Records.Testing.Outter.NestedInInterface WithAttributes(
                    this global::Fluentify.Records.Testing.Outter.NestedInInterface subject,
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
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string NestedInInterfaceWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInInterfaceExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInInterface WithName(
                    this global::Fluentify.Records.Testing.Outter.NestedInInterface subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

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