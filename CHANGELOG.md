# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.8] - 2026-06-03

### Fixed

- Fixed UPM CLI installation path in sign and release workflow to avoid adding it to the package.

## [1.0.7] - 2026-05-18

### Security

- Added sign and release workflow to sign and release a UPM package when a Git tag is pushed.

## [1.0.6] - 2025-12-22

### Added

- Added support for getting child types of generic types with `IsSubclassOfGeneric`.
- Types sort alphabetically on their display strings in serialized type drawers and generic menus.

## [1.0.5] - 2025-10-19

### Added

- Added `hiddenTypes` property on the serialized type options attribute to hide abstract classes.

### Fixed

- Don't show empty namespace parentheses in nicified type display names.

## [1.0.4] - 2025-01-26

### Fixed

- Non-existing serialized types don't throw NullReferenceExceptions anymore.

## [1.0.3] - 2024-12-27

### Changed

- Updated source code license from GPL 3.0 to LGPL 3.0, so that larger works can use the library without disclosing their source code.

## [1.0.2] - 2024-12-10

### Changed

- Updated package information.

## [1.0.1] - 2024-06-16

### Changed

- Updated serialized type drawer to account for empty lists of types.

## [1.0.0] - 2024-04-11

### Added

- Serializable type class and editor code.
- Some extension and utility methods related to C# types.