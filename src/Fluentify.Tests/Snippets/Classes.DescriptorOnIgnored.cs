namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string DescriptorOnIgnoredContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class DescriptorOnIgnored
            {
                public int Age { get; set; }

                [Descriptor("Named"), Ignore]
                public string Name { get; set; }

                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared DescriptorOnIgnored;

    public static readonly Generated DescriptorOnIgnoredWithAgeExtensions = new(
        DescriptorOnIgnoredWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnIgnoredExtensions.WithAge");

    public static readonly Generated DescriptorOnIgnoredWithAttributesExtensions = new(
        DescriptorOnIgnoredWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnIgnoredExtensions.WithAttributes");

    private const string DescriptorOnIgnoredWithAgeExtensionsContent = """
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

            public static partial class DescriptorOnIgnoredExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnIgnored WithAge(
                    this global::Fluentify.Classes.Testing.DescriptorOnIgnored subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.DescriptorOnIgnored
                    {
                        Age = value,
                        Name = subject.Name,
                        Attributes = subject.Attributes,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string DescriptorOnIgnoredWithAttributesExtensionsContent = """
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

            public static partial class DescriptorOnIgnoredExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnIgnored WithAttributes(
                    this global::Fluentify.Classes.Testing.DescriptorOnIgnored subject,
                    params object[] values)
                {
                    subject.ThrowIfNull("subject");

                    global::System.Collections.Generic.IReadOnlyList<object> value = values;

                    if (subject.Attributes != null)
                    {
                        value = subject.Attributes
                            .Union(values)
                            .ToArray();
                    }

                    return new global::Fluentify.Classes.Testing.DescriptorOnIgnored
                    {
                        Age = subject.Age,
                        Name = subject.Name,
                        Attributes = value,
                    };
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnIgnored WithAttributes(
                    this global::Fluentify.Classes.Testing.DescriptorOnIgnored subject,
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
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;
}