# Implementation Plan: Maybe Helper Methods

**Branch**: `[001-maybe-helper-methods]` | **Date**: 2026-04-21 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/001-maybe-helper-methods/spec.md`

## Summary

Add the subset of `Either`-style helpers that stays semantically correct for `Maybe<TValue>`: `Map`, `Bind`, `GetOrElse`, `OrElse`, and `Tap`. Implement the helpers directly on [`src/Funcfy/Monads/Maybe.cs`](../../src/Funcfy/Monads/Maybe.cs), align delegate validation with the newer `Either` guard-clause style, preserve empty/full short-circuit semantics, and document the adopted subset plus the intentionally excluded second-branch helpers.

## Technical Context

**Language/Version**: C# 12 on .NET 8 (`net8.0`)  
**Primary Dependencies**: .NET base class library, `Microsoft.AspNetCore.App` shared framework reference for the package, xUnit, Shouldly, coverlet  
**Storage**: N/A  
**Testing**: `tests/Funcfy.Tests` with xUnit + Shouldly assertions and coverlet coverage collection  
**Target Platform**: Cross-platform .NET 8 class library distributed as a NuGet package  
**Project Type**: Library  
**Performance Goals**: Preserve the existing lightweight `Maybe<TValue>` branching model, avoid eager fallback execution, and avoid unnecessary allocations beyond the expected returned `Maybe<TResult>` instances for mapping/binding  
**Constraints**: Keep public API naming aligned with existing Funcfy monads, reject null delegates with explicit guard clauses, reject null `Maybe` instances returned from `Bind`/`OrElse`, and do not introduce helpers that imply a second payload branch  
**Scale/Scope**: One public monad type (`Maybe<TValue>`), focused unit-test additions under `MaybeTests`, and consumer-facing documentation updates for the optional-value guide and docs index

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- Public API impact identified: add public instance methods `Map`, `Bind`, `GetOrElse` overloads, `OrElse`, and `Tap` on `Maybe<TValue>`; update `Maybe<TValue>.Match` to throw `ArgumentNullException` for null delegates instead of failing later through an indirect `NullReferenceException`.
- Compatibility decision recorded: this is a backward-compatible API expansion with a minor SemVer impact; no deprecation or migration path is needed, but the behavior change in `Match` needs changelog coverage because it is an observable exception-contract improvement.
- Testing plan defined: add unit coverage for full-path transformation, empty-path short-circuiting, lazy fallback execution, same-instance preservation for `Tap`/`OrElse`, null delegate guard clauses, and invalid null results from `Bind` and `OrElse`.
- Documentation impact identified: update XML comments in `Maybe.cs`, `docs/maybe.md`, `docs/README.md`, and `CHANGELOG.md`; root `README.md` does not need feature-level edits unless final review finds a discoverability gap in the top-level summary.
- Performance/allocation review completed: `GetOrElse` and `OrElse` must stay lazy on full instances; `Tap` must return the original instance; implementation should avoid speculative helper abstractions or eager delegate invocation.
- Simplicity check passed: the change extends the existing `Maybe<TValue>` surface directly instead of introducing wrappers, extension-only APIs, or alternate optional abstractions.

## Project Structure

### Documentation (this feature)

```text
specs/001-maybe-helper-methods/
|-- plan.md
|-- research.md
|-- data-model.md
|-- quickstart.md
`-- contracts/
    `-- maybe-public-api.md
```

### Source Code (repository root)

```text
src/
`-- Funcfy/
    `-- Monads/
        `-- Maybe.cs

tests/
`-- Funcfy.Tests/
    `-- MonadsTests/
        `-- MaybeTests/
            |-- BindUnitTests.cs
            |-- GuardUnitTests.cs
            |-- MapUnitTests.cs
            |-- MatchUnitTests.cs
            |-- RecoveryUnitTests.cs
            `-- TapUnitTests.cs

docs/
|-- README.md
`-- maybe.md

CHANGELOG.md
.windsurf/rules/specify-rules.md
```

**Structure Decision**: Keep the work inside the existing monad type and its current documentation/test areas. `Maybe<TValue>` already lives in `src/Funcfy/Monads/Maybe.cs`, existing behavior tests live in `tests/Funcfy.Tests/MonadsTests/MaybeTests`, and public discoverability updates belong in `docs/maybe.md`, `docs/README.md`, and `CHANGELOG.md`.

## Phase 0: Research Outcomes

- Confirm the `Either` helper set should be mirrored selectively, not wholesale: `Map`, `Bind`, `GetOrElse`, `OrElse`, and `Tap` map cleanly onto presence/absence semantics, while `MapLeft` and `TapLeft` do not because `Maybe<TValue>` has no second payload branch.
- Standardize guard clauses on `ArgumentNullException.ThrowIfNull(...)` to match the newer style already used by `Either<TLeft, TRight>`.
- Preserve explicit invalid-state failures for delegates that are required to return `Maybe<TValue>` instances; `Bind` and `OrElse` must throw immediately if those delegates return `null`.
- Keep recovery APIs lazy for empty-path substitution by using a raw fallback overload plus a parameterless lazy overload for `GetOrElse`, and a parameterless lazy overload for `OrElse`.

## Phase 1: Design Plan

### API Design

- Add `Map<TResult>(Func<TValue, TResult> mapper)` returning `Maybe<TResult>`.
- Add `Bind<TResult>(Func<TValue, Maybe<TResult>> binder)` returning `Maybe<TResult>`.
- Add `GetOrElse(TValue fallback)` and `GetOrElse(Func<TValue> fallback)` returning `TValue`.
- Add `OrElse(Func<Maybe<TValue>> fallback)` returning `Maybe<TValue>`.
- Add `Tap(Action<TValue> onFull)` returning the original `Maybe<TValue>`.
- Update both `Match` overloads to validate delegates before dispatching.

### Test Design

- Add one test file per helper concern to mirror the `Either` test organization: mapping, binding, recovery, tapping, and guards.
- Reuse the existing `Maybe<TValue>` full/empty semantics in assertions rather than inventing new helper-specific test fixtures.
- Include regression tests for the explicit `Match` guard behavior change so the exception contract is locked in.

### Documentation Design

- Expand `docs/maybe.md` from creation and branching into composition, recovery, and observation guidance.
- Update `docs/README.md` so the current API surface for `Maybe<T>` lists the new helpers.
- Add an `Unreleased` changelog entry describing the new methods and the earlier null-delegate validation in `Match`.

## Phase 2: Implementation Strategy

1. Update `Maybe.cs` XML comments and add the new helper methods plus explicit delegate guards in `Match`.
2. Add focused unit tests under `tests/Funcfy.Tests/MonadsTests/MaybeTests`.
3. Refresh `docs/maybe.md`, `docs/README.md`, and `CHANGELOG.md`.
4. Run the targeted test suite, then the full test project if no environment issue blocks it.

## Post-Design Constitution Check

- Public API consistency remains intact because the adopted helpers reuse Funcfy's established names and right-biased/success-biased composition style adapted to full/empty semantics.
- Compatibility remains minor-only; the only observable behavior change is a more explicit guard-clause exception path in `Match`, which will be documented.
- Test and documentation work are first-class deliverables in the implementation plan, satisfying the constitution's non-negotiable coverage and discoverability requirements.
- No extra abstraction, namespace, or project structure was introduced.

## Complexity Tracking

No constitution violations or exceptional complexity are expected for this feature.
