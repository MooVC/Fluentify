# Changelog
All notable changes to Fluentify will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.0.0-alpha0006] - 2024-06-19

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