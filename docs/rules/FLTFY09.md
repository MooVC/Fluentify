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

`AutoInitiateWith` allows a static property or parameterless static method to be used when a type does not expose a default constructor. When the attribute references a member that cannot be resolved, Fluentify cannot determine how to create a default value.

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

Update the attribute to reference a static property or parameterless static method that returns the target type, or remove the attribute if no such member is available.

```csharp
[AutoInitiateWith(nameof(Default))]
public sealed class Dependent
{
    private Dependent()
    {
    }

    public static Dependent Default => new();
}
```

## When to Suppress Warnings

Warnings should only be suppressed when the attribute is intentionally pointing to a member that will be added later or when the initialization path is handled externally. Generally, providing a valid static property or method is preferred.
