# Contract: `Maybe<TValue>` Helper Methods

## Scope

This contract defines the public API additions and observable behavior changes for the `001-maybe-helper-methods` feature.

## New Public Members

```csharp
public sealed record Maybe<TValue>
{
    public Maybe<TResult> Map<TResult>(Func<TValue, TResult> mapper);
    public Maybe<TResult> Bind<TResult>(Func<TValue, Maybe<TResult>> binder);
    public TValue GetOrElse(TValue fallback);
    public TValue GetOrElse(Func<TValue> fallback);
    public Maybe<TValue> OrElse(Func<Maybe<TValue>> fallback);
    public Maybe<TValue> Tap(Action<TValue> onFull);
}
```

## Updated Existing Member Contract

```csharp
public TResult Match<TResult>(Func<TValue, TResult> onFull, Func<TResult> onEmpty);
public void Match(Action<TValue> onFull, Action onEmpty);
```

Updated behavior:

- `Match` must throw `ArgumentNullException` when any delegate argument is null.

## Behavioral Guarantees

- `Map` transforms only full values and returns an empty `Maybe<TResult>` when the source is empty.
- `Bind` invokes its delegate only for full values and short-circuits to an empty `Maybe<TResult>` when the source is empty.
- `Bind` must throw `InvalidOperationException` if the binder returns `null`.
- `GetOrElse(TValue fallback)` returns the wrapped value for full instances and the supplied raw fallback for empty instances.
- `GetOrElse(Func<TValue> fallback)` is lazy and invokes the fallback only when the instance is empty.
- `OrElse(Func<Maybe<TValue>> fallback)` is lazy, returns the same instance when already full, and must throw `InvalidOperationException` if the fallback returns `null`.
- `Tap(Action<TValue> onFull)` invokes the action only for full instances and returns the original instance unchanged.

## Explicit Non-Goals

- No `MapLeft` equivalent will be added because `Maybe<TValue>` has no second payload branch.
- No `TapLeft` equivalent will be added for the same reason.
- No semantics of emptiness are changed; `Maybe<TValue>` keeps its existing `null`/`default(TValue)` empty-state rule.
