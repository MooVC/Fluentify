<!-- omit in toc -->
# Contributing to Fluentify

First off, thanks for taking the time to contribute! ❤️

All types of contributions are encouraged and valued. See the [Table of Contents](#table-of-contents) for different ways to help and details about how this project handles them. Please make sure to read the relevant section before making your contribution. It will make it a lot easier for us maintainers and smooth out the experience for all involved. The community looks forward to your contributions. 🎉

> And if you like the project, but just don't have time to contribute, that's fine. There are other easy ways to support the project and show your appreciation, which we would also be very happy about:
> - Star the project
> - Tweet about it
> - Refer this project in your project's readme
> - Mention the project at local meetups and tell your friends/colleagues

<!-- omit in toc -->
## Table of Contents

- [I Have a Question](#i-have-a-question)
- [I Want To Contribute](#i-want-to-contribute)
  - [Reporting Bugs](#reporting-bugs)
  - [Suggesting Enhancements](#suggesting-enhancements)
  - [Your First Code Contribution](#your-first-code-contribution)
  - [Improving The Documentation](#improving-the-documentation)
- [Styleguides](#styleguides)
  - [Commit Messages](#commit-messages)
- [Join The Project Team](#join-the-project-team)

## I Have a Question

> If you want to ask a question, we assume that you have read the available [Documentation](README.md).

Before you ask a question, it is best to search for existing [Issues](https://github.com/MooVC/Fluentify/issues) that might help you. In case you have found a suitable issue and still need clarification, you can write your question in this issue. It is also advisable to search the internet for answers first.

If you then still feel the need to ask a question and need clarification, we recommend the following:

- Open an [Issue](https://github.com/MooVC/Fluentify/issues/new).
- Provide as much context as you can about what you're running into.
- Provide project and platform versions, depending on what seems relevant.

We will then take care of the issue as soon as possible.

## I Want To Contribute

> ### Legal Notice <!-- omit in toc -->
> When contributing to this project, you must agree that you have authored 100% of the content, that you have the necessary rights to the content and that the content you contribute may be provided under the project license.

### Reporting Bugs

<!-- omit in toc -->
#### Before Submitting a Bug Report

A good bug report shouldn't leave others needing to chase you up for more information. Therefore, we ask you to investigate carefully, collect information and describe the issue in detail in your report. Please complete the following steps in advance to help us fix any potential bug as fast as possible.

- Make sure that you are using the latest version.
- Determine if your bug is really a bug and not an error on your side e.g. using incompatible environment components/versions (Make sure that you have read the [documentation](README.md). If you are looking for support, you might want to check [this section](#i-have-a-question)).
- To see if other users have experienced (and potentially already solved) the same issue you are having, check if there is not already a bug report existing for your bug or error in the [bug tracker](https://github.com/MooVC/Fluentify/issues?q=label%3Abug).
- Also make sure to search the internet (including Stack Overflow) to see if users outside of the GitHub community have discussed the issue.
- Collect information about the bug:
  - Stack trace (Traceback)
  - OS, Platform and Version (Windows, Linux, macOS, x86, ARM)
  - Version of the interpreter, compiler, SDK, runtime environment, package manager, depending on what seems relevant.
  - Possibly your input and the output
  - Can you reliably reproduce the issue? And can you also reproduce it with older versions?

<!-- omit in toc -->
#### How Do I Submit a Good Bug Report?

> You must never report security related issues, vulnerabilities or bugs including sensitive information to the issue tracker, or elsewhere in public. Instead sensitive bugs must be reported [here](https://github.com/MooVC/Fluentify/security).

We use GitHub issues to track bugs and errors. If you run into an issue with the project:

- Open an [Issue](https://github.com/MooVC/Fluentify/issues/new). (Since we can't be sure at this point whether it is a bug or not, we ask you not to talk about a bug yet and not to label the issue.)
- Explain the behavior you would expect and the actual behavior.
- Please provide as much context as possible and describe the *reproduction steps* that someone else can follow to recreate the issue on their own. This usually includes your code. For good bug reports you should isolate the problem and create a reduced test case.
- Provide the information you collected in the previous section.

Once it's filed:

- The project team will label the issue accordingly.
- A team member will try to reproduce the issue with your provided steps. If there are no reproduction steps or no obvious way to reproduce the issue, the team will ask you for those steps and mark the issue as `needs-repro`. Bugs with the `needs-repro` tag will not be addressed until they are reproduced.
- If the team is able to reproduce the issue, it will be marked `needs-fix`, as well as possibly other tags (such as `critical`), and the issue will be left to be [implemented by someone](#your-first-code-contribution).

### Suggesting Enhancements

This section guides you through submitting an enhancement suggestion for Fluentify, **including completely new features and minor improvements to existing functionality**. Following these guidelines will help maintainers and the community to understand your suggestion and find related suggestions.

<!-- omit in toc -->
#### Before Submitting an Enhancement

- Make sure that you are using the latest version.
- Read the [documentation](https://github.com/MooVC/Fluentify/blob/master/README.md) carefully and find out if the functionality is already covered, maybe by an individual configuration.
- Perform a [search](https://github.com/MooVC/Fluentify/issues) to see if the enhancement has already been suggested. If it has, add a comment to the existing issue instead of opening a new one.
- Find out whether your idea fits with the scope and aims of the project. It's up to you to make a strong case to convince the project's developers of the merits of this feature. Keep in mind that we want features that will be useful to the majority of our users and not just a small subset. If you're just targeting a minority of users, consider writing an add-on/plugin library.

<!-- omit in toc -->
#### How Do I Submit a Good Enhancement Suggestion?

Enhancement suggestions are tracked as [GitHub issues](https://github.com/MooVC/Fluentify/issues).

- Use a **clear and descriptive title** for the issue to identify the suggestion.
- Provide a **step-by-step description of the suggested enhancement** in as many details as possible.
- **Describe the current behavior** and **explain which behavior you expected to see instead** and why. At this point you can also tell which alternatives do not work for you.
- You may want to **include screenshots and animated GIFs** which help you demonstrate the steps or point out the part which the suggestion is related to. You can use [this tool](https://www.cockos.com/licecap/) to record GIFs on macOS and Windows, and [this tool](https://github.com/colinkeenan/silentcast) or [this tool](https://github.com/GNOME/byzanz) on Linux.
- **Explain why this enhancement would be useful** to most Fluentify users. You may also want to point out the other projects that solved it better and which could serve as inspiration.

### Your First Code Contribution

#### 1. Fork the Repository

- Visit the Fluentify GitHub repository.
- Click on the "Fork" button to create your own copy of the repository.

#### 2. Clone the Repository

Clone your forked repository to your local machine using the following command:

```shell
git clone https://github.com/your-username/Fluentify.git
cd Fluentify
```

Replace `your-username` with your GitHub username.

#### 3. Set Up Your Development Environment

Ensure you have the following installed:

- **.NET SDK**: Fluentify is built on .NET, so you'll need the .NET SDK installed. You can download it from the [official .NET website](https://dotnet.microsoft.com/download).
- **IDE**: We recommend using Visual Studio or Visual Studio Code for development. Ensure that your IDE is set up to work with .NET projects.
- **Roslyn Analyzer SDK**: Since Fluentify is a Roslyn Source Generator, you may need the Roslyn Analyzer SDK for testing and debugging source generators.

#### 4. Install Dependencies

Navigate to the Fluentify directory and restore the necessary NuGet packages:

```shell
dotnet restore
```

#### 5. Build the Solution

Build the solution to ensure everything is set up correctly:

```shell
dotnet build
```

#### 6. Run Tests

Run the unit tests to verify that your environment is correctly configured:

```shell
dotnet test
```

All tests should pass if the setup is correct.

#### 7. Make Your Changes

- Create a new branch for your feature or bugfix:

  ```shell
  git checkout -b your-feature-branch
  ```

- Make the necessary changes in the codebase.
- Add or modify tests as needed.
- Ensure all tests pass by running `dotnet test` again.

#### 8. Commit and Push Your Changes

- Commit your changes with a clear and concise message:

  ```shell
  git add .
  git commit -m "Add feature X or Fix issue Y"
  ```

- Push your changes to your forked repository:

  ```shell
  git push origin your-feature-branch
  ```

#### 9. Create a Pull Request

- Go to the Fluentify repository on GitHub.
- You should see an option to create a pull request from your recently pushed branch.
- Provide a descriptive title and include any necessary details about your contribution.
- Submit the pull request for review.

### Improving The Documentation

Contributions to the documentation are crucial in making Fluentify more accessible and useful to everyone. While the [README.md](README.md) provides basic usage examples, there is significant potential for improvement.

All public and internal methods in the code are commented to aid in the extension and maintenance of the library. However, a dedicated contributor's guide would be a valuable addition to help new contributors navigate the project more effectively.

## Styleguides

To ensure consistency and maintainability across the Fluentify codebase, we follow a set of style guidelines based on the [Microsoft .NET Code Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions). Adhering to these guidelines is essential when contributing to the project. The [.editorconfig](https://github.com/MooVC/Fluentify/blob/master/.editorconfig), along with the [StyleCop.Analyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers) and [SonarAnalyzer.CSharp](https://www.nuget.org/packages/SonarAnalyzer.CSharp) have been configured to aid constributors in adherence. Please review all feedback provided by them, including the suggestions. Below are some of the key points:

### 1. C# Coding Conventions

- **File-Scoped Namespaces**: Use file-scoped namespaces to reduce indentation and improve readability.

  ```csharp
  namespace Fluentify;
  ```

- **Naming Conventions**:
  - Follow [Microsoft's .NET Naming Guidelines](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines).
  - Use PascalCase for class names, properties, and method names.
  - Use camelCase for local variables and method parameters.
  - Prefix interface names with "I" (e.g., `IService`).
  - Avoid abbreviations; prefer descriptive names that clearly convey the purpose.
  - Avoid overqualification; namespaces and type names for part of the context in which your code is authored.

- **Sealed Classes**: 
  - Where applicable, classes should be declared as `sealed` to prevent inheritance, especially when the class is not designed for extension.

  ```csharp
  public sealed class MyGenerator
  {
  }
  ```

- **Discards**: 
  - Utilize discards (`_`) for unused return values or parameters to indicate intentional disregard.

  ```csharp
  _ = SomeMethodReturningValue();
  ```

- **Extension Methods**: 
  - Create an extension method per type and per `public`/`internal` method.
  - Name the file `{Type Name}Extensions.{Method Name}.cs`.
  - Co-locate `private` methods or `static` fields that are used exclusively by the method.
  - Place common elements used by multiple extenson methods for a given type in a file named `{Type Name}Extensions.cs`.
 
- **Localization**
  - Use resource strings for all messaging produced by the library.
  - Create a resource file per type.
  - Name the file `{Type Name}.Resources.{locale}.resx`.
  - Name each resource string based on the context within which it is used.
  - Use the following pattern: `{Context}{Subject}{Purpose}`.
    - For a guard condition on parameter fileName on a method called Load, the following might be appropriate for a message that covers the need to specify the value: `LoadFileNameRequired`.
    - For a guard condition realting to a field or property called fileName supplied through a constructor, the following might be appropriate for a message that covers the need to specify the value: `FileNameRequired`.

### 2. Unit Testing Conventions

- **Test Structure**: Follow the Arrange, Act, Assert pattern for organizing tests.

  ```csharp
  // Arrange
  const int expected = 5;

  // Act
  var actual = Math.Max(expected, 4);

  // Assert
  actual.ShouldBe(expected);
  ```

- **Test Naming**: 
  - Place all tests associated with a given class in a namespace that matches `{Class Namespace}.{Class Name}Tests`. 
  - Create a class for each method tested using the naming convention `When{MethodName}IsCalled`.
  - Create a method for each test using the naming convention `Given{Condition}When{State}Then{Expectation}`.

- **Assertions**:
  - Use `FluentAssertions` for readable and expressive assertions.
  - Use `NSubstitute` for mocking dependencies in tests.
  - When asserting exceptions, especially `ArgumentExceptions`, check for the parameter name using `nameof`.

### 3. Documentation and Comments

- **XML Documentation**:
  - Provide XML documentation for public methods, properties, and classes to enhance IntelliSense and user guidance.
  
- **Code Comments**:
  - Use comments sparingly and only to explain complex logic or non-obvious decisions.
  - Prefer self-explanatory code over excessive comments.

### 4. Project Structure and Organization

- **Directory Structure**:
  - Organize code by feature or module to enhance clarity and ease of navigation.
  - Place unit tests in a separate test project that mirrors the structure of the main project.

- **Code Layout**:
  - Maintain a consistent code layout with proper spacing, alignment.
  - Never  use of regions.
  
By following these style guidelines, we ensure that the Fluentify codebase remains clean, consistent, and easy to maintain. Contributions that align with these practices are more likely to be accepted and integrated into the project.

<!-- omit in toc -->
## Attribution
This guide is based on the **contributing-gen**. [Make your own](https://github.com/bttger/contributing-gen)!
