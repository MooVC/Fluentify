# Changelog
All notable changes to Fluentify will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.1.1] - 2024-08-15

## Fixed

- The default constructor, generated for record types when marked as partial, now applies the fully qualified type when propagating the default value, thereby removing the potential for ambiguity with the projection constructor (#8).

## [1.1.0] - 2024-08-05

## Changed

- The fully qualified type information for the subject upon which Fluentify has been placed is now captured and utilized when generating extensions, thereby enabling support for nested classes (#3).
- Utilized Microsoft.CodeAnalysis.CSharp.SourceGenerators.Testing to improve the quality of the Generator tests.

## Fixed

- The generated code files no longer include the namespace if the containing namespace is global (#6).
- The generated code files for classes now apply a conditional preprocessor directive to #nullable calls to prevent compilation failure in .NET Standard 2.0 projects.
- The generated extensions no longer call ArgumentNullException.ThrowIfNull to prevent compilation failure in .NET Standard 2.0 projects.
- The generated extensions no longer use pattern matching to prevent compilation failure in .NET Standard 2.0 projects.
- The generated constructor for records now applies a preprocessor directive to the SetRequiredMembers attribute to prevent compilation failure in .NET 6 projects (#5).

## [1.0.0] - 2024-07-22

Initial Release

## [1.0.0-rc.3] - 2024-07-18

### Fixed

- Diagnostic Id documentation for FLTFY01.
- HelpUrl for diagnostics.

## [1.0.0-rc.2] - 2024-07-17

### Changed

- Nuget metadata to include DevelopmentDependency, instructing consumers to set PrivateAssets to all.
- Nuget metadata to include PackageReadmeFile and PackageTags.

## [1.0.0-rc.1] - 2024-07-16

### Changed

- Auto Instantiation extension overloads to use the `Func<T, T>` delegae in place of `Builder<T>`, addressing a warning relating to ambigious types when an assembly referencing Fluentify is referenced by another assembly that also uses Fluentify.
- Nuget package metadata.

### Removed

- The BuilderDelegateGenerator, used to generate the Builder<T> delegate, formally used by Fluentify to support Auto Instantiation and onward configuration.

## [1.0.0-alpha0007] - 2024-07-15

### Added

- Analyzer FLTFY01 that issues a Warning whenever the Fluentify attribute is attached to a class that does not have an accessible default constructor.
- Analyzer FLTFY02 that issues a Suggestion whenever the Descriptor attribute is attached to a property that is disregarded by Fluentify.
- Analyzer FLTFY03 that issues a Suggestion whenever the Descriptor attribute is attached to a property on a type that is not annotated with Fluentify.
- Analyzer FLTFY04 that issues a Warning whenever the value provided for the Descriptor attribute is deemed unsuitable for use as a method name.
- Analyzer FLTFY05 that issues a Suggestion whenever the Ignore attribute is attached to a property on a type that is not annotated with Fluentify.
- Analyzer FLTFY06 that issues a Suggestion whenever the Ignore attribute is attached to a property that is already disregarded by Fluentify.
- README.md to Nuget package.
- Security Policy.

### Changed

- The Builder<T> delegate is now public, enabling generated extensions to be called from outside the scope of the current assembly.

## [1.0.0-alpha0006] - 2024-06-20

### Changed

- The default descriptor for properties of type ``bool`` or ``bool?`` will now be the same name as the property, instead of ``With{PropertyName}``.

### Fixed

- An extension accepting a ``Builder<T>`` parameter is no longer generated for value types.

## [1.0.0-alpha0005] - 2024-06-15

### Added

- Params based extension generation for **IEnumerable<T>**, **IReadOnlyCollection<T>** and **IReadOnlyList<T>**, supporting additive operations.

## [1.0.0-alpha0004] - 2024-06-13

### Added

- Params based extension generation for types deriving from **ICollection<T>** that adhere to the **new()** constraint, supporting additive operations.

### Fixed

- **Class** instance **initialization** now correctly sets each member, including properties annotated with **Ignore**.
- The **generated constructor** for **partial record** types now correctly accounts for generic type parameters.

## [1.0.0-alpha0003] - 2024-06-11

### Added

- Params based extension generation for **Array** types, supporting additive operations.

### Fixed

- **Builder<T>** is now correctly invoked by the generated extension, applying the returned instance to the subject.

## [1.0.0-alpha0002] - 2024-06-08

### Added

- Extension generation on **class** type using the **Fluentify** attribute.

## [1.0.0-alpha0001] - 2024-05-30

### Added

- Extension generation per property on a **record** type using the **Fluentify** attribute.
- Custom descriptors for generated extensions on a per property basis using the **Descriptor** attribute.
- The ability to opt-out of extension generation via the **Ignore** attribute.