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

    public static readonly Generated SingleGenericWithExtensions = new(
        SingleGenericWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SingleGenericExtensions.With");

    private const string SingleGenericWithAgeExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif

        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SingleGenericExtensions
            {
                public static global::Fluentify.Classes.Testing.SingleGeneric<T> WithAge<T>(
                    this global::Fluentify.Classes.Testing.SingleGeneric<T> subject,
                    int value)
                    where T : global::System.Collections.IEnumerable
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SingleGeneric<T>
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }

        #pragma warning restore CS8625

        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string SingleGenericWithAttributesExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif

        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SingleGenericExtensions
            {
                public static global::Fluentify.Classes.Testing.SingleGeneric<T> WithAttributes<T>(
                    this global::Fluentify.Classes.Testing.SingleGeneric<T> subject,
                    Func<T, T> builder)
                    where T : global::System.Collections.IEnumerable
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = subject.Attributes;

                    if (ReferenceEquals(instance, null))
                    {
                        throw new NotSupportedException();
                    }

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SingleGeneric<T> WithAttributes<T>(
                    this global::Fluentify.Classes.Testing.SingleGeneric<T> subject,
                    T value)
                    where T : global::System.Collections.IEnumerable
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SingleGeneric<T>
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }

        #pragma warning restore CS8625

        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string SingleGenericWithNameExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif

        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SingleGenericExtensions
            {
                public static global::Fluentify.Classes.Testing.SingleGeneric<T> WithName<T>(
                    this global::Fluentify.Classes.Testing.SingleGeneric<T> subject,
                    string value)
                    where T : global::System.Collections.IEnumerable
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SingleGeneric<T>
                    {
                        Age = subject.Age,
                        Attributes = subject.Attributes,
                        Name = value,
                    };
                }
            }
        }

        #pragma warning restore CS8625

        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string SingleGenericWithExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif

        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using Fluentify.Internal;

            public static partial class SingleGenericExtensions
            {
                internal static global::Fluentify.Classes.Testing.SingleGeneric<T> With<T>(
                    this global::Fluentify.Classes.Testing.SingleGeneric<T> subject,
                    Func<int> age = default,
                    Func<T> attributes = default,
                    Func<string> name = default)
                    where T : global::System.Collections.IEnumerable
                {
                    subject.ThrowIfNull("subject");

                    var ageValue = ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = ReferenceEquals(name, null) ? subject.Name : name();

                    return new global::Fluentify.Classes.Testing.SingleGeneric<T>
                    {
                        Age = ageValue,
                        Attributes = attributesValue,
                        Name = nameValue,
                    };
                }
            }
        }

        #pragma warning restore CS8625

        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;
}