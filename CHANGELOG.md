# Changelog
All notable changes to Fluentify will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

# [1.10.0] - TBC

## Changed

- Builder delegate overloads now reuse existing property values and throw a `NotSupportedException` when no value is available for non-buildable types or when auto instantiation is disabled.

# [1.9.0] - 2025-11-29

## Added

- Introduced analyzer `FLTFY08` to suggest when records annotated with `Fluentify` are not partial and need a generated parameterless constructor.

# [1.8.2] - 2025-11-22

## Fixed

- Types annotated with `SkipAutoInstantiation` no longer have auto instantiation overloads generated when the type serves as a argument to a collection property (#96).

# [1.8.1] - 2025-11-21

## Fixed

- Metadata associated with `FLTFY03` are now correctly added to the description (#89).
- `ImmutableArray`, `ImmutableHashSet`, `ImmutableList` and `ImmutableSortedSet` are now correctly identified as collection types (#93).

# [1.8.0] - 2025-11-14

## Added

- Introduced the `SkipAutoInstantiation` attribute, allowing consumers to opt out of generating builder-based overloads when a type, or a specific property, should not be automatically instantiated.

# [1.7.0] - 2025-10-17

## Changed

- Reverted **Microsoft.CodeAnalysis.Analyzers** to Version **3.11.0** to maximize compatibility with Visual Studio 2022.
- Reverted **Microsoft.CodeAnalysis.CSharp** to Version **4.0.1** to maximize compatibility with Visual Studio 2022.
- Reverted **Microsoft.CodeAnalysis.CSharp.Workspaces** Version **4.0.1** to maximize compatibility with Visual Studio 2022.
- Reverted **Microsoft.CodeAnalysis.Workspaces.Common** Version **4.0.1** to maximize compatibility with Visual Studio 2022.

# [1.6.0] - 2025-06-29

## Changed

- Updated **Microsoft.CodeAnalysis.Analyzers** to Version **4.14.0**.
- Updated **Microsoft.CodeAnalysis.CSharp** to Version **4.14.0**.
- Updated **Microsoft.CodeAnalysis.CSharp.Workspaces** to Version **4.14.0**.
- Updated **Microsoft.CodeAnalysis.Workspaces.Common** to Version **4.14.0**.

# [1.5.1] - 2025-04-17

## Fixed

- Generic constraints associated with nested types are now reflected on generated extension methods (#50).

# [1.5.0] - 2025-03-01

## Changed

- Updated **Microsoft.CodeAnalysis.CSharp** to Version **4.13.0** (#43).
- Updated **Microsoft.CodeAnalysis.CSharp.Workspaces** Version **4.13.0** (#43).
- Updated **Microsoft.CodeAnalysis.Workspaces.Common** Version **4.13.0** (#43).

# [1.4.0] - 2024-12-12

## Changed

- Updated **Microsoft.CodeAnalysis.CSharp** to Version **4.12.0** (#26).
- Updated **Microsoft.CodeAnalysis.CSharp.Workspaces** Version **4.12.0** (#26).
- Updated **Microsoft.CodeAnalysis.Workspaces.Common** Version **4.12.0** (#26).

# [1.3.0] - 2024-12-03

## Added

- Support for Fluentify on nested classes and records (#3).

## Fixed

- Corrected typo in documentation for analyzers relating to disabling custom rules.

# [1.2.0] - 2024-09-10

## Added

- `FLTFY07` is now raised if the usage of the `Descriptor` attribute results in the same value that Fluentify would selected by default.

## Changed

- The Descriptor attribute can now direct Fluentify to use the delcared name as the Descriptor without having to repeat the declared name (#9). 

# [1.1.1] - 2024-08-15

## Fixed

- The default constructor, generated for record types when marked as partial, now applies the fully qualified type when propagating the default value, thereby removing the potential for ambiguity with the projection constructor (#8).

# [1.1.0] - 2024-08-05

## Changed

- The fully qualified type information for the subject upon which Fluentify has been placed is now captured and utilized when generating extensions, thereby enabling support for nested classes (#3).
- Utilized **Microsoft.CodeAnalysis.CSharp.SourceGenerators.Testing** to improve the quality of the Generator tests.

## Fixed

- The generated code files no longer include the namespace if the containing namespace is global (#6).
- The generated code files for classes now apply a conditional preprocessor directive to #nullable calls to prevent compilation failure in .NET Standard 2.0 projects.
- The generated extensions no longer call `ArgumentNullException.ThrowIfNull` to prevent compilation failure in .NET Standard 2.0 projects.
- The generated extensions no longer use pattern matching to prevent compilation failure in .NET Standard 2.0 projects.
- The generated constructor for records now applies a preprocessor directive to the SetRequiredMembers attribute to prevent compilation failure in .NET 6 projects (#5).

# [1.0.0] - 2024-07-22

Initial Release

# [1.0.0-rc.3] - 2024-07-18

## Fixed

- Diagnostic Id documentation for `FLTFY01`.
- HelpUrl for diagnostics.

# [1.0.0-rc.2] - 2024-07-17

## Changed

- Nuget metadata to include DevelopmentDependency, instructing consumers to set PrivateAssets to all.
- Nuget metadata to include PackageReadmeFile and PackageTags.

# [1.0.0-rc.1] - 2024-07-16

## Changed

- Auto Instantiation extension overloads to use the `Func<T, T>` delegae in place of `Builder<T>`, addressing a warning relating to ambigious types when an assembly referencing Fluentify is referenced by another assembly that also uses Fluentify.
- Nuget package metadata.

## Removed

- The BuilderDelegateGenerator, used to generate the Builder<T> delegate, formally used by Fluentify to support Auto Instantiation and onward configuration.

# [1.0.0-alpha0007] - 2024-07-15

## Added

- Analyzer FLTFY01 that issues a Warning whenever the Fluentify attribute is attached to a class that does not have an accessible default constructor.
- Analyzer FLTFY02 that issues a Suggestion whenever the Descriptor attribute is attached to a property that is disregarded by Fluentify.
- Analyzer FLTFY03 that issues a Suggestion whenever the Descriptor attribute is attached to a property on a type that is not annotated with Fluentify.
- Analyzer FLTFY04 that issues a Warning whenever the value provided for the Descriptor attribute is deemed unsuitable for use as a method name.
- Analyzer FLTFY05 that issues a Suggestion whenever the Ignore attribute is attached to a property on a type that is not annotated with Fluentify.
- Analyzer FLTFY06 that issues a Suggestion whenever the Ignore attribute is attached to a property that is already disregarded by Fluentify.
- README.md to Nuget package.
- Security Policy.

## Changed

- The Builder<T> delegate is now public, enabling generated extensions to be called from outside the scope of the current assembly.

# [1.0.0-alpha0006] - 2024-06-20

## Changed

- The default descriptor for properties of type ``bool`` or ``bool?`` will now be the same name as the property, instead of ``With{PropertyName}``.

## Fixed

- An extension accepting a ``Builder<T>`` parameter is no longer generated for value types.

# [1.0.0-alpha0005] - 2024-06-15

## Added

- Params based extension generation for **IEnumerable<T>**, **IReadOnlyCollection<T>** and **IReadOnlyList<T>**, supporting additive operations.

# [1.0.0-alpha0004] - 2024-06-13

## Added

- Params based extension generation for types deriving from **ICollection<T>** that adhere to the **new()** constraint, supporting additive operations.

## Fixed

- **Class** instance **initialization** now correctly sets each member, including properties annotated with **Ignore**.
- The **generated constructor** for **partial record** types now correctly accounts for generic type parameters.

# [1.0.0-alpha0003] - 2024-06-11

## Added

- Params based extension generation for **Array** types, supporting additive operations.

## Fixed

- **Builder<T>** is now correctly invoked by the generated extension, applying the returned instance to the subject.

# [1.0.0-alpha0002] - 2024-06-08

## Added

- Extension generation on **class** type using the **Fluentify** attribute.

# [1.0.0-alpha0001] - 2024-05-30

## Added

- Extension generation per property on a **record** type using the **Fluentify** attribute.
- Custom descriptors for generated extensions on a per property basis using the **Descriptor** attribute.
- The ability to opt-out of extension generation via the **Ignore** attribute.
