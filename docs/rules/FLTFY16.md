# FLTFY16: DisallowNull is redundant for non-nullable properties

<table>
  <tr>
    <td>Type Name</td>
    <td>FLTFY16_NullabilityAttributeAnalyzer</td>
  </tr>
  <tr>
    <td>CheckId</td>
    <td>FLTFY16</td>
  </tr>
  <tr>
    <td>Category</td>
    <td>Usage</td>
  </tr>
</table>

## Cause

A non-nullable property on a type annotated with `Fluentify` is also annotated with `DisallowNull`.

## Rule Description

Generated Fluentify extensions already reject `null` assignments for non-nullable properties. Applying `DisallowNull` to a non-nullable property is redundant for Fluentify null-assignment behavior.

## How to Fix Violations

Remove the redundant `DisallowNull` attribute.

## Example

```csharp
using System.Diagnostics.CodeAnalysis;
using Fluentify;

[Fluentify]
public sealed class Actor
{
    [DisallowNull]
    public string Name { get; init; }
}
```

Use the non-nullable annotation without `DisallowNull`.

```csharp
using Fluentify;

[Fluentify]
public sealed class Actor
{
    public string Name { get; init; }
}
```

## How to Suppress Violations

```csharp
#pragma warning disable FLTFY16 // DisallowNull is redundant for non-nullable properties
```

## How to Disable FLTFY16

```ini
dotnet_diagnostic.FLTFY16.severity = none
```