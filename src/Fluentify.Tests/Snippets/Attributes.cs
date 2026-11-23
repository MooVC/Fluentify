namespace Fluentify.Snippets;

public static class Attributes
{
    public static readonly Generated AutoInitializeWith = new(
        """
            namespace Fluentify
            {
                [global::System.AttributeUsageAttribute(
                    global::System.AttributeTargets.Class | global::System.AttributeTargets.Parameter | global::System.AttributeTargets.Property,
                    Inherited = false,
                    AllowMultiple = false)]
                internal sealed class AutoInitializeWithAttribute
                    : global::System.Attribute
                {
                    public AutoInitializeWithAttribute()
                    {
                        Factory = string.Empty;
                    }

                    public AutoInitializeWithAttribute(string factory)
                    {
                        Factory = factory;
                    }

                    public string Factory { get; }
                }
            }
            """,
        typeof(AutoInitializeWithAttributeGenerator),
        "AutoInitializeWithAttribute");

    public static readonly Generated Descriptor = new(
        """
            namespace Fluentify
            {
                [global::System.AttributeUsageAttribute(
                    global::System.AttributeTargets.Parameter | global::System.AttributeTargets.Property,
                    Inherited = false,
                    AllowMultiple = false)]
                internal sealed class DescriptorAttribute
                    : global::System.Attribute
                {
                    public DescriptorAttribute()
                    {
                        Value = string.Empty;
                    }

                    public DescriptorAttribute(string value)
                    {
                        Value = value;
                    }

                    public string Value { get; }
                }
            }
            """,
        typeof(DescriptorAttributeGenerator),
        "DescriptorAttribute");

    public static readonly Generated Fluentify = new(
        """
            namespace Fluentify
            {
                [global::System.AttributeUsageAttribute(global::System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
                internal sealed class FluentifyAttribute
                    : global::System.Attribute
                {
                }
            }
            """,
        typeof(FluentifyAttributeGenerator),
        "FluentifyAttribute");

    public static readonly Generated Hide = new(
        """
            namespace Fluentify
            {
                [global::System.AttributeUsageAttribute(
                    global::System.AttributeTargets.Parameter | global::System.AttributeTargets.Property,
                    Inherited = false,
                    AllowMultiple = false)]
                internal sealed class HideAttribute
                    : global::System.Attribute
                {
                }
            }
            """,
        typeof(HideAttributeGenerator),
        "HideAttribute");

    public static readonly Generated Ignore = new(
        """
            namespace Fluentify
            {
                [global::System.AttributeUsageAttribute(
                    global::System.AttributeTargets.Parameter | global::System.AttributeTargets.Property,
                    Inherited = false,
                    AllowMultiple = false)]
                internal sealed class IgnoreAttribute
                    : global::System.Attribute
                {
                }
            }
            """,
        typeof(IgnoreAttributeGenerator),
        "IgnoreAttribute");

    public static readonly Generated SkipAutoInitialization = new(
        """
            namespace Fluentify
            {
                [global::System.AttributeUsageAttribute(
                    global::System.AttributeTargets.Class | global::System.AttributeTargets.Parameter | global::System.AttributeTargets.Property | global::System.AttributeTargets.Struct,
                    Inherited = false,
                    AllowMultiple = false)]
                internal sealed class SkipAutoInitializationAttribute
                    : global::System.Attribute
                {
                }
            }
            """,
        typeof(SkipAutoInitializationAttributeGenerator),
        "SkipAutoInitializationAttribute");
}