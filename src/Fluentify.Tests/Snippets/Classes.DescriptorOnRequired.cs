namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string DescriptorOnRequiredContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class DescriptorOnRequired
            {
                [Descriptor("Aged")]
                public int Age { get; set; }

                public string Name { get; set; }

                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared DescriptorOnRequired;

    public static readonly Generated DescriptorOnRequiredAgedExtensions = new(
        DescriptorOnRequiredAgedExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnRequiredExtensions.Aged");

    public static readonly Generated DescriptorOnRequiredWithAttributesExtensions = new(
        DescriptorOnRequiredWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnRequiredExtensions.WithAttributes");

    public static readonly Generated DescriptorOnRequiredWithNameExtensions = new(
        DescriptorOnRequiredWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnRequiredExtensions.WithName");

    private const string DescriptorOnRequiredAgedExtensionsContent = """
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

            public static partial class DescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnRequired Aged(
                    this global::Fluentify.Classes.Testing.DescriptorOnRequired subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.DescriptorOnRequired
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

    private const string DescriptorOnRequiredWithAttributesExtensionsContent = """
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

            public static partial class DescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnRequired WithAttributes(
                    this global::Fluentify.Classes.Testing.DescriptorOnRequired subject,
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

                    return new global::Fluentify.Classes.Testing.DescriptorOnRequired
                    {
                        Age = subject.Age,
                        Name = subject.Name,
                        Attributes = value,
                    };
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnRequired WithAttributes(
                    this global::Fluentify.Classes.Testing.DescriptorOnRequired subject,
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
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string DescriptorOnRequiredWithNameExtensionsContent = """
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

            public static partial class DescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnRequired WithName(
                    this global::Fluentify.Classes.Testing.DescriptorOnRequired subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.DescriptorOnRequired
                    {
                        Age = subject.Age,
                        Name = value,
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
}