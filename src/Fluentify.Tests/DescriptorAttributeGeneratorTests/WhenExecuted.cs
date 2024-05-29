namespace Fluentify.DescriptorAttributeGeneratorTests;

using static Fluentify.DescriptorAttributeGenerator;

public sealed class WhenExecuted
    : WhenPostInitializationOutputGeneratorIsExecuted<DescriptorAttributeGenerator>
{
    public WhenExecuted()
        : base(Source)
    {
    }
}