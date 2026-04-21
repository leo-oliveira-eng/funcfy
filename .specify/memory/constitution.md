<!--
Sync Impact Report
- Version change: template -> 0.1.0
- Ratification status: initial pre-1.0 constitution ratification
- Modified principles:
  - Template Principle 1 -> I. Small, Composable Public APIs
  - Template Principle 2 -> II. Compatibility Before Novelty
  - Template Principle 3 -> III. Behavior-Proving Tests (NON-NEGOTIABLE)
  - Template Principle 4 -> IV. Clear, Cohesive Implementations
  - Template Principle 5 -> V. Discoverable Documentation and Explicit Performance Tradeoffs
- Added sections:
  - Repository Standards
  - Workflow & Review
- Removed sections:
  - None
- Templates requiring updates:
  - updated .specify/templates/plan-template.md
  - updated .specify/templates/spec-template.md
  - updated .specify/templates/tasks-template.md
  - pending .specify/templates/commands/ (directory not present in this repository)
- Follow-up TODOs:
  - None
-->
# Funcfy Constitution

## Core Principles

### I. Small, Composable Public APIs
Funcfy MUST preserve a compact public surface centered on practical functional
primitives for .NET: `Maybe<T>`, `Either<TLeft, TRight>`, `Result`,
`Result<T>`, `Message`, message categories, and narrowly-scoped extension
helpers. New public APIs MUST earn their place by improving composition across
existing primitives rather than introducing parallel abstractions, alternate
naming vocabularies, or framework-style magic. Public members MUST use explicit,
predictable names such as `Create`, `Success`, `Failure`, `Match`, `Map`,
`Bind`, `GetOrElse`, and `ToResult`; they MUST make branch semantics obvious at
the call site. Consumer ergonomics matter, but convenience MUST NOT hide
important semantics such as emptiness rules, failure signaling, message
propagation, branch bias, or HTTP translation behavior.

### II. Compatibility Before Novelty
Every change to a public type, public member, observable serialization shape,
package target, or documented behavior MUST be treated as a compatibility
decision. Breaking changes MUST have written justification, an explicit SemVer
impact, changelog coverage, and migration guidance before implementation begins.
Deprecation is preferred over abrupt removal: contributors SHOULD mark outgoing
APIs as obsolete, document the replacement path, and keep old and new behavior
coexisting for at least one released minor version unless security or
correctness requires faster removal. New abstractions MUST match existing
Funcfy style before they are admitted: right-biased composition stays right-
biased, result/message helpers stay explicit, and factory/match patterns stay
consistent across the library.

### III. Behavior-Proving Tests (NON-NEGOTIABLE)
All new behavior, public API additions, bug fixes, and behavior-changing
refactors MUST ship with automated tests. Tests MUST verify observable behavior
and edge cases, not just implementation details, and MUST be readable enough
that contributors can use them as executable examples. For Funcfy, that means
covering branch semantics, null/default handling, message accumulation,
conversion behavior, guard clauses, and any monad or composition laws the type
claims to support. Refactors MUST preserve or improve confidence in existing
behavior by keeping relevant tests green and adding regression coverage where
the previous suite was thin.

### IV. Clear, Cohesive Implementations
Implementation work MUST optimize for clarity first. Types and extension classes
MUST keep narrow responsibilities, and code MUST favor direct control flow over
indirection, speculative hooks, or abstraction layers that exist only for
future possibility. Small helper methods and focused extension classes are
preferred; hidden state, broad utility buckets, and speculative generalization
are not. Performance work is welcome where it matters, but maintainability is
the default: contributors MUST be able to explain why a type exists, what
invariants it preserves, and how it composes with the rest of the library.

### V. Discoverable Documentation and Explicit Performance Tradeoffs
Public APIs that affect discoverability, usage patterns, or semantics MUST be
documented in XML comments and, when relevant to consumers, in package-facing
docs such as `README.md`, `docs/README.md`, and the focused guides under
`docs/`. Documentation MUST explain intent and tradeoffs, not just mechanics,
and usage examples MUST resemble realistic service-layer, controller-layer, or
application-flow scenarios already present in Funcfy docs. Contributors MUST be
allocation-aware in hot paths and composition-heavy helpers: avoid unnecessary
copying, closure churn, and indirection when the path is expected to be used
frequently. However, API clarity wins by default; a less clear API requires
measured or strongly justified performance need before adoption.

## Repository Standards

- The repository target is `.NET 8` and the package entry point is
  `src/Funcfy/Funcfy.csproj`; new work MUST preserve packability, nullable
  annotations, XML documentation output, and NuGet-facing metadata quality.
- Public API additions SHOULD live in the existing package structure unless a
  new namespace is clearly justified. Current structure is the baseline:
  `Monads/`, `Monads/Extensions/`, `Monads/Enums/`, `Extensions/`, and
  `Types/`.
- API design SHOULD follow the repository's existing patterns:
  records for immutable value-like abstractions where appropriate, explicit
  factory methods over ambiguous constructors, `Match` for branching, and
  extension methods for fluent message decoration or conversions.
- Tests live under `tests/Funcfy.Tests` and SHOULD stay organized by primitive
  or extension area. New test files MUST be named for the behavior they verify,
  following the repository's `*UnitTests.cs` and integration-style naming
  patterns.
- Documentation changes are required when public behavior changes. At minimum,
  contributors MUST update whichever of `README.md`, `docs/README.md`,
  primitive-specific guides, and `CHANGELOG.md` are affected by the change.
- CI is part of the definition of done. Changes MUST keep the cross-platform
  build-and-test workflow passing, preserve coverage collection, and maintain
  release/tagging assumptions based on SemVer tags and PR labels.

## Workflow & Review

- Specifications and plans MUST identify any public API impact, compatibility
  risk, documentation impact, and performance-sensitive code paths before
  implementation starts.
- Pull requests MUST be reviewed against this constitution, not only local
  preference. Reviewers MUST check API consistency, test adequacy, docs impact,
  SemVer implications, and whether the change adds avoidable abstraction.
- Exceptions are allowed only when the pull request documents the rationale,
  scope, and rollback or migration consequences. Silence is not an exception.
- Breaking changes, deprecations, or semantics changes MUST call out affected
  consumer code in the PR description and in `CHANGELOG.md`.
- Constitution changes MUST be rare, explicit, and justified by repository-wide
  needs. They are not a shortcut for approving a one-off implementation.

## Governance

This constitution overrides conflicting local habits, ad hoc preferences, and
spec defaults for this repository. Every spec, plan, task list, implementation,
and review MUST demonstrate compliance with these principles or document a
deliberate exception.

Semantic versioning is mandatory for Funcfy:

- `MAJOR` for breaking public API or behavior changes.
- `MINOR` for backward-compatible public capabilities.
- `PATCH` for backward-compatible fixes, documentation corrections, and
  internal improvements with unchanged public behavior.

Deprecation policy is part of compatibility governance:

- Deprecated APIs MUST identify the preferred replacement.
- Removal MUST be announced through documentation and changelog entries.
- Migration guidance MUST be shipped before or alongside any removal.

Amendment policy:

- Constitution amendments require an explicit pull request that explains why the
  existing rule is insufficient for Funcfy.
- Amendments MUST include any required updates to templates or contributor
  guidance in the same change when practical.
- Versioning of this constitution follows SemVer:
  major for incompatible governance changes, minor for new or materially
  expanded principles, patch for clarifications only.

Compliance review expectations:

- Plans MUST include a constitution check before design and before
  implementation.
- Tasks MUST include testing, documentation, and compatibility work whenever the
  change affects behavior or public APIs.
- Reviewers and implementers share responsibility for enforcing these rules.

**Version**: 0.1.0 | **Ratified**: 2026-04-21 | **Last Amended**: 2026-04-21
