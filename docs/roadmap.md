# Roadmap

## Library Scope

The intended scope of `funcfy` is centered on these feature areas:

- currying and partial application
- `Either`, `Maybe`, and `Result` types
- `Functor`, `Applicative`, and `Monad` interfaces
- lens utilities, compose, and pipe helpers

## Current

- `Maybe<T>` with creation helpers, implicit conversion, and `Match`
- `Result` and `Result<T>` with structured messages
- success/failure `Match` support for `Result` and `Result<T>`
- typed message helpers for common application and HTTP-style error cases
- ASP.NET Core `IActionResult` conversion for non-generic results
- CI/release workflow separation for tagging and prerelease publishing

## Near Term

- improve package/runtime consistency in test execution and ASP.NET Core integration tests
- add focused docs for `Maybe<T>`, `Result`, and result extension helpers
- add examples for common service-layer and controller-layer usage
- improve release notes and changelog visibility

## Next

- add `Either` as a first-class monad in the library surface
- introduce currying and partial application helpers
- define foundational `Functor`, `Applicative`, and `Monad` interfaces
- add compose and pipe helpers for function composition
- expand result-to-HTTP integration for generic results
- document guidance for message codes, sources, and error handling conventions
- add richer composition examples for `Maybe<T>` and `Result<T>`

## Later

- evaluate lens utilities once the core monad and composition APIs are stable
- evaluate whether to add additional monads or functional helpers only after the current primitives are well documented and stable
- consider a dedicated documentation site if the `docs/` folder grows beyond repository-scale guides
