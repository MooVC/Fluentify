# Changelog
All notable changes to Fluentify will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

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