# FLTFY10: AutoInitializeWith ignored when SkipAutoInitialization is present

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

Both `SkipAutoInitialization` and `AutoInitializeWith` are applied to the same type.

## Rule Description

`SkipAutoInitialization` indicates that a type cannot be automatically instantiated. When `AutoInitializeWith` is also applied to the same type, the skip attribute takes precedence and the auto initiation path is ignored. The analyzer highlights the conflict to avoid confusion.

For example:

```csharp
[AutoInitializeWith(nameof(Default))]
[SkipAutoInitialization]
public sealed class Dependent
{
    public static Dependent Default => new();
}
```

In this scenario, the `Default` property will not be used because the type explicitly opts out of auto initialization.

## How to Fix Violations

Remove the `AutoInitializeWith` attribute when `SkipAutoInitialization` is present, or remove `SkipAutoInitialization` if the type should be eligible for auto initiation.

```csharp
[AutoInitializeWith(nameof(Default))]
public sealed class Dependent
{
    public static Dependent Default => new();
}
```

## How to Suppress Violations

It is not recommended to suppress the rule. Instead, decide whether or not the type should use `AutoInitializeWith` or `SkipAutoInitialization`. Once the decision is made, remove the other attribute.

If suppression is desired, one of the following approaches can be used:

```csharp
#pragma warning disable FLTFY10 // AutoInitializeWith ignored when SkipAutoInitialization is present
[AutoInitializeWith(nameof(Default))]
[SkipAutoInitialization]
public sealed class Dependent
#pragma warning restore FLTFY10 // AutoInitializeWith ignored when SkipAutoInitialization is present
```

or alternatively:

```csharp
using System.Diagnostics.CodeAnalysis;
using Fluentify;

[AutoInitializeWith(nameof(Default))]
[SkipAutoInitialization]
[SuppressMessage("Design", "FLTFY10:AutoInitializeWith ignored when SkipAutoInitialization is present", Justification = "Explanation for suppression")]
public sealed class Dependent
```

## How to Disable FLTFY09

It is not recommended to disable the rule, as this may result in some confusion if expected extension methods are not present.

```ini
# Disable FLTFY10: AutoInitializeWith ignored when SkipAutoInitialization is present
[*.cs]
dotnet_diagnostic.FLTFY10.severity = none
```