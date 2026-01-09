namespace Fluentify.Console.Record;

[Fluentify]
public sealed partial record SkipAutoInitializationOnType(
    int Age,
    SkipAutoInitializationOnType.Dependent Dependency)
{
    [SkipAutoInitialization]
    public sealed class Dependent
    {
        public string Name { get; set; } = string.Empty;
    }
}