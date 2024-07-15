# FLTFY01: Class must have an accessible parameterless constructor to use Fluentify

<table>
<tr>
  <td>Type Name</td>
  <td>FLTFY01_ClassAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>FLTFY02</td>
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

A `class` annotated with the `Fluentify` attribute does not have a `public` or `internal` parameterless constructor** (implicit or explicit).

## Rule Description

A violation of this rule occurs when a `class` defines one or more constructors, none of which is parameterless, with a `public` or `internal` scope. When applied to a `class`, `Fluentify` will utilize the default constructor to project state to a new instance of the type, preserving immutability of the original instance.

For example:

```csharp
[Fluentify]
public class ExampleWithParameterizedConstructor
{
    public ExampleWithParameterizedConstructor(int value)
    {
        Value = value;
    }

    public int Value { get; }
}

[Fluentify]
public class ExampleWithInaccessibleConstructor
{
    private ExampleWithInaccessibleConstructor()
    {
    }

    public int Value { get; init; }
}
```

## How to Fix Violations

To fix a violation of this rule, ensure that the `class` has a `public` or `internal` parameterless constructor.

For example:

```csharp
[Fluentify]
public class ExampleWithImplicitConstructor
{
    public int Value { get; init; }
}
```

or alternatively:

```csharp
[Fluentify]
public class ExampleWithInternalConstructor
{
    public ExampleWithInternalConstructor(int value)
    {
        Value = value;
    }

    internal ExampleWithInternalConstructor()
    {
    }

    public int Value { get; internal init; }
}
```

## How to Suppress Violations

It is not recommended to suppress the rule. Instead, it is suggested that the `Fluentify` attribute be removed.

If suppression is desired, one of the following approaches can be used:

```csharp
#pragma warning disable FLTFY01 // Class must have an accessible parameterless constructor to use Fluentify
[Fluentify]
public class ExampleWithParameterizedConstructor
{
    public ExampleWithParameterizedConstructor(int value)
    {
        Value = value;
    }

    public int Value { get; }
}
#pragma warning restore FLTFY01 // Class must have an accessible parameterless constructor to use Fluentify
```

or alternatively:

```
[Fluentify]
[SuppressMessage("Design", "FLTFY01:Class must have an accessible parameterless constructor to use Fluentify", Justification = "Explanation for suppression")]
public class ExampleWithParameterizedConstructor
{
    public ExampleWithParameterizedConstructor(int value)
    {
        Value = value;
    }

    public int Value { get; }
}
```

## How to Disable FLTFY01

It is not recommended to disable the rule, as this may result in some confusion if expected extension methods are not present.

```ini
# Disable FLTFY01: Class must have an accessible parameterless constructor to use Fluentify
[*.cs]
dotnet_diagnostic.FLTYF01.severity = none
```