# FLTFY11: Type does not utilize Fluentify

<table>
<tr>
  <td>Type Name</td>
  <td>FLTFY11_HideAttributeAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>FLTFY11</td>
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

The property is not considered by Fluentify because the type has not been annotated with the `Fluentify` attribute.

## Rule Description

A violation of this rule occurs when a property is marked with the `Hide` attribute, but the containing type, be it a `class` or `record`, is not annotated with the `Fluentify` attribute. Therefore, no extension methods will be generated, making use of the `Hide` attribute redundant.

For example:

```csharp
public class Example
{
    [Hide]
    public string Property { get; set; }
}
```

In this example, the `Hide` attribute on `Property`, and the `class` itself, will be ignored by Fluentify, suggesting a misunderstanding by the engineer as to its intended usage.

## How to Fix Violations

Reevaluate the decision to apply the `Hide` attribute. If the `Hide` attribute usage is deemed correct, annotate the type with the `Fluentify` attribute, otherwise remove the `Hide` attribute.

For example:

```csharp
[Fluentify]
public class Example
{
    [Hide]
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

Warnings from this rule should be suppressed only if there is a strong justification for not using the `Fluentify` attribute on the containing type when the `Hide` attribute is applied.

If suppression is desired, one of the following approaches can be used:

```csharp
[Fluentify]
public class Example
{
    #pragma warning disable FLTFY11 // Type does not utilize Fluentify
    
    [Hide]
    public string Property { get; set; }
    
    #pragma warning restore FLTFY11 // Type does not utilize Fluentify
}
```

or alternatively:

```csharp
public class Example
{
    [Hide]
    [SuppressMessage("Usage", "FLTFY11:Type does not utilize Fluentify", Justification = "Explanation for suppression")]
    public string Property { get; set; }
}
```

## How to Disable FLTFY11

It is not recommended to disable the rule, as this may result in some confusion if expected extension methods are not present.

```ini
# Disable FLTFY11: Type does not utilize Fluentify
[*.cs]
dotnet_diagnostic.FLTFY11.severity = none
```