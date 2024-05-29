# Fluentify [![NuGet](https://img.shields.io/nuget/v/Fluentify?logo=nuget)](https://www.nuget.org/packages/Fluentify/) [![GitHub](https://img.shields.io/github/license/MooVC/Fluentify)](LICENSE.md)

Fluentify is a .NET Roslyn Source Generator designed to automate the creation of Fluent APIs. This tool enables engineers to rapidly develop rich, expressive, and maintainable APIs with ease.

## Getting Started

### Installation

To install Fluentify, use the following command in your package manager console:

```shell
install-package Fluentify
```

### Usage

Fluentify automatically creates extension methods for each property on a **record type** that has the `Fluentify` attribute. These extension methods facilitate the development of Fluent APIs by following the immutability patterns enabled by record types in C#.

```csharp
[Fluentify]
public record Person(ushort Age, DateOnly DateOfBirth, Name Name);
```

The generated Fluentify extension methods allow for a specific instance to be applied to a given property. Alternatively, by using the `Builder<T>` delegate, Fluentify will create the instance on your behalf and allow you to configure it. Each extension method returns a new instance of the annotated type, using the **with-expression** feature of records. This approach preserves the immutability of the original instance.

```csharp
var person = new Person(...);

_ = person
    .WithAge(41)
    .WithName(name => name
        .WithGiven("Paul")
        .WithFamily("Martins"))
    .WithDateOfBirth(new DateOnly(1983, 7, 24));
```

When the annotated record is marked as partial, an internal, parameterless constructor is created, avoiding the need to specify values upon construction when using the `Builder<T>` delegate.

```csharp
[Fluentify]
public partial record Name(string Family, string Given);
```

The name used for the generated Fluentify extension method can also be customized using the `Descriptor` attribute:

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

Specific properties can be excluded from generating Fluentify extension methods using the `Ignore` attribute:

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

### Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues to suggest improvements or add new features.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.