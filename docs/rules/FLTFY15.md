# FLTFY15: MaybeNull is redundant for nullable properties

<table>
  <tr>
    <td>Type Name</td>
    <td>FLTFY15_NullabilityAttributeAnalyzer</td>
  </tr>
  <tr>
    <td>CheckId</td>
    <td>FLTFY15</td>
  </tr>
  <tr>
    <td>Category</td>
    <td>Usage</td>
  </tr>
</table>

## Cause

A nullable property on a type annotated with `Fluentify` is also annotated with `MaybeNull`.

## Rule Description

Generated Fluentify extensions already allow `null` to be assigned to nullable properties. Applying `MaybeNull` to a nullable property is redundant for Fluentify null-assignment behavior.

## How to Fix Violations

Remove the redundant `MaybeNull` attribute.

## Example

```csharp
using System.Diagnostics.CodeAnalysis;
using Fluentify;

[Fluentify]
public sealed class Actor
{
    [MaybeNull]
    public string? Name { get; init; }
}
```

Use the nullable annotation without `MaybeNull`.

```csharp
using Fluentify;

[Fluentify]
public sealed class Actor
{
    public string? Name { get; init; }
}
```

## How to Suppress Violations

```csharp
#pragma warning disable FLTFY15 // MaybeNull is redundant for nullable properties
```

## How to Disable FLTFY15

```ini
dotnet_diagnostic.FLTFY15.severity = none
```