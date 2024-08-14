namespace Fluentify.Snippets;

public static partial class Records
{
    public const string MultipleGenericsContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record MultipleGenerics<T1, T2, T3>(T1? Age, T2? Name, T3 Attributes)
                where T1 : struct
                where T2 : class, new()
                where T3 : IEnumerable<string>;
        }
        """;

    public static readonly Declared MultipleGenerics;

    public static readonly Generated MultipleGenericsConstructor = new(
        MultipleGenericsConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.MultipleGenerics.ctor");

    public static readonly Generated MultipleGenericsWithAgeExtensions = new(
        MultipleGenericsWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.MultipleGenericsExtensions.WithAge");

    public static readonly Generated MultipleGenericsWithAttributesExtensions = new(
        MultipleGenericsWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.MultipleGenericsExtensions.WithAttributes");

    public static readonly Generated MultipleGenericsWithNameExtensions = new(
        MultipleGenericsWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.MultipleGenericsExtensions.WithName");

    private const string MultipleGenericsConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record MultipleGenerics<T1, T2, T3>
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public MultipleGenerics()
                    : this(default(T1?), default(T2?), default(T3))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string MultipleGenericsWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class MultipleGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.MultipleGenerics<T1, T2, T3> WithAge<T1, T2, T3>(
                    this global::Fluentify.Records.Testing.MultipleGenerics<T1, T2, T3> subject,
                    T1? value)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
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

    private const string MultipleGenericsWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class MultipleGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.MultipleGenerics<T1, T2, T3> WithAttributes<T1, T2, T3>(
                    this global::Fluentify.Records.Testing.MultipleGenerics<T1, T2, T3> subject,
                    T3 value)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
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

    private const string MultipleGenericsWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class MultipleGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.MultipleGenerics<T1, T2, T3> WithName<T1, T2, T3>(
                    this global::Fluentify.Records.Testing.MultipleGenerics<T1, T2, T3> subject,
                    Func<T2, T2> builder)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    subject.ThrowIfNull("subject");
        
                    builder.ThrowIfNull("builder");
        
                    var instance = new T2();
        
                    instance = builder(instance);
        
                    return subject.WithName(instance);
                }
 
                public static global::Fluentify.Records.Testing.MultipleGenerics<T1, T2, T3> WithName<T1, T2, T3>(
                    this global::Fluentify.Records.Testing.MultipleGenerics<T1, T2, T3> subject,
                    T2? value)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
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