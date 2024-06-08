namespace Fluentify.IgnoreAttributeGeneratorTests;

public sealed class WhenExecuted
    : WhenPostInitializationOutputGeneratorIsExecuted<IgnoreAttributeGenerator>
{
    internal const string Source = $$"""
        namespace Fluentify
        {
            using System;

            [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
            internal sealed class IgnoreAttribute
                : Attribute
            {
            }
        }
        """;

    public WhenExecuted()
        : base(Source)
    {
    }
}