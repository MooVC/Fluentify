# FLTFY08: Record should be partial to allow Fluentify to generate a parameterless constructor

<table>
<tr>
  <td>Type Name</td>
  <td>FLTFY08_RecordAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>FLTFY08</td>
</tr>
<tr>
  <td>Category</td>
  <td>Design</td>
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

A `record` annotated with the `Fluentify` attribute does not declare the `partial` keyword and does not provide a `public` or `internal` parameterless constructor.

## Rule Description

A violation of this rule occurs when a `record` relies on Fluentify to generate a parameterless constructor but does not allow that generation by omitting the `partial` keyword. Fluentify emits a default constructor for partial records without an accessible parameterless constructor so that fluent builders can create new instances without reusing projection constructors.

For example:

```csharp
using Fluentify;

[Fluentify]
public record ExampleWithoutPartial(string Value);
```

## How to Fix Violations

To fix a violation of this rule, add the `partial` keyword or explicitly provide an accessible parameterless constructor. Adding `partial` allows Fluentify to generate the constructor automatically.

For example:

```csharp
using Fluentify;

[Fluentify]
public partial record ExampleWithPartial(string Value);
```

or alternatively:

```csharp
using Fluentify;

[Fluentify]
public record ExampleWithDefault
{
    public ExampleWithDefault()
    {
    }

    public string Value { get; init; } = string.Empty;
}
```

## How to Suppress Violations

It is not recommended to suppress the rule. Instead, it is suggested that the `Fluentify` attribute be removed.

If suppression is desired, one of the following approaches can be used:

```csharp
#pragma warning disable FLTFY08 // Record should be partial to allow Fluentify to generate a parameterless constructor
[Fluentify]
public record ExampleWithoutPartial(string Value);
#pragma warning restore FLTFY08 // Record should be partial to allow Fluentify to generate a parameterless constructor
```

or alternatively:

```csharp
using System.Diagnostics.CodeAnalysis;
using Fluentify;

[Fluentify]
[SuppressMessage("Design", "FLTFY08:Record should be partial to allow Fluentify to generate a parameterless constructor", Justification = "Explanation for suppression")]
public record ExampleWithoutPartial(string Value);
```

## How to Disable FLTFY08

It is not recommended to disable the rule, as this may result in some confusion if expected extension methods are not present.

```ini
# Disable FLTFY08: Record should be partial to allow Fluentify to generate a parameterless constructor
[*.cs]
dotnet_diagnostic.FLTFY08.severity = none
```
