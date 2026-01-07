# FLTFY09: Auto initiate target is invalid

<table>
<tr>
  <td>Type Name</td>
  <td>FLTFY09_AutoInitiateWithAttributeAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>FLTFY09</td>
</tr>
<tr>
  <td>Category</td>
  <td>Usage</td>
</tr>
<tr>
  <td>Severity</td>
  <td>Warning</td>
</tr>
<tr>
  <td>Is Enabled By Default</td>
  <td>Yes</td>
</tr>
</table>

## Cause

The `AutoInitiateWith` attribute references a member that does not exist, is not static, requires parameters, or does not return the expected type.

## Rule Description

`AutoInitiateWith` allows a static field, property or parameterless static factory method to be used. When the attribute references a member that cannot be resolved, `Fluentify` cannot determine how to create a default value.

For example:

```csharp
[AutoInitiateWith(nameof(Create))]
public sealed class Dependent
{
    private Dependent()
    {
    }

    private static Dependent Create(int value) => new();
}
```

In this case, the `Create` method requires a parameter, so the analyzer flags the attribute usage.

## How to Fix Violations

Update the attribute to reference a static field, property or parameterless static factory method that returns the target type, or remove the attribute if no such member is available.

```csharp
[AutoInitiateWith(nameof(Default))]
public sealed class Dependent
{
    private Dependent(string name)
    {
        Name = name;
    }

    public static Dependent Default => new("Unknown");

    public string Name { get; }
}
```

## How to Suppress Violations

It is not recommended to suppress the rule. Instead, it is suggested that the `AutoInitiateWith` attribute be removed, or alternatively, the target for `AutoInitiateWith` be implemented per specification.

If suppression is desired, one of the following approaches can be used:

```csharp
#pragma warning disable FLTFY09 // Auto initiate target is invalid
[AutoInitiateWith(nameof(Default))]
public sealed class Dependent
#pragma warning restore FLTFY09 // Auto initiate target is invalid
```

or alternatively:

```csharp
using System.Diagnostics.CodeAnalysis;
using Fluentify;

[AutoInitiateWith(nameof(Default))]
[SuppressMessage("Design", "FLTFY09:Auto initiate target is invalid", Justification = "Explanation for suppression")]
public sealed class Dependent
```

## How to Disable FLTFY09

It is not recommended to disable the rule, as this may result in some confusion if expected extension methods are not present.

```ini
# Disable FLTFY09: Auto initiate target is invalid
[*.cs]
dotnet_diagnostic.FLTFY09.severity = none
```