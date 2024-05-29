namespace Fluentify.BuilderDelegateGeneratorTests;

using static Fluentify.BuilderDelegateGenerator;

public sealed class WhenExecuted
    : WhenPostInitializationOutputGeneratorIsExecuted<BuilderDelegateGenerator>
{
    public WhenExecuted()
        : base(Source)
    {
    }
}