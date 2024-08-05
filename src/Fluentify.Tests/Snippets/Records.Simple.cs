namespace Fluentify.Snippets;

public static partial class Records
{
    public static readonly Declared Simple;

    public static readonly Generated SimpleConstructor = new(
        SimpleConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Simple.ctor");

    public static readonly Generated SimpleWithAgeExtensions = new(
        SimpleWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleExtensions.WithAge");

    public static readonly Generated SimpleWithAttributesExtensions = new(
        SimpleWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleExtensions.WithAttributes");

    public static readonly Generated SimpleWithNameExtensions = new(
        SimpleWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleExtensions.WithName");

    private const string SimpleContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record Simple(int Age, string Name, IReadOnlyList<object>? Attributes = default);
        }
        """;

    private const string SimpleConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record Simple
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public Simple()
                    : this(default, default, default)
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string SimpleWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleExtensions
            {
                public static global::Fluentify.Records.Testing.Simple WithAge(
                    this global::Fluentify.Records.Testing.Simple subject,
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

    private const string SimpleWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleExtensions
            {
                public static global::Fluentify.Records.Testing.Simple WithAttributes(
                    this global::Fluentify.Records.Testing.Simple subject,
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

                public static global::Fluentify.Records.Testing.Simple WithAttributes(
                    this global::Fluentify.Records.Testing.Simple subject,
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

    private const string SimpleWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleExtensions
            {
                public static global::Fluentify.Records.Testing.Simple WithName(
                    this global::Fluentify.Records.Testing.Simple subject,
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