# FLTFY06: Property is already disregarded from consideration by Fluentify

<table>
<tr>
  <td>Type Name</td>
  <td>FLTFY06_IgnoreAttributeAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>FLTFY06</td>
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

This property is not considered by Fluentify, so the usage of the Ignore attribute is redundant.

## Rule Description

A violation of this rule occurs when a property that is already disregarded by Fluentify is marked with the `Ignore` attribute, making the usage of the `Ignore` attribute redundant.

For example:

```csharp
[Fluentify]
public class Example
{
    [Ignore]
    public string Property { get; } = string.Empty;
}
```

In this example, `Property` is already disregarded by Fluentify as it is deemed to be immutable, so the usage of the `Ignore` attribute is redundant.

## How to Fix Violations

Remove the redundant `Ignore` attribute from properties that are already disregarded by Fluentify.

For example:

```csharp
[Fluentify]
public class Example
{
    public string Property { get; } = string.Empty;
}
```

## When to Suppress Warnings

Warnings from this rule should be suppressed only if there is a strong justification for the redundant use of the `Ignore` attribute.

If suppression is desired, one of the following approaches can be used:

```csharp
[Fluentify]
public class Example
{
    #pragma warning disable FLTFY06 // Property is already disregarded from consideration by Fluentify
    
    [Ignore]
    public string Property { get; } = string.Empty;
    
    #pragma warning restore FLTFY06 // Property is already disregarded from consideration by Fluentify
}
```

or alternatively:

```csharp
[Fluentify]
public class Example
{
    [Ignore]
    [SuppressMessage("Usage", "FLTFY06:Property is already disregarded from consideration by Fluentify", Justification = "Explanation for suppression")]
    public string Property { get; } = string.Empty;
}
```

## How to Disable FLTFY06

It is not recommended to disable the rule, as its presence suggests a misunderstanding by the engineer as to its intended usage.

```ini
# Disable FLTFY06: Property is already disregarded from consideration by Fluentify
[*.cs]
dotnet_diagnostic.FLTYF06.severity = none
```