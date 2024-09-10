# FLTFY07: Specified descriptor is already the default used by Fluentify

<table>
<tr>
  <td>Type Name</td>
  <td>FLTFY07_DescriptorAttributeAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>FLTFY07</td>
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

The specified descriptor is already the default used by Fluentify, so its usage is redundant.

## Rule Description

A violation of this rule occurs when the descriptor specified for a given property results in the same value that Fluentify will select by default if no `Descriptor` attribute is specified, making the usage of the `Descriptor` attribute redundant.

For example:

```csharp
[Fluentify]
public class Example
{
    [Descriptor]
    public bool IsActive { get; init; }

    [Descriptor("WithName")]
    public string Name { get; init; }
}
```

In this example, `IsActive` is a boolean, and by default, Fluentify will select its declared name as the descriptor, so the usage of the `Descriptor` attribute is redundant. Likewise, Fluentify will use the `With{PropertyName}` pattern for non-boolean properties, so the default descriptor for the `Name` property would be `WithName`. 

## How to Fix Violations

Remove the redundant `Descriptor` attribute from properties that are already given the matching descriptor by default.

For example:

```csharp
[Fluentify]
public class Example
{
    public bool IsActive { get; init; }
    public string Name { get; init; }
}
```

## When to Suppress Warnings

Warnings from this rule should be suppressed only if there is a strong justification for the redundant use of the `Descriptor` attribute.

If suppression is desired, one of the following approaches can be used:

```csharp
[Fluentify]
public class Example
{
    #pragma warning disable FLTFY07 // Specified descriptor is already the default used by Fluentify

    [Descriptor]
    public bool IsActive { get; init; }

    [Descriptor("WithName")]
    public string Name { get; init; }

    #pragma warning restore FLTFY07 // Specified descriptor is already the default used by Fluentify
}
```

or alternatively:

```csharp
[Fluentify]
public class Example
{
    [Descriptor]
    [SuppressMessage("Usage", "FLTFY07:Specified descriptor is already the default used by Fluentify", Justification = "Explanation for suppression")]
    public bool IsActive { get; init; }

    [Descriptor("WithName")]
    [SuppressMessage("Usage", "FLTFY07:Specified descriptor is already the default used by Fluentify", Justification = "Explanation for suppression")]
    public string Name { get; init; }
}
```

## How to Disable FLTFY06

It is not recommended to disable the rule, as its presence suggests a misunderstanding by the engineer as to its intended usage.

```ini
# Disable FLTFY07: Specified descriptor is already the default used by Fluentify
[*.cs]
dotnet_diagnostic.FLTYF07.severity = none
```