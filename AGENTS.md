# Root Instructions for Codex

This repository hosts **Fluentify**, a .NET source generator and analyzers solution.

> **Note**
> This file provides quick instructions for Codex. Human contributors should refer to the
> [README](README.md) and [CONTRIBUTING guide](.github/CONTRIBUTING.md) for a fuller picture.

## Build and Test

- Use the .NET SDK **10.0**. The latest SDK can be found at [dotnet.microsoft.com](https://dotnet.microsoft.com/).
- Restore packages with `dotnet restore`.
- Run `dotnet test` to execute the test suite. This is the primary check before committing.
- Tests are configured via `.runsettings` and use xUnit.

## Coding Style

The project enforces strong C# coding conventions through `.editorconfig`, [StyleCop Analyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers), and [SonarAnalyzer for C#](https://github.com/SonarSource/sonar-dotnet). Key points from the [Contributing guide](.github/CONTRIBUTING.md):

- Prefer **file-scoped namespaces**.
- Follow Microsoft naming guidelines (PascalCase for types and members, camelCase for locals/parameters, prefix interfaces with `I`).
- Seal classes when extension is not intended.
- Use discards (`_`) for unused values.
- Organize extension methods so each file is named `{TypeName}Extensions.{MethodName}.cs`.
- Use resource files for all user-facing strings with names `{TypeName}.Resources.{locale}.resx` and keys formatted as `{Context}{Subject}{Purpose}`.
- Avoid `#region` pragmas.
- Avoid placing a newline character at the end of files.
- Avoid abrieviations in names, except for well-known acronyms (e.g., `Http`, `Xml`).
- Avoid single-letter names, even for loop variables.
- Avoid qualified types, use `using` directives instead or an alias if necessary.
- Avoid blank lines at the end of a file.
- Use `var` for local variables when the type is clear from the right-hand side.
- Use `nameof` for member names in exceptions and logging.
- Use `string.Empty` instead of `""` for empty strings.
- When possible, place declarations in alphabetical order (e.g. fields, parameters, properties, methods).

### Unit Testing Conventions

- Follow **Arrange-Act-Assert** structure.
- Place tests for a class under a matching namespace `{Class Namespace}.{Class Name}Tests`.
- Name test classes `When{MethodName}IsCalled` and test methods `Given{Condition}When{State}Then{Expectation}`. If no {State} is needed, use `Given{Condition}Then{Expectation}`. Do not use the MethodName in the test name, as all tests in the class relate to the same method and the class name identified the method name.
- Use [`Shouldy`](https://docs.shouldly.org/) and `NSubstitute` for assertions and mocks.
- Use constants for test data, especially for strings and numbers, to avoid magic values.

## Project Structure

- Production code resides in `src/Fluentify`.
- Tests live in `src/Fluentify.Tests` and mirror the main project layout.
- Documentation for analyzers is under `docs/rules`.

## Pull Requests

The [PR template](.github/pull_request_template.md) requires that you:

- Follow the coding style.
- Add or update tests when necessary.
- Ensure tests pass locally.
- Update `CHANGELOG.md` when consumer-facing changes occur.