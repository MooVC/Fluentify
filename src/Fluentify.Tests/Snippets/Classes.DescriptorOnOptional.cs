namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string DescriptorOnOptionalContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class DescriptorOnOptional
            {
                public int Age { get; set; }

                public string Name { get; set; }

                [Descriptor("AttributedWith")]
                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared DescriptorOnOptional;

    public static readonly Generated DescriptorOnOptionalWithAgeExtensions = new(
        DescriptorOnOptionalWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnOptionalExtensions.WithAge");

    public static readonly Generated DescriptorOnOptionalAttributedWithExtensions = new(
        DescriptorOnOptionalAttributedWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnOptionalExtensions.AttributedWith");

    public static readonly Generated DescriptorOnOptionalWithNameExtensions = new(
        DescriptorOnOptionalWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnOptionalExtensions.WithName");

    private const string DescriptorOnOptionalWithAgeExtensionsContent = """
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

            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnOptional WithAge(
                    this global::Fluentify.Classes.Testing.DescriptorOnOptional subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.DescriptorOnOptional
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

    private const string DescriptorOnOptionalAttributedWithExtensionsContent = """
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

            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnOptional AttributedWith(
                    this global::Fluentify.Classes.Testing.DescriptorOnOptional subject,
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

                    return new global::Fluentify.Classes.Testing.DescriptorOnOptional
                    {
                        Age = subject.Age,
                        Name = subject.Name,
                        Attributes = value,
                    };
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnOptional AttributedWith(
                    this global::Fluentify.Classes.Testing.DescriptorOnOptional subject,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.AttributedWith(instance);
                }
            }
        }

        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string DescriptorOnOptionalWithNameExtensionsContent = """
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

            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnOptional WithName(
                    this global::Fluentify.Classes.Testing.DescriptorOnOptional subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.DescriptorOnOptional
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