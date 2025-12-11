namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SingleGenericContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections;

            [Fluentify]
            public sealed class SingleGeneric<T>
                where T : IEnumerable
            {
                public int Age { get; set; }

                public string Name { get; set; }

                public T Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared SingleGeneric;

    public static readonly Generated SingleGenericWithAgeExtensions = new(
        SingleGenericWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SingleGenericExtensions.WithAge");

    public static readonly Generated SingleGenericWithAttributesExtensions = new(
        SingleGenericWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SingleGenericExtensions.WithAttributes");

    public static readonly Generated SingleGenericWithNameExtensions = new(
        SingleGenericWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SingleGenericExtensions.WithName");

    private const string SingleGenericWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SingleGenericExtensions
            {
                public static global::Fluentify.Records.Testing.SingleGeneric<T> WithAge<T>(
                    this global::Fluentify.Records.Testing.SingleGeneric<T> subject,
                    int value)
                    where T : global::System.Collections.IEnumerable
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

    private const string SingleGenericWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SingleGenericExtensions
            {
                public static global::Fluentify.Records.Testing.SingleGeneric<T> WithAttributes<T>(
                    this global::Fluentify.Records.Testing.SingleGeneric<T> subject,
                    Func<T, T> builder)
                    where T : global::System.Collections.IEnumerable
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = subject.Attributes;

                    if (instance is null)
                    {
                        throw new NotSupportedException();
                    }

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.SingleGeneric<T> WithAttributes<T>(
                    this global::Fluentify.Records.Testing.SingleGeneric<T> subject,
                    T? value)
                    where T : global::System.Collections.IEnumerable
                {
                    subject.ThrowIfNull("subject");

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

    private const string SingleGenericWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SingleGenericExtensions
            {
                public static global::Fluentify.Records.Testing.SingleGeneric<T> WithName<T>(
                    this global::Fluentify.Records.Testing.SingleGeneric<T> subject,
                    string value)
                    where T : global::System.Collections.IEnumerable
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