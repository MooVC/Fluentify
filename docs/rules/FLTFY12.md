# FLTFY12: Hide is disregarded when Ignore is applied

<table>
<tr>
  <td>Type Name</td>
  <td>FLTFY12_HideAttributeAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>FLTFY12</td>
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

The property is annotated with both `Hide` and `Ignore`.

## Rule Description

A violation of this rule occurs when a property is marked with both the `Hide` and `Ignore` attributes. The `Ignore` attribute removes the property from Fluentify consideration entirely, so the `Hide` attribute is redundant and ignored.

For example:

```csharp
[Fluentify]
public class Example
{
    [Hide, Ignore]
    public string Property { get; set; }
}
```

In this example, Fluentify will not generate extension methods for `Property` because `Ignore` takes precedence. The `Hide` attribute has no effect.

## How to Fix Violations

Reevaluate whether the property should be excluded or merely hidden.

- If the property should be excluded from Fluentify, remove the `Hide` attribute.
- If the property should remain in Fluentify but be internal, remove the `Ignore` attribute.

For example:

```csharp
[Fluentify]
public class Example
{
    [Ignore]
    public string Property { get; set; }
}
```
or alternatively:

```csharp
[Fluentify]
public class Example
{
    [Hide]
    public string Property { get; set; }
}
```

## When to Suppress Warnings

Warnings from this rule should be suppressed only if there is a strong justification for applying both `Hide` and `Ignore`.

If suppression is desired, one of the following approaches can be used:

```csharp
[Fluentify]
public class Example
{
    #pragma warning disable FLTFY12 // Hide is disregarded when Ignore is applied
    
    [Hide, Ignore]
    public string Property { get; set; }
    
    #pragma warning restore FLTFY12 // Hide is disregarded when Ignore is applied
}
```

or alternatively:

```csharp
[Fluentify]
public class Example
{
    [Hide, Ignore]
    [SuppressMessage("Usage", "FLTFY12:Hide is disregarded when Ignore is applied", Justification = "Explanation for suppression")]
    public string Property { get; set; }
}
```

## How to Disable FLTFY12

It is not recommended to disable the rule, as this may result in confusion about which attribute is expected to take effect.

```ini
# Disable FLTFY12: Hide is disregarded when Ignore is applied
[*.cs]
dotnet_diagnostic.FLTFY12.severity = none
```