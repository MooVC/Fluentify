# FLTFY02: Descriptor is disregarded from consideration by Fluentify

<table>
<tr>
  <td>Type Name</td>
  <td>FLTFY02_DescriptorAttributeAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>FLTFY02</td>
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

A property marked with the `Descriptor` attribute is also marked with the `Ignore` attribute, making the use of the `Descriptor` attribute redundant.

## Rule Description

A violation of this rule occurs when a property is marked with the `Ignore` attribute,  excluding it from consideration by Fluentify. Therefore, using the `Descriptor` attribute on such properties is redundant and should be avoided.

For example:

```csharp
[Descriptor("Assign"), Ignore]
public string Property { get; set; }
```

In this example, `Property` is excluded from Fluentify's consideration due to the presence of the `Ignore` attribute, making use of the `Descriptor` attribute redundant, suggesting a misunderstanding by the engineer as to its intended usage.

## How to Fix Violations

Reevaluate the decision to apply the `Ignore` attribute. If the `Ignore` attribute usage is deemed correct, remove the `Descriptor` attribute, otherwise remove the `Ignore` attribute.

For example:

```csharp
[Descriptor("Assign")]
public string Property { get; set; }
```
or alternatively:

```csharp
[Ignore]
public string Property { get; set; }
```

## When to Suppress Warnings

Warnings from this rule should be suppressed only if there is a strong justification for the redundant use of the `Descriptor` attribute.

If suppression is desired, one of the following approaches can be used:

```csharp
[Fluentify]
public class Example
{
    #pragma warning disable FLTFY02 // Descriptor is disregarded from consideration by Fluentify
    
    [Descriptor("Assign"), Ignore]
    public string Property { get; set; }
    
    #pragma warning restore FLTFY02 // Descriptor is disregarded from consideration by Fluentify
}
```

or alternatively:

```csharp
[Fluentify]
public class Example
{
    [Descriptor("Assign"), Ignore]
    [SuppressMessage("Design", "FLTFY02:Descriptor is disregarded from consideration by Fluentify", Justification = "Explanation for suppression")]
    public string Property { get; set; }
}
```

## How to Disable FLTFY02

It is not recommended to disable the rule, as this may result in some confusion if expected extension methods are not present.

```ini
# Disable FLTFY02: Descriptor is disregarded from consideration by Fluentify
[*.cs]
dotnet_diagnostic.FLTFY02.severity = none
```