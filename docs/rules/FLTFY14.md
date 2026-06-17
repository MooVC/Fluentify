# FLTFY14: AllowNull is redundant for nullable properties

<table>
  <tr>
    <td>Type Name</td>
    <td>FLTFY14_NullabilityAttributeAnalyzer</td>
  </tr>
  <tr>
    <td>CheckId</td>
    <td>FLTFY14</td>
  </tr>
  <tr>
    <td>Category</td>
    <td>Usage</td>
  </tr>
</table>

## Cause

A nullable property on a type annotated with `Fluentify` is also annotated with `AllowNull`.

## Rule Description

Generated Fluentify extensions already allow `null` to be assigned to nullable properties. Applying `AllowNull` to a nullable property is redundant for Fluentify null-assignment behavior.

## How to Fix Violations

Remove the redundant `AllowNull` attribute.

## Example

```csharp
using System.Diagnostics.CodeAnalysis;
using Fluentify;

[Fluentify]
public sealed class Actor
{
    [AllowNull]
    public string? Name { get; init; }
}
```

Use the nullable annotation without `AllowNull`.

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
#pragma warning disable FLTFY14 // AllowNull is redundant for nullable properties
```

## How to Disable FLTFY14

```ini
dotnet_diagnostic.FLTFY14.severity = none
```