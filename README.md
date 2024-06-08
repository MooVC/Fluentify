
# Fluentify [![NuGet](https://img.shields.io/nuget/v/Fluentify?logo=nuget)](https://www.nuget.org/packages/Fluentify/) [![GitHub](https://img.shields.io/github/license/MooVC/Fluentify)](LICENSE.md)

Fluentify is a .NET Roslyn Source Generator designed to automate the creation of Fluent APIs. This tool enables engineers to rapidly develop rich, expressive, and maintainable APIs with ease. Utilizing Fluentify allows for cleaner code, easier maintenance, and more expressive interactions within your C# .NET applications.

## Installation

To install Fluentify, use the following command in your package manager console:

```shell
install-package Fluentify
```

## Usage

Fluentify automatically creates extension methods for each property on types that have the `Fluentify` attribute, supporting both class and record types.

### Class Type Usage

```csharp
[Fluentify]
public class Person
{
    public ushort Age { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public Name Name { get; init; }
}
```

### Record Type Usage

```csharp
[Fluentify]
public record Person(ushort Age, DateOnly DateOfBirth, Name Name);
```

The generated extension methods preserve immutability, providing a new instance with the specified value applied to the associated property. Using the `Builder<T>` delegate, Fluentify can also instantiate the value for the associated property, allowing for it to be configured.

```csharp
var person = new Person(...);

_ = person
    .WithAge(41)               // Apply 41 to the Age property
    .WithName(name => name     // A Builder<T> delegate, creating a new instance of the Name type and allowing for it's configuration
        .WithGiven("Paul")
        .WithFamily("Martins"))
    .WithDateOfBirth(new DateOnly(1983, 7, 24));
```

## Custom Descriptors

Extension method names can be customized via the `Descriptor` attribute.

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
public sealed record Person(
  [Descriptor("Aged")] ushort Age,
  [Descriptor("BornOn")] DateOnly DateOfBirth,
  [Descriptor("Named")] Name Name);
```

This allows for greater alignment with domain semantics:

```csharp
_ = person
    .Aged(41)
    .Named(name => name
        .WithGiven("Paul")
        .WithFamily("Martins"))
    .BornOn(new DateOnly(1983, 7, 24));
```

## Property Exclusion

Specific properties can be excluded from generating Fluentify extension methods using the `Ignore` attribute:

### Class Type Usage

```csharp
[Fluentify]
public class Person
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
public sealed record Person([Ignore] ushort Age, DateOnly DateOfBirth, Name Name);
```

This will result in an error if you try to use the ignored property in the fluent chain:

```csharp
_ = person
    .WithAge(41) // IntelliSense Error: 'Person' does not contain a definition for 'WithAge'
    .WithName(name => name
        .WithGiven("Paul")
        .WithFamily("Martins"))
    .WithDateOfBirth(new DateOnly(1983, 7, 24));
```

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues to suggest improvements or add new features.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.