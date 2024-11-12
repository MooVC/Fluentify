# FLTFY03: Type does not utilize Fluentify

<table>
<tr>
  <td>Type Name</td>
  <td>FLTFY03_DescriptorAttributeAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>FLTFY03</td>
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

The property and its `Descriptor` are not considered by Fluentify because the type has not been annotated with the `Fluentify` attribute.

## Rule Description

A violation of this rule occurs when a property is marked with the `Descriptor` attribute, but the containing type, be it a `class` or `record`, is not annotated with the `Fluentify` attribute. Therefore, no extension methods will be generated, making use of the `Descriptor` attribute redundant.

For example:

```csharp
public class Example
{
    [Descriptor("Assign")]
    public string Property { get; set; }
}
```

In this example, the `Descriptor` attribute on `Property`, and the `class` itself, will be ignored by `Fluentify`, suggesting a misunderstanding by the engineer as to its intended usage.

## How to Fix Violations

Reevaluate the decision to apply the `Discriptor` attribute. If the `Discriptor` attribute usage is deemed correct, annotate the type with the `Fluentify` attribute, otherwise remove the `Descriptor` attribute.

For example:

```csharp
[Fluentify]
public class Example
{
    [Descriptor("Assign")]
    public string Property { get; set; }
}
```
or alternatively:

```csharp
public class Example
{
    public string Property { get; set; }
}
```

## When to Suppress Warnings

Warnings from this rule should be suppressed only if there is a strong justification for not using the `Fluentify` attribute on the containing type when the `Discriptor` attribute is applied.

If suppression is desired, one of the following approaches can be used:

```csharp
[Fluentify]
public class Example
{
    #pragma warning disable FLTFY03 // Type does not utilize Fluentify
    
    [Descriptor("Assign")]
    public string Property { get; set; }
    
    #pragma warning restore FLTFY03 // Type does not utilize Fluentify
}
```

or alternatively:

```csharp
public class Example
{
    [Descriptor("Assign")]
    [SuppressMessage("Usage", "FLTFY03:Type does not utilize Fluentify", Justification = "Explanation for suppression")]
    public string Property { get; set; }
}
```

## How to Disable FLTFY03

It is not recommended to disable the rule, as this may result in some confusion if expected extension methods are not present.

```ini
# Disable FLTFY03: Type does not utilize Fluentify
[*.cs]
dotnet_diagnostic.FLTFY03.severity = none
```