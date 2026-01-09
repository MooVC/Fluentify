namespace Fluentify.Snippets;

public static class Attributes
{
    public static readonly Generated Descriptor = new(
        DescriptorAttributeGenerator.Source,
        typeof(DescriptorAttributeGenerator),
        "DescriptorAttribute");

    public static readonly Generated Fluentify = new(
        $$"""
            namespace Fluentify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
                internal sealed class FluentifyAttribute
                    : Attribute
                {
                }
            }
            """,
        typeof(FluentifyAttributeGenerator),
        "FluentifyAttribute");

    public static readonly Generated Ignore = new(
        $$"""
            namespace Fluentify
            {
                using System;

                [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
                internal sealed class IgnoreAttribute
                    : Attribute
                {
                }
            }
            """,
        typeof(IgnoreAttributeGenerator),
        "IgnoreAttribute");

    public static readonly Generated SkipAutoInitialization = new(
        $$"""
            namespace Fluentify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
                internal sealed class SkipAutoInitializationAttribute
                    : Attribute
                {
                }
            }
            """,
        typeof(SkipAutoInitializationAttributeGenerator),
        "SkipAutoInitializationAttribute");
}