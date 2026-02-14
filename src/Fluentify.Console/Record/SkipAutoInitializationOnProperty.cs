namespace Fluentify.Console.Record;

[Fluentify]
public sealed partial record SkipAutoInitializationOnProperty(
    int Age,
    [SkipAutoInitialization] SkipAutoInitializationOnProperty.Dependent Dependency)
{
    public sealed class Dependent
    {
        public string Name { get; set; } = string.Empty;
    }
}