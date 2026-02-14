namespace Fluentify.Console.Class;

[Fluentify]
public sealed class SkipAutoInitializationOnType
{
    public int Age { get; set; }

    public Dependent Dependency { get; set; } = new Dependent();

    [SkipAutoInitialization]
    public sealed class Dependent
    {
        public string Name { get; set; } = string.Empty;
    }
}