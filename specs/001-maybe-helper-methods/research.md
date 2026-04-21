# Research: Maybe Helper Methods

## Decision 1: Mirror only the `Either` helpers that preserve `Maybe<TValue>` semantics

- Decision: Adopt `Map`, `Bind`, `GetOrElse`, `OrElse`, and `Tap` on `Maybe<TValue>`, and explicitly exclude `MapLeft` and `TapLeft`.
- Rationale: `Maybe<TValue>` models presence versus absence, not two payload-bearing branches. `Map`, `Bind`, recovery helpers, and side-effect observation all translate cleanly to full/empty behavior. Helpers that depend on a left payload would imply semantics that `Maybe<TValue>` does not have.
- Alternatives considered: Full parity with `Either<TLeft, TRight>` was rejected because it would add misleading API surface and violate the repository rule that public APIs must keep branch semantics obvious at the call site.

## Decision 2: Reuse the `Either` guard-clause style for all new `Maybe<TValue>` helpers

- Decision: Use `ArgumentNullException.ThrowIfNull(...)` for delegate parameters on `Match`, `Map`, `Bind`, `GetOrElse(Func<TValue>)`, `OrElse`, and `Tap`.
- Rationale: `Either<TLeft, TRight>` already uses this explicit pattern, and the spec calls out that `Maybe<TValue>.Match` should stop failing indirectly when a null delegate is supplied. Aligning `Maybe<TValue>` with that style keeps guard behavior predictable across the library.
- Alternatives considered: Leaving `Match` unchanged and only guarding the new helpers was rejected because it would preserve an inconsistent exception contract inside the same monad family. Using custom guard helpers was rejected because the repository favors direct, clear control flow over extra abstraction.

## Decision 3: Keep recovery helpers lazy and shaped around the absence-only branch

- Decision: Add `GetOrElse(TValue fallback)`, `GetOrElse(Func<TValue> fallback)`, and `OrElse(Func<Maybe<TValue>> fallback)`.
- Rationale: Unlike `Either`, `Maybe<TValue>` has no payload on the empty branch, so recovery delegates do not need an input parameter. A raw fallback overload covers simple defaults, while delegate overloads preserve laziness for expensive or stateful fallback creation. `OrElse` should stay lazy and return the original instance when already full.
- Alternatives considered: A `GetOrElse(Func<TValue, TValue>)`-style signature was rejected because there is no empty payload to pass. An eager `OrElse(Maybe<TValue> fallback)` overload was rejected because it weakens the laziness guarantee that the spec explicitly calls out and adds little semantic value over the lazy overload.

## Decision 4: Treat null `Maybe<T>` results from `Bind` and `OrElse` as invalid usage, not empty results

- Decision: Throw `InvalidOperationException` when `Bind` or `OrElse` delegates return `null`.
- Rationale: The spec requires the library to reject null `Maybe<TValue>` results so monadic state stays explicit and valid. Silent conversion from `null` to `Maybe.Empty()` would hide incorrect delegate implementations and blur the distinction between "no value" and "invalid helper contract."
- Alternatives considered: Converting `null` results to empty values was rejected because it masks bugs. Throwing `ArgumentNullException` was rejected because the delegate itself is not null; the returned `Maybe<TValue>` instance is invalid.

## Decision 5: Extend the existing `Maybe` documentation and test layout rather than introducing new feature-specific abstractions

- Decision: Implement the helpers directly in `src/Funcfy/Monads/Maybe.cs`, document them in `docs/maybe.md` and `docs/README.md`, and add focused unit tests under `tests/Funcfy.Tests/MonadsTests/MaybeTests`.
- Rationale: The repository already organizes monad behavior by primitive, and `Either` provides a strong local precedent for both method naming and test grouping. Staying inside the current structure keeps the feature discoverable and easy to review.
- Alternatives considered: Extension-method-based helpers or a separate optional-composition utility type were rejected because they would fragment the `Maybe<TValue>` API and add unnecessary abstraction for behavior that belongs on the core monad itself.
