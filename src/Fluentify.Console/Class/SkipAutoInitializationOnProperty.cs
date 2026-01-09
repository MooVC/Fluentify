namespace Fluentify.Console.Class;

[Fluentify]
public sealed class SkipAutoInitializationOnProperty
{
    public int Age { get; set; }

    [SkipAutoInitialization]
    public Dependent Dependency { get; set; } = new Dependent();

    public sealed class Dependent
    {
        public string Name { get; set; } = string.Empty;
    }
}