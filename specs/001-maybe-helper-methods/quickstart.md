# Quickstart: Maybe Helper Methods

## Goal

Validate that `Maybe<TValue>` supports transform, chain, recover, and observe flows without falling back to manual `Match` branches for common optional-value operations.

## Implementation Checklist

1. Extend [`src/Funcfy/Monads/Maybe.cs`](../../src/Funcfy/Monads/Maybe.cs) with `Map`, `Bind`, `GetOrElse`, `OrElse`, and `Tap`.
2. Add explicit null-delegate guards to both `Match` overloads and to every new helper that accepts delegates.
3. Reject null `Maybe<T>` results from `Bind` and `OrElse` with `InvalidOperationException`.
4. Add focused tests under `tests/Funcfy.Tests/MonadsTests/MaybeTests`.
5. Update XML comments, `docs/maybe.md`, `docs/README.md`, and `CHANGELOG.md`.

## Expected Usage

### Compose optional values

```csharp
Maybe<int> maybe = 21;

var doubled = maybe
    .Map(value => value * 2)
    .Bind(value => value > 0 ? Maybe<int>.Full(value + 1) : Maybe<int>.Empty());
```

Expected result:

- `doubled.IsFull` is `true`
- `doubled.Value` is `43`

### Recover from absence

```csharp
var empty = Maybe<int>.Empty();

var rawValue = empty.GetOrElse(() => 99);
var recovered = empty.OrElse(() => Maybe<int>.Full(99));
```

Expected result:

- `rawValue` is `99`
- `recovered.IsFull` is `true`
- the fallback delegates run only for the empty branch

### Observe without altering the pipeline

```csharp
Maybe<int> maybe = 10;
var observed = 0;

var same = maybe.Tap(value => observed = value);
```

Expected result:

- `observed` is `10`
- `ReferenceEquals(maybe, same)` is `true`

## Suggested Verification Commands

```powershell
dotnet test tests/Funcfy.Tests/Funcfy.Tests.csproj --filter "FullyQualifiedName~MaybeTests"
dotnet test tests/Funcfy.Tests/Funcfy.Tests.csproj
```

## Acceptance Focus

- Full instances transform and chain correctly.
- Empty instances short-circuit `Map` and `Bind`.
- Recovery helpers are lazy on full instances.
- `Tap` observes only full values and returns the original instance.
- Null delegates fail fast with `ArgumentNullException`.
- Null `Maybe<T>` results from `Bind` and `OrElse` fail fast with `InvalidOperationException`.
