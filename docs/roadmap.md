# Roadmap

## Library Scope

The intended scope of `funcfy` is centered on these feature areas:

- `Either`, `Maybe`, and `Result` types
- currying and partial application
- `Functor`, `Applicative`, and `Monad` interfaces
- lens utilities, compose, and pipe helpers

## Current

- `Maybe<T>` with creation helpers, implicit conversion, and `Match`
- `Result` and `Result<T>` with structured messages
- success/failure `Match` support for `Result` and `Result<T>`
- typed message helpers for common application and HTTP-style error cases
- ASP.NET Core `IActionResult` conversion for non-generic results
- .NET 8-aligned ASP.NET Core runtime references and integration-style action result tests
- focused docs and service/controller examples for `Maybe<T>`, `Result`, and ASP.NET Core usage
- root changelog and release-notes visibility from the README and docs index
- CI/release workflow separation for tagging and prerelease publishing

## Near Term

- add `Either` as a first-class monad in the library surface
- expand result-to-HTTP integration for generic `Result<T>`
- document guidance for message codes, sources, and error handling conventions

## Next

- evaluate multi-targeting for `net8.0`, `net9.0`, and `net10.0`
- define package validation coverage for each supported target framework
- introduce currying and partial application helpers
- define foundational `Functor`, `Applicative`, and `Monad` interfaces
- add compose and pipe helpers for function composition
- add richer composition examples for `Maybe<T>` and `Result<T>`

## Later

- evaluate lens utilities once the core monad and composition APIs are stable
- evaluate whether to add additional monads or functional helpers only after the current primitives are well documented and stable
- consider a dedicated documentation site if the `docs/` folder grows beyond repository-scale guides
