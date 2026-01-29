# Fluentify [![NuGet](https://img.shields.io/nuget/v/Fluentify?logo=nuget)](https://www.nuget.org/packages/Fluentify/) [![GitHub](https://img.shields.io/github/license/MooVC/Fluentify)](LICENSE.md)

Fluentify is a .NET Roslyn Source Generator designed to automate the creation of Fluent APIs. This tool enables engineers to rapidly develop rich, expressive, and maintainable APIs with ease. Utilizing Fluentify allows for cleaner code, easier maintenance, and more expressive interactions within your C# .NET applications.

If you are unfamiliar with Fluent Builder pattern, please review [Building Complex Objects in a Simple Way with C#](https://www.youtube.com/watch?v=kjxf3T4tRh4) by [Gui Ferreira](https://www.youtube.com/@gui.ferreira). Using its example, with Fluentify, we can transform how we configure movies from this:

```csharp
var movie = new Movie
{
    Genre = Genre.SciFi,
    Title = "Star Trek: First Contact",
    ReleasedOn = new DateOnly(1996, 12, 13),
    Actors =
    [
        new Actor
        {
            Birthday = 1940,
            FirstName = "Patrick",
            Surname = "Stewart",
        },
    ],
};
```

to this:

```csharp
var movie = new Movie()
   .OfGenre(Genre.SciFi)
   .WithTitle("Star Trek: First Contact")
   .ReleasedOn(new DateOnly(1996, 12, 13))
   .WithActors(actor => actor
       .WithFirstName("Patrick")
       .WithSurname("Stewart")
       .BornIn(1940));
```

This document will use the `Movie` example to describe how the features of Fluentify can be used to make the illustrated use of the Fluent Builder pattern possible. 

## Requirements

- C# v3.0 or later.
- Visual Studio 2022 v17.0 or later, or any compatible IDE that supports Roslyn source generators.

## Installation

To install Fluentify, use the following command in your package manager console:

```shell
install-package Fluentify
```

## Usage

Fluentify automatically creates extension methods for each property on types that have the `Fluentify` attribute, supporting both `class` and `record` types.

### Record Type Usage

```csharp
[Fluentify]
public record Actor(int Birthday, string FirstName, string Surname);

[Fluentify]
public record Movie(Actor[] Actors, Genre Genre, DateOnly ReleasedOn, string Title);
```

Marking the `record` type as `partial` will generate a default constructor, allowing for the `record` to be instantiated without first initializing the properties.

```csharp
[Fluentify]
public partial record Actor(int Birthday, string FirstName, string Surname);

// Allows for instantiation without property initialization
var actor = new Actor();
...
```

### Class Type Usage

```csharp
[Fluentify]
public class Actor
{
    public int Birthday { get; init; }
    public string FirstName { get; init; }
    public string Surname { get; init; }
}

[Fluentify]
public class Movie
{
    public Actor[] Actors { get; init; }
    public Genre Genre { get; init; }
    public DateOnly ReleasedOn { get; init; }
    public string Title { get; init; }
}
```

A `class` type is supported as long as the type has an accessible default constructor (implicit or explicit).

## Immutability

The generated extension methods preserve immutability, providing a new instance with the specified value applied to the associated property.

```csharp
var original = new Actor { Birthday = 1942 };
var @new = original.WithBirthday(1975);

Console.WriteLine(original.Birthday); // Displays 1942
Console.WriteLine(@new.Birthday);     // Displays 1975
```

## Auto Initialization

The value associated with a given property can be automatically instantiated, as long as that type associated with the property adheres to the `new()` constraint. For scalar properties, a second extension method is generated, accepting a `Func<T, T>` delegate as its parameter, which allows for the existing value to be configured before being applied. If the existing value is `null` and the type is buildable, a new instance is created. If the existing value is `null` and the type cannot be automatically instantiated, a `NotSupportedException` is thrown. Delegate overloads are not generated for value types or framework types such as `string`, and collection builder overloads continue to create a new element rather than reusing the current value.

In some scenarios it may be undesirable for a given property to allow for Auto Instantiation. This feature can be disabled by applying the `SkipAutoInitialization]` attribute to the property, the corresponding primary constructor parameter, or the type referenced by the property to suppress generation of the builder overload.

```csharp
public sealed class Movie
{
    [SkipAutoInitialization]
    public Actor Lead { get; init; }
}
```

When multiple properties share the same type, it may be desirable to annotate the type to prevent the builder overload from being generated on any property that uses that type.

```csharp
[SkipAutoInitialization]
public sealed class Actor
{
    public string Name { get; init; }
}

public sealed class Movie
{
    public Actor Lead { get; init; }
    public Actor Supporting { get; init; }
}
```

In some cases, it may also be desirable that a specific instance be used instead of that provided by the default constructor, or when no default constructor exists. In these cases, use the `[AutoInitializeWith]` attribute to reference a static field, property or a parameterless static factory method that returns the target type.

```csharp
[AutoInitializeWith(nameof(Default))]
public sealed class Actor
{
    public Actor(string name)
    {
        Name = name;
    }

    public static Actor Default => new(string.Empty);

    public string Name { get; init; }
}

public sealed class Movie
{
    public Actor Lead { get; init; }
}
```

## Collection Parameterization 

Values can be appended to a list as long as the property type is `T[]`, `IEnumerable<T>`, `IReadOnlyCollection<T>`, `IReadOnlyList<T>`. Property types that derive from `ICollection<T>` and adhere to the `new()` constraint are also supported. Unlike with scalar properties, the generated extension method accepts a `params T[]`, allowing for one or more values to be specified in a single invocation.

```csharp
var original = new Movie { Actors = [picard] };
var @new = original.WithActors(worf);

Console.WriteLine(original.Actors.Length); // Displays 1
Console.WriteLine(@new.Actors.Length);     // Displays 2
```

## Custom Descriptors

By default, Fluentify will generate an extension method for each property using the `With{Property Name}` pattern for all types, with the exception of `bool`, which defaults to the declared name of the property.

The name used can be customized via the `Descriptor` attribute. When a descriptor is provided that is deemed acceptable as a method name to the compiler, it is applied to the extension method. When no descriptor is provided, the declared name of the property used.

### Record Type Usage

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
    [Descriptor] DateOnly ReleasedOn,
    string Title);
```

### Class Type Usage

```csharp
[Fluentify]
public class Actor
{
    [Descriptor("BornIn")]
    public int Birthday { get; init; }
    
    public string FirstName { get; init; }

    public string Surname { get; init; }
}

[Fluentify]
public class Movie
{
    public Actor[] Actors { get; init; }
    
    [Descriptor("OfGenre")] 
    public Genre Genre { get; init; }
    
    [Descriptor]
    public DateOnly ReleasedOn { get; init; }
    
    public string Title { get; init; }
}
```

This allows for greater alignment with domain semantics:

```csharp
var movie = new Movie()
   .OfGenre(Genre.SciFi)
   .WithTitle("Star Trek: First Contact")
   .ReleasedOn(new DateOnly(1996, 12, 13))
   .WithActors(actor => actor
       .WithFirstName("Patrick")
       .WithSurname("Stewart")
       .BornIn(1940));
```

## Property Exclusion

Specific properties can be excluded from generating Fluentify extension method(s) using the `Ignore` attribute:

### Record Type Usage

```csharp
[Fluentify]
public record Actor([Ignore] int Birthday, string FirstName, string Surname);
```

### Class Type Usage

```csharp
[Fluentify]
public class Actor
{
    [Ignore]
    public int Birthday { get; init; }
    public string FirstName { get; init; }
    public string Surname { get; init; }
}
```

This will result in an error if you try to use the ignored property in the chain:

```csharp
_ = actor
    .WithBirthday(1975) // IntelliSense Error: 'Actor' does not contain a definition for 'WithBirthday'
    .WithFirstName("Avery")
    .WithSurname("Brooks");
```

## Property Hiding

Specific properties can have their Fluentify extension method(s) scoped to `internal` by applying the `Hide` attribute:

### Record Type Usage

```csharp
[Fluentify]
public record Actor([Hide] int Birthday, string FirstName, string Surname);
```

### Class Type Usage

```csharp
[Fluentify]
public class Actor
{
    [Hide]
    public int Birthday { get; init; }
    public string FirstName { get; init; }
    public string Surname { get; init; }
}
```

When both `Hide` and `Ignore` are applied, `Ignore` takes precedence and no extension methods are generated for the property.

## Analyzers

Fluentify includes several analyzers to assist engineers with its usage. These are:

Rule ID                          | Category | Severity | Notes
:--------------------------------|:---------|:---------|:-------------------------------------------------------------------------
[FLTFY01](docs/rules/FLTFY01.md) | Design   | Warning  | Class must have an accessible parameterless constructor to use Fluentify
[FLTFY02](docs/rules/FLTFY02.md) | Usage    | Info     | Descriptor is disregarded from consideration by Fluentify
[FLTFY03](docs/rules/FLTFY03.md) | Usage    | Info     | Type does not utilize Fluentify
[FLTFY04](docs/rules/FLTFY04.md) | Naming   | Warning  | Descriptor must adhere to the naming conventions for Methods
[FLTFY05](docs/rules/FLTFY05.md) | Usage    | Info     | Type does not utilize Fluentify
[FLTFY06](docs/rules/FLTFY06.md) | Usage    | Info     | Property is already disregarded from consideration by Fluentify
[FLTFY07](docs/rules/FLTFY07.md) | Usage    | Info     | Specified descriptor is already the default used by Fluentify
[FLTFY08](docs/rules/FLTFY08.md) | Design   | Info     | Record should be partial to allow Fluentify to generate a parameterless constructor
[FLTFY11](docs/rules/FLTFY11.md) | Usage    | Info     | Type does not utilize Fluentify
[FLTFY12](docs/rules/FLTFY12.md) | Usage    | Info     | Hide is disregarded when Ignore is applied

## Building a Service

Combining Fluentify with additional, custom methods, can assist with the construction of complex types. For example:

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
    [Descriptor("ConnectsTo")] string ConnectionString,
    [Descriptor("Waits")] int Timeout)
{
    public static MyServiceBuilder Default => new();
    
    public MyService Build()
    {
        return new MyService(ConnectionString, TimeSpan.FromSeconds(Timeout));
    }
}
```

In this example, a new instance of `MyService` can be created as follows:

```csharp
MyService service = MyServiceBuilder
    .Default
    .ConnectsTo("Some Connection String")
    .Waits(30)
    .Build();
```

## Contributing

Contributions are welcome - see the [CONTRIBUTING.md](/.github/CONTRIBUTING.md) file for details.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.