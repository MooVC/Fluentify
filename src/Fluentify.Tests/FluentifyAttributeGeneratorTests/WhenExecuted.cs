namespace Fluentify.FluentifyAttributeGeneratorTests;

public sealed class WhenExecuted
    : WhenPostInitializationOutputGeneratorIsExecuted<FluentifyAttributeGenerator>
{
    internal const string Source = $$"""
        namespace Fluentify
        {
            using System;

            [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
            internal sealed class FluentifyAttribute
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