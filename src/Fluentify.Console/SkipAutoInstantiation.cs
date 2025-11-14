namespace Fluentify.Records.Testing;

[Fluentify]
public sealed partial record SkipAutoInstantiation(
    int Age,
    [SkipAutoInstantiation] SkipAutoInstantiation.Dependent Dependency)
{
    public sealed class Dependent
    {
        public string Name { get; set; }
    }
}