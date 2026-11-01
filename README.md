# Fluentify [![NuGet](https://img.shields.io/nuget/v/Fluentify?logo=nuget)](https://www.nuget.org/packages/Fluentify/) [![License](https://img.shields.io/github/license/MooVC/Fluentify)](LICENSE.md)

**Generate fluent APIs from your C# models. Stop maintaining repetitive builder methods.**

Fluentify is a Roslyn source generator for classes and records. Add `[Fluentify]` to a type and get fluent extension methods at compile time, including support for nested objects, collections, custom method names and null-aware assignments.

Use it for reusable test-data fixtures, configuration models and builder types whose fluent APIs need to stay aligned with their properties.

## The code you no longer have to maintain

A small immutable fixture can start with a few handwritten methods:

```csharp
namespace Examples;

public sealed record CustomerFixture(string Name, string Email, string Country)
{
    public static CustomerFixture Default => new(
        "Alex Murphy",
        "alex@example.com",
        "IE");

    public CustomerFixture WithName(string value)
        => this with { Name = value };

    public CustomerFixture WithEmail(string value)
        => this with { Email = value };

    public CustomerFixture WithCountry(string value)
        => this with { Country = value };
}
```

With Fluentify, the definition becomes:

```csharp
namespace Examples;

[Fluentify]
public sealed record CustomerFixture(string Name, string Email, string Country)
{
    public static CustomerFixture Default => new(
        "Alex Murphy",
        "alex@example.com",
        "IE");
}
```

```csharp
var customer = CustomerFixture.Default
    .WithName("Sam Costa")
    .WithCountry("PT");
```

Add another property and its fluent method is generated on the next build. There is no corresponding method to write or keep in sync. You still own the fixture defaults, custom behaviour and any mapping to your application types.

## Install

```shell
dotnet add package Fluentify
```

Use a compiler and IDE that support Roslyn source generators, such as Visual Studio 2022 or a compatible .NET SDK and editor. Individual examples use modern C# features; collection expressions require C# 12.

## Where Fluentify fits

| Your use case | What Fluentify contributes |
| --- | --- |
| Test-data fixtures | Reuse meaningful defaults and express each scenario through the values that change. |
| Nested configuration | Compose object graphs with generated delegate overloads. |
| Public fluent APIs | Customize method names while generating repetitive property-update methods. |
| Dedicated builders | Generate the fluent surface and keep construction and validation in your own `Build()` method. |

For a one-off object initializer, ordinary C# may already be sufficient. For a small record update, a `with` expression is often enough. Fluentify becomes useful when you want a reusable fluent API and would otherwise maintain those methods yourself.

## Compose nested objects

```csharp
namespace Examples;

[Fluentify]
public sealed class OrderDraft
{
    public string Reference { get; init; } = string.Empty;

    public DeliveryAddress DeliveryAddress { get; init; } = new();

    public OrderLine[] Lines { get; init; } = [];
}

[Fluentify]
public sealed class DeliveryAddress
{
    public string City { get; init; } = string.Empty;

    public string Country { get; init; } = "IE";
}

[Fluentify]
public sealed class OrderLine
{
    public string ProductCode { get; init; } = string.Empty;

    public int Quantity { get; init; } = 1;
}
```

```csharp
var order = new OrderDraft()
    .WithReference("ORDER-1042")
    .WithDeliveryAddress(address => address
        .WithCity("Dundalk")
        .WithCountry("IE"))
    .WithLines(line => line
        .WithProductCode("BOOK-001")
        .WithQuantity(2));
```

## Behaviour at a glance

- **Updates return a new instance.** Keep the returned value or continue the chain.
- **Collection methods append elements.** They do not replace the existing collection.
- **Nullability influences assignment guards.** Non-nullable reference assignments reject null by default; supported nullability attributes can change that behaviour.
- **Construction rules still matter.** Supported classes need an accessible parameterless constructor. Partial records can receive a generated parameterless constructor.
- **Business rules remain yours.** For domain objects with invariants, use a separate builder and construct the valid object in your own `Build()` method.
- **Returning copies is not a deep-immutability guarantee.** Consider the mutability of referenced objects and collections. Measure allocations for performance-sensitive workloads.

## Reference

- [Classes and records](#classes-and-records)
- [Immutability](#immutability)
- [Null assignment behaviour](#null-assignment-behavior)
- [Auto initialization](#auto-initialization)
- [Collections](#collection-parameterization)
- [Custom method names](#custom-descriptors)
- [Excluding properties](#property-exclusion)
- [Internal methods](#property-hiding)
- [Diagnostics](#analyzers)
- [Building a service](#building-a-service)

## Classes and records

Fluentify automatically creates extension methods for each property on types that have the `Fluentify` attribute, supporting both `class` and `record` types.

### Record Type Usage

```csharp
[Fluentify]
public sealed record Actor(int Birthday, string FirstName, string Surname);

[Fluentify]
public sealed record Movie(Actor[] Actors, Genre Genre, DateOnly ReleasedOn, string Title);
```

Marking the `record` type as `partial` will generate a default constructor, allowing for the `record` to be instantiated without first initializing the properties.

```csharp
[Fluentify]
public sealed partial record Actor(int Birthday, string FirstName, string Surname);

// Allows for instantiation without property initialization
var actor = new Actor();
...
```

### Class Type Usage

```csharp
[Fluentify]
public sealed class Actor
{
    public int Birthday { get; init; }
    public string FirstName { get; init; }
    public string Surname { get; init; }
}

[Fluentify]
public sealed class Movie
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

## Null Assignment Behavior

Generated extensions guard non-nullable property assignments by calling `ThrowIfNull` before creating the updated instance. This means assigning `null` to a non-nullable reference property throws an `ArgumentNullException` by default.

```csharp
[Fluentify]
public sealed class Actor
{
    public string FirstName { get; init; }
}

_ = new Actor().WithFirstName(null); // Throws ArgumentNullException
```

Nullable properties accept `null` by default.

```csharp
[Fluentify]
public sealed class Actor
{
    public string? MiddleName { get; init; }
}

_ = new Actor().WithMiddleName(null); // Allowed
```

The behavior can be controlled with attributes from `System.Diagnostics.CodeAnalysis`. Apply `AllowNull` or `MaybeNull` to a non-nullable property when `null` should be accepted, and apply `DisallowNull` to a nullable property when generated extensions should reject `null`.

Fluentify reports suggestion diagnostics when these attributes are redundant for generated null-assignment behavior: `FLTFY14` for `AllowNull`, `FLTFY15` for `MaybeNull`, and `FLTFY16` for `DisallowNull`.

```csharp
using System.Diagnostics.CodeAnalysis;

[Fluentify]
public sealed class Actor
{
    [AllowNull]
    public string FirstName { get; init; }

    [DisallowNull]
    public string? Surname { get; init; }
}
```

For record primary constructors, apply the attribute to the generated property or to the constructor parameter as needed.

```csharp
using System.Diagnostics.CodeAnalysis;

[Fluentify]
public sealed record Actor([param: AllowNull] string FirstName, [param: DisallowNull] string? Surname);
```

## Auto Initialization

The value associated with a given property can be automatically instantiated, as long as that type associated with the property adheres to the `new()` constraint. For scalar properties, a second extension method is generated, accepting a `Func<T, T>` delegate as its parameter, which allows for the existing value to be configured before being applied. A third overload is also generated when delegate support is available, accepting both an explicit instance and a `Func<T, T>` delegate so callers can chain both behaviors in a single call. If the existing value is `null` and the type is buildable, a new instance is created. If the existing value is `null` and the type cannot be automatically instantiated, a `NotSupportedException` is thrown. Delegate overloads are not generated for value types or framework types such as `string`, and collection builder overloads continue to create a new element rather than reusing the current value.

In some scenarios it may be undesirable for a given property to allow for Auto Instantiation. This feature can be disabled by applying the `[SkipAutoInitialization]` attribute to the property, the corresponding primary constructor parameter, or the type referenced by the property to suppress generation of the builder overload.

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

The name used can be customized via the `Descriptor` attribute. When a descriptor is provided that is deemed acceptable as a method name to the compiler, it is applied to the extension method. When `[Descriptor]` is applied without a name, the property name is used as the method name.

### Record Type Usage

```csharp
[Fluentify]
public sealed partial record Actor(
    [Descriptor("BornIn")] int Birthday,
    string FirstName,
    string Surname);

[Fluentify]
public sealed partial record Movie(
    Actor[] Actors,
    [Descriptor("OfGenre")] Genre Genre,
    [Descriptor] DateOnly ReleasedOn,
    string Title);
```

### Class Type Usage

```csharp
[Fluentify]
public sealed class Actor
{
    [Descriptor("BornIn")]
    public int Birthday { get; init; }
    
    public string FirstName { get; init; }

    public string Surname { get; init; }
}

[Fluentify]
public sealed class Movie
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
public sealed record Actor([Ignore] int Birthday, string FirstName, string Surname);
```

### Class Type Usage

```csharp
[Fluentify]
public sealed class Actor
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
public sealed record Actor([Hide] int Birthday, string FirstName, string Surname);
```

### Class Type Usage

```csharp
[Fluentify]
public sealed class Actor
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
[FLTFY03](docs/rules/FLTFY03.md) | Usage    | Info     | Type does not utilize Fluentify (Descriptor)
[FLTFY04](docs/rules/FLTFY04.md) | Naming   | Warning  | Descriptor must adhere to the naming conventions for Methods
[FLTFY05](docs/rules/FLTFY05.md) | Usage    | Info     | Type does not utilize Fluentify (Ignore)
[FLTFY06](docs/rules/FLTFY06.md) | Usage    | Info     | Property is already disregarded from consideration by Fluentify
[FLTFY07](docs/rules/FLTFY07.md) | Usage    | Info     | Specified descriptor is already the default used by Fluentify
[FLTFY08](docs/rules/FLTFY08.md) | Design   | Info     | Record should be partial to allow Fluentify to generate a parameterless constructor
[FLTFY09](docs/rules/FLTFY09.md) | Usage    | Warning  | Auto initiate target is invalid
[FLTFY10](docs/rules/FLTFY10.md) | Usage    | Info     | AutoInitializeWith ignored when SkipAutoInitialization is present
[FLTFY11](docs/rules/FLTFY11.md) | Usage    | Info     | Type does not utilize Fluentify (Hide)
[FLTFY12](docs/rules/FLTFY12.md) | Usage    | Info     | Hide is disregarded when Ignore is applied
[FLTFY13](docs/rules/FLTFY13.md) | Usage    | Error    | Descriptor must be unique within a type
[FLTFY14](docs/rules/FLTFY14.md) | Usage    | Info     | AllowNull is redundant for nullable properties
[FLTFY15](docs/rules/FLTFY15.md) | Usage    | Info     | MaybeNull is redundant for nullable properties
[FLTFY16](docs/rules/FLTFY16.md) | Usage    | Info     | DisallowNull is redundant for non-nullable properties

## Building a Service

Combining Fluentify with additional, custom methods, can assist with the construction of complex types. For example:

```csharp
public sealed class MyService
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
public sealed partial record MyServiceBuilder(
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