# FLTFY13: Descriptor must be unique within a type

<table>
  <tr>
    <td>TypeName</td>
    <td>FLTFY13_DescriptorAttributeAnalyzer</td>
  </tr>
  <tr>
    <td>CheckId</td>
    <td>FLTFY13</td>
  </tr>
  <tr>
    <td>Category</td>
    <td>Usage</td>
  </tr>
  <tr>
    <td>Severity</td>
    <td>Error</td>
  </tr>
  <tr>
    <td>IsEnabled</td>
    <td>True</td>
  </tr>
</table>

## Cause

This diagnostic is raised when two properties on the same type are annotated with `Descriptor` and resolve to the same descriptor value.

## Rule Description

Descriptor values map directly to generated extension method names. Duplicate descriptor values on a single type would produce method name collisions and ambiguous intent, so each descriptor must be unique.

## Noncompliant Code Example

```csharp
using Fluentify;

[Fluentify]
public class Person
{
    [Descriptor("WithValue")]
    public string FirstName { get; set; }

    [Descriptor("WithValue")]
    public string LastName { get; set; }
}
```

## Compliant Code Example

```csharp
using Fluentify;

[Fluentify]
public class Person
{
    [Descriptor("WithFirstName")]
    public string FirstName { get; set; }

    [Descriptor("WithLastName")]
    public string LastName { get; set; }
}
```

## Suppressing Warnings

You can suppress this diagnostic using standard .NET suppression mechanisms:

```csharp
#pragma warning disable FLTFY13 // Descriptor must be unique within a type
// ... code that intentionally duplicates descriptors ...
#pragma warning restore FLTFY13 // Descriptor must be unique within a type
```

or:

```csharp
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Usage", "FLTFY13:Descriptor must be unique within a type", Justification = "Explanation for suppression")]
```

## How to Disable FLTFY13

In your `.editorconfig`:

```ini
# Disable FLTFY13: Descriptor must be unique within a type
dotnet_diagnostic.FLTFY13.severity = none
```