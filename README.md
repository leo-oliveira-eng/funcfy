<p align="center">
  <img
    alt="funcfy - A lightweight, batteries-included functional programming toolkit for C#/.NET."
    src="https://github.com/leo-oliveira-eng/funcfy/blob/main/images/logo.png"
    height="320px"
  />
</p>

[![CI - Build & Test](https://github.com/leo-oliveira-eng/funcfy/actions/workflows/build.yml/badge.svg)](https://github.com/leo-oliveira-eng/funcfy/actions/workflows/build.yml) [![artifact](https://github.com/leo-oliveira-eng/funcfy/actions/workflows/artifact.yml/badge.svg)](https://github.com/leo-oliveira-eng/funcfy/actions/workflows/artifact.yml) [![CodeQL](https://github.com/leo-oliveira-eng/funcfy/actions/workflows/codeql.yml/badge.svg)](https://github.com/leo-oliveira-eng/funcfy/actions/workflows/codeql.yml) [![Dependabot Updates](https://github.com/leo-oliveira-eng/funcfy/actions/workflows/dependabot/dependabot-updates/badge.svg)](https://github.com/leo-oliveira-eng/funcfy/actions/workflows/dependabot/dependabot-updates) [![Prerelease](https://github.com/leo-oliveira-eng/funcfy/actions/workflows/prerelease.yml/badge.svg?branch=main)](https://github.com/leo-oliveira-eng/funcfy/actions/workflows/prerelease.yml) [![Release](https://github.com/leo-oliveira-eng/funcfy/actions/workflows/release.yml/badge.svg)](https://github.com/leo-oliveira-eng/funcfy/actions/workflows/release.yml) [![codecov](https://codecov.io/gh/leo-oliveira-eng/funcfy/graph/badge.svg?token=EBEQ9TTZBG)](https://codecov.io/gh/leo-oliveira-eng/funcfy) [![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md) ![NuGet Version](https://img.shields.io/nuget/vpre/funcfy)


# funcfy

A lightweight, batteries-included functional programming toolkit for C#/.NET.

## Why Funcfy

Functional programming can make application code easier to reason about when it is used in the right places. `funcfy` is not trying to force a pure functional style across an entire .NET codebase. The goal is to provide a small set of practical tools that help developers model optional values, success/failure flows, and explicit branching in a clear and predictable way.

In practice, this can help you:

- reduce null-related checks and make missing values explicit with `Maybe<T>`
- make recoverable failure explicit with `Either<TLeft, TRight>`
- represent operation outcomes without relying only on exceptions for expected business cases
- keep success and failure handling close to the code path through `Match`
- carry structured messages that are easier to translate into API responses
- make service and application-layer code more expressive while staying familiar to C# developers

## What It Includes

- `Maybe<T>` for optional values
- `Either<TLeft, TRight>` for right-biased success/failure composition
- `Result` and `Result<T>` for success/failure flows with typed messages
- `Match` APIs for `Maybe<T>`, `Either<TLeft, TRight>`, `Result`, and `Result<T>`
- message helpers and typed error categories
- ASP.NET Core helpers to convert results into `IActionResult`

## Install

```bash
dotnet add package Funcfy
```

## Documentation

For detailed guides, API references, examples, and more, explore our documentation:

- **[Getting Started](./docs/README.md)** - Quick start guide to using funcfy in your projects
- **[Maybe](./docs/maybe.md)** - Optional-value patterns with service and controller examples
- **[Either](./docs/either.md)** - Explicit recoverable failures with right-biased composition
- **[Result](./docs/result.md)** - Success/failure flows, messages, and extension helpers
- **[ASP.NET Core](./docs/aspnetcore.md)** - Controller translation patterns and HTTP mapping
- **[API Reference](./docs/README.md)** - Complete documentation of all types, methods, and extensions
- **[Roadmap](./docs/roadmap.md)** - Upcoming features and planned improvements
- **[Changelog](./CHANGELOG.md)** - Release notes and noteworthy package/runtime changes

Whether you're a developer looking to use funcfy or a contributor wanting to understand the codebase, our docs have you covered.

## Contributing

1. Fork the repository.
2. Create a feature branch from `develop`.
3. Commit your changes using a clear conventional-style message.
4. Push your branch and open a pull request to `develop`.
