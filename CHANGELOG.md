# Changelog

All notable changes to `funcfy` should be documented in this file.

This project follows a lightweight changelog format:

- keep an `Unreleased` section at the top
- move entries into a versioned section when publishing a release
- group entries under `Added`, `Changed`, `Fixed`, and `Documentation`

## Unreleased

### Added

- Introduced `Either<TLeft, TRight>` as a first-class right-biased monad with `Left`/`Right` factories, `Match`, `Map`, `MapLeft`, `Bind`, recovery helpers, and side-effect helpers.
- Added `Either` conversion helpers for `Maybe<T>` and one-way conversion to `Result<T>`.
- Integration-style ASP.NET Core coverage for `Result.ToActionResult()` execution.
- Additional service-layer and controller-layer examples for `Maybe<T>`, `Result`, and ASP.NET Core usage.

### Changed

- Replaced legacy ASP.NET Core MVC package references with the .NET 8 shared framework reference.
- Updated documentation navigation so examples, roadmap, and release notes are easier to find.

### Fixed

- Added automated coverage for `Either` branch equality, monad laws, lazy fallback behavior, and delegate guard clauses.

### Documentation

- Added `CHANGELOG.md` and linked it from the root README and docs index.
- Added `Either` documentation and surfaced it in the root README and docs index.
