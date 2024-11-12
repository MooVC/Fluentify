# FLTFY04: Descriptor must adhere to the naming conventions for Methods

<table>
<tr>
  <td>Type Name</td>
  <td>FLTFY04_DescriptorAttributeAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>FLTFY04</td>
</tr>
<tr>
  <td>Category</td>
  <td>Naming</td>
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

The value provided for the `Descriptor` will be used as the name of the extension method and must adhere to the standard C# .NET naming conventions for methods.

## Rule Description

A violation of this rule occurs when the value provided for the `Descriptor` attribute does not adhere to the naming conventions for methods in C# .NET, evaluated based on the following expression: `^[A-Z][a-zA-Z0-9]*$`

For example:

```csharp
[Descriptor("Assign Value")]
public string Property { get; set; }
```

In this example, the value `Assign Value` is not suitable for use as an extension method name as it does not adhere to the C# .NET naming conventions for methods.

## How to Fix Violations

Provide a value for the `Descriptor` attribute that adheres to the standard C# .NET naming conventions for methods.

For example:

```csharp
[Descriptor("AssignValue")]
public string Property { get; set; }
```

## When to Suppress Warnings

Warnings from this rule should be suppressed only if there is a strong justification for using a non-standard name as the `Descriptor`. Even when suppressed, Fluentify will not apply the non-standard name.

If suppression is desired, one of the following approaches can be used:

```csharp
#pragma warning disable FLTFY04 // Descriptor must adhere to the naming conventions for Methods

[Descriptor("Assign Value")]
public string Property { get; set; }

#pragma warning restore FLTFY04 // Descriptor must adhere to the naming conventions for Methods
```

or alternatively:

```csharp
[Descriptor("Assign Value")]
[SuppressMessage("Naming", "FLTFY04:Descriptor must adhere to the naming conventions for Methods", Justification = "Explanation for suppression")]
public string Property { get; set; }
```

## How to Disable FLTFY04

It is not recommended to disable the rule, as this may result in some confusion if expected extension methods are not present.

```ini
# Disable FLTFY04: Descriptor must adhere to the naming conventions for Methods
[*.cs]
dotnet_diagnostic.FLTFY04.severity = none
```