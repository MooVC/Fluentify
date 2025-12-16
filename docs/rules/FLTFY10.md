# FLTFY10: AutoInitiateWith ignored when SkipAutoInitialization is present

<table>
<tr>
  <td>Type Name</td>
  <td>FLTFY10_SkipAutoInitializationAttributeAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>FLTFY10</td>
</tr>
<tr>
  <td>Category</td>
  <td>Usage</td>
</tr>
<tr>
  <td>Severity</td>
  <td>Info</td>
</tr>
<tr>
  <td>Is Enabled By Default</td>
  <td>Yes</td>
</tr>
</table>

## Cause

Both `SkipAutoInitialization` and `AutoInitiateWith` are applied to the same type.

## Rule Description

`SkipAutoInitialization` indicates that a type cannot be automatically instantiated. When `AutoInitiateWith` is also applied to the same type, the skip attribute takes precedence and the auto initiation path is ignored. The analyzer highlights the conflict to avoid confusion.

For example:

```csharp
[SkipAutoInitialization]
[AutoInitiateWith(nameof(Default))]
public sealed class Dependent
{
    public static Dependent Default => new();
}
```

In this scenario, the `Default` property will not be used because the type explicitly opts out of auto initialization.

## How to Fix Violations

Remove the `AutoInitiateWith` attribute when `SkipAutoInitialization` is present, or remove `SkipAutoInitialization` if the type should be eligible for auto initiation.

```csharp
[AutoInitiateWith(nameof(Default))]
public sealed class Dependent
{
    public static Dependent Default => new();
}
```

## When to Suppress Warnings

Suppress the warning only when both attributes are intentionally combined and the loss of auto initiation is acceptable. In most cases, removing the redundant `AutoInitiateWith` attribute is clearer.
