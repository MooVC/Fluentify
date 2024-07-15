


# Fluentify [![NuGet](https://img.shields.io/nuget/v/Fluentify?logo=nuget)](https://www.nuget.org/packages/Fluentify/) [![GitHub](https://img.shields.io/github/license/MooVC/Fluentify)](LICENSE.md)

Fluentify is a .NET Roslyn Source Generator designed to automate the creation of Fluent APIs. This tool enables engineers to rapidly develop rich, expressive, and maintainable APIs with ease. Utilizing Fluentify allows for cleaner code, easier maintenance, and more expressive interactions within your C# .NET applications.

## Installation

To install Fluentify, use the following command in your package manager console:

```shell
install-package Fluentify
```

## Usage

Fluentify automatically creates extension methods for each property on types that have the `Fluentify` attribute, supporting both ``class`` and ``record`` types.

### Class Type Usage

Class types are supported as long as the type has an accessible default constructor.

```csharp
[Fluentify]
public class Person
{
    public ushort Age { get; init; }
    public string[] Aliases { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public Name Name { get; init; }
}
```

### Record Type Usage

Record types are supported without the need for any special provisions

```csharp
[Fluentify]
public record Person(ushort Age, string[] Aliases, DateOnly DateOfBirth, Name Name);
```

Marking the ``record`` type as ``partial`` will generate a default constructor, allowing for the ``record`` to be instantiated without first initializing the properties.

```csharp
[Fluentify]
public partial record Person(ushort Age, string[] Aliases, DateOnly DateOfBirth, Name Name);

// Allows for instantiation without property initialization
var person = new Person();
...
```

## Immutability

The generated extension methods preserve immutability, providing a new instance with the specified value applied to the associated property.

```csharp
var original = new Person { Age = 42 };
var @new = original.WithAge(75);

Console.WriteLine(original.Age); // Displays 42
Console.WriteLine(@new.Age);     // Displays 75
```

## Auto Instantiation 

The value associated with a given property can be automatically instantiated, as long as that type associated with the property adheres to the ``new()`` constraint. A second extension method is generated for the property, accepting a `Builder<T>` delegate as its parameter, which allows for the newly instantiated value to be configured before being applied.

```csharp
_ = person.WithName(name => name
    .WithGiven("Avery")
    .WithFamily("Brooks"));
```

## Collection Parameterization 

Values can be appended to a list as long as the property type is ``T[]``, ``IEnumerable<T>``, ``IReadOnlyCollection<T>``, ``IReadOnlyList<T>``. Property types that derive from ``ICollection<T>`` and adhere to the ``new()`` constraint are also supported. Unlike with scalar properties, the generated extension method accepts a ``params T[]``, allowing for one or more values to be specified in a single invocation.

```csharp
var original = new Person { Aliases = ["Avery Franklin"] };
var @new = original.WithAliases("Benjamin Sisko");

Console.WriteLine(original.Aliases.Length); // Displays 1
Console.WriteLine(@new.Aliases.Length);     // Displays 2
```

## Custom Descriptors

The name of the generated extension method(s) can be customized via the `Descriptor` attribute.

### Class Type Usage

```csharp
[Fluentify]
public class Person
{
    [Descriptor("Aged")]
    public ushort Age { get; init; }

    [Descriptor("BornOn")]
    public DateOnly DateOfBirth { get; init; }

    [Descriptor("Named")]
    public Name Name { get; init; }
}
```

### Record Type Usage

```csharp
[Fluentify]
public record Person(
  [Descriptor("Aged")] ushort Age,
  [Descriptor("BornOn")] DateOnly DateOfBirth,
  [Descriptor("Named")] Name Name);
```

This allows for greater alignment with domain semantics:

```csharp
_ = person
    .Aged(75)
    .Named(name => name
        .WithGiven("Avery")
        .WithFamily("Brooks"))
    .BornOn(new DateOnly(1948, 10, 2));
```

When no custom descriptor is specified, the extension method(s) will use the following pattern for all property types, except ``bool``:

``With{PropertyName}``

For ``bool``, the extension method will utilize the same name as the property.

## Property Exclusion

Specific properties can be excluded from generating Fluentify extension method(s) using the `Ignore` attribute:

### Class Type Usage

```csharp
[Fluentify]
public sealed class Person
{
    [Ignore]
    public ushort Age { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public Name Name { get; init; }
}
```

### Record Type Usage

```csharp
[Fluentify]
public record Person([Ignore] ushort Age, DateOnly DateOfBirth, Name Name);
```

This will result in an error if you try to use the ignored property in the chain:

```csharp
_ = person
    .WithAge(75) // IntelliSense Error: 'Person' does not contain a definition for 'WithAge'
    .WithName(name => name
        .WithGiven("Avery")
        .WithFamily("Brooks"))
    .WithDateOfBirth(new DateOnly(1948, 10, 2));
```

## Analyzers

Fluentify includes several analyzers to assist engineers with its usage. These are:

Rule ID                                                                                | Category | Severity | Notes
---------------------------------------------------------------------------------------|----------|----------|-------
[FLTFY01](docs/rules/FLTFY01.md) | Design   | Warning  | Class must have an accessible parameterless constructor to use Fluentify
[FLTFY02](docs/rules/FLTFY02.md) | Usage    | Info     | Descriptor is disregarded from consideration by Fluentify
[FLTFY03](docs/rules/FLTFY03.md) | Usage    | Info     | Type does not utilize Fluentify
[FLTFY04](docs/rules/FLTFY04.md) | Naming   | Warning  | Descriptor must adhere to the naming conventions for Methods
[FLTFY05](docs/rules/FLTFY05.md) | Usage    | Info     | Type does not utilize Fluentify
[FLTFY06](docs/rules/FLTFY06.md) | Usage    | Info     | Property is already disregarded from consideration by Fluentify

## How to Apply in Practice

If you are unfamiliar with Fluent Builder pattern, please review [Building Complex Objects in a Simple Way with C#](https://www.youtube.com/watch?v=kjxf3T4tRh4) by [Gui Ferreira](https://www.youtube.com/@gui.ferreira).

If we take the example of the `MovieBuilder` and apply Fluentify, it may look like this:

```csharp
[Fluentify]
public partial record Actor(
    [Descriptor("BornIn")] int Birthday,
    string FirstName,
    string Surname);

[Fluentify]
public partial record Movie(
    Actor[] Actors,
    [Descriptor("OfGenre")] Genre Genre,
    [Descriptor("ReleasedOn")] DateOnly ReleasedOn,
    string Title);
```
In this example, we did not need to create the various `With` methods, nor did we need to explicitly create the `Build` method, significantly reducing the effort required by the engineer to support the highly expressive Fluent approach to building the `Movie` instance, demonstrated as follows:

```csharp
var actual = new Movie()
   .OfGenre(Genre.SciFi)
   .WithTitle("Star Trek: First Contact")
   .ReleasedOn(new DateOnly(1996, 12, 13))
   .WithActors(actor => actor
       .WithFirstName("Patrick")
       .WithSurname("Stewart")
       .BornIn(1940));
```

Naturally, using Fluentify does not preclude engineers from adding additional methods to support building, and this will often be required if you choose to adopt the guided builder approach, or if specific validations or conversions are required before the final instance can be build. For example:
```csharp
public class MyService
{
    public MyService(string connectionString, TimeSpan timeout)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
        ArgumentOutOfRangeException.ThrowIfLessThan(timeout.TotalSeconds, 1);

        ConnectionString = connectionString;
        Timeout = timeout;
    }

    public string ConnectionString { get; }

    public TimeSpan Timeout { get; }
}

[Fluentify]
public partial record MyServiceBuilder(
    [Descriptor("ConnectsTo")]string ConnectionString,
    [Descriptor("Waits")] int Timeout)
{
    public static MyServiceBuilder Empty => new();
    
    public MyService Build()
    {
        return new MyService(ConnectionString, TimeSpan.FromSeconds(Timeout));
    }
}
```
In this example, a new instance of `MyService` can be created as follows:

```csharp
MyService service = MyServiceBuilder
    .Empty
    .ConnectsTo("Some Connection String")
    .Waits(30)
    .Build();
```

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues to suggest improvements or add new features.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.