namespace Fluentify.Snippets;

public static partial class Records
{
    public const string SimpleWithDefaultConstructorContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;
            using System.Diagnostics.CodeAnalysis;

            [Fluentify]
            public sealed partial record SimpleWithDefaultConstructor(int Age, string Name, IReadOnlyList<object>? Attributes = default)
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public SimpleWithDefaultConstructor()
                    : this(default(int), default(string), default(global::System.Collections.Generic.IReadOnlyList<object>?))
                {
                }

                #pragma warning restore CS8604
            }
        }
        """;

    public static readonly Declared SimpleWithDefaultConstructor;

    public static readonly Generated SimpleWithDefaultConstructorWithAgeExtensions = new(
        SimpleWithDefaultConstructorWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleWithDefaultConstructorExtensions.WithAge");

    public static readonly Generated SimpleWithDefaultConstructorWithAttributesExtensions = new(
        SimpleWithDefaultConstructorWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleWithDefaultConstructorExtensions.WithAttributes");

    public static readonly Generated SimpleWithDefaultConstructorWithNameExtensions = new(
        SimpleWithDefaultConstructorWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleWithDefaultConstructorExtensions.WithName");

    private const string SimpleWithDefaultConstructorWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleWithDefaultConstructorExtensions
            {
                public static global::Fluentify.Records.Testing.SimpleWithDefaultConstructor WithAge(
                    this global::Fluentify.Records.Testing.SimpleWithDefaultConstructor subject,
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

    private const string SimpleWithDefaultConstructorWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleWithDefaultConstructorExtensions
            {
                public static global::Fluentify.Records.Testing.SimpleWithDefaultConstructor WithAttributes(
                    this global::Fluentify.Records.Testing.SimpleWithDefaultConstructor subject,
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

                public static global::Fluentify.Records.Testing.SimpleWithDefaultConstructor WithAttributes(
                    this global::Fluentify.Records.Testing.SimpleWithDefaultConstructor subject,
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

    private const string SimpleWithDefaultConstructorWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleWithDefaultConstructorExtensions
            {
                public static global::Fluentify.Records.Testing.SimpleWithDefaultConstructor WithName(
                    this global::Fluentify.Records.Testing.SimpleWithDefaultConstructor subject,
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