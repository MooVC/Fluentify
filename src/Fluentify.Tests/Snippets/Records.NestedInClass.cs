namespace Fluentify.Snippets;

public static partial class Records
{
    public const string NestedInClassContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            public sealed partial class Outter
            {
                [Fluentify]
                public sealed partial record NestedInClass(int Age, string Name, IReadOnlyList<object>? Attributes = default);
            }
        }
        """;

    public static readonly Declared NestedInClass;

    public static readonly Generated NestedInClassConstructor = new(
        NestedInClassConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInClass.ctor");

    public static readonly Generated NestedInClassWithAgeExtensions = new(
        NestedInClassWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInClassExtensions.WithAge");

    public static readonly Generated NestedInClassWithAttributesExtensions = new(
        NestedInClassWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInClassExtensions.WithAttributes");

    public static readonly Generated NestedInClassWithNameExtensions = new(
        NestedInClassWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInClassExtensions.WithName");

    private const string NestedInClassConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial class Outter
            {
                partial record NestedInClass
                {
                    #pragma warning disable CS8604

                    #if NET7_0_OR_GREATER
                    [SetsRequiredMembers]
                    #endif
                    public NestedInClass()
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

    private const string NestedInClassWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInClassExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInClass WithAge(
                    this global::Fluentify.Records.Testing.Outter.NestedInClass subject,
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

    private const string NestedInClassWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInClassExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInClass WithAttributes(
                    this global::Fluentify.Records.Testing.Outter.NestedInClass subject,
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

                public static global::Fluentify.Records.Testing.Outter.NestedInClass WithAttributes(
                    this global::Fluentify.Records.Testing.Outter.NestedInClass subject,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string NestedInClassWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInClassExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInClass WithName(
                    this global::Fluentify.Records.Testing.Outter.NestedInClass subject,
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