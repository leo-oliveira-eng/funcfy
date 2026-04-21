# Data Model: Maybe Helper Methods

## Overview

This feature does not add persisted data structures. Its design model is the public behavioral contract of `Maybe<TValue>` and the delegate-driven transitions introduced by the new helper methods.

## Entity: `Maybe<TValue>`

### Fields and Derived State

| Field / Property | Type | Description |
|---|---|---|
| `Value` | `TValue?` | Wrapped value stored by the instance. |
| `IsFull` | `bool` | `true` when the instance represents a present value. |
| `IsEmpty` | `bool` | `true` when the instance represents absence according to the current `Maybe<TValue>` rules (`null` or `default(TValue)`). |

### Invariants

- `IsFull` and `IsEmpty` remain complementary derived states.
- The existing emptiness rule is unchanged by this feature.
- Helper methods must never fabricate a second payload branch or hidden state.

### State Transitions

| Operation | Full Instance | Empty Instance |
|---|---|---|
| `Map` | Invokes mapper and returns `Maybe<TResult>.Create(mappedValue)` | Returns `Maybe<TResult>.Empty()` without invoking mapper |
| `Bind` | Invokes binder and returns its non-null `Maybe<TResult>` result | Returns `Maybe<TResult>.Empty()` without invoking binder |
| `GetOrElse` | Returns the wrapped value | Returns fallback value, evaluating delegate fallback only on this branch |
| `OrElse` | Returns the same instance | Invokes fallback and returns its non-null `Maybe<TValue>` result |
| `Tap` | Invokes side effect and returns the same instance | Returns the same instance without invoking the side effect |
| `Match` | Invokes `onFull` | Invokes `onEmpty` |

## Entity: Helper Delegate Contract

### Fields

| Field | Type | Description |
|---|---|---|
| `Kind` | enum-like classification | One of mapper, binder, fallback-value factory, fallback-maybe factory, observer, or match branch handler |
| `Invocation Branch` | branch rule | Determines whether the delegate runs on full, empty, or both branches |
| `Return Shape` | raw value or `Maybe<T>` | Distinguishes transforms from monadic continuations |
| `Null Policy` | validation rule | Whether null delegate references or null delegate results are allowed |

### Validation Rules

- All delegate parameters are required and must throw `ArgumentNullException` when null.
- `Bind` delegates must return a non-null `Maybe<TResult>`.
- `OrElse` delegates must return a non-null `Maybe<TValue>`.
- `GetOrElse` delegates may return any raw `TValue`, because they do not produce a `Maybe<TValue>` instance.

## Relationships

- A `Maybe<TValue>` conditionally invokes a helper delegate based on whether it is full or empty.
- `Map` and `Bind` transition from one `Maybe` instance to another `Maybe` instance while preserving short-circuit semantics on emptiness.
- `GetOrElse` and `OrElse` define the recovery boundary for empty instances.
- `Tap` is observational only and must not alter the current `Maybe<TValue>` state.

## Test-Relevant Rules

- Full-path composition must preserve transformed values and skip empty-path work.
- Empty-path recovery must remain lazy.
- `Tap` and full-path `OrElse` must preserve reference identity by returning the original instance.
- Invalid delegate contracts must fail immediately and explicitly.
