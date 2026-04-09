# Either

## What problem `Either<TLeft, TRight>` solves

Some operations can fail in ways that are expected, meaningful, and recoverable.

Examples:

- validation can fail
- parsing can fail
- authorization checks can fail
- domain rules can reject input

Using exceptions for those outcomes can hide the failure path from the method signature.

`Either<TLeft, TRight>` makes that branch explicit.

Instead of returning only a success value and relying on exceptions for expected problems, the API says:

- `Left<TLeft>` contains the failure value
- `Right<TRight>` contains the success value

In `funcfy`, `Either` is right-biased. That means mapping and binding continue through the `Right` branch and short-circuit on `Left`.

## The theory, in practical terms

`Either<TLeft, TRight>` is a common functional abstraction for computations with two possible outcomes.

The important practical ideas are:

- failure is explicit in the return type
- `Right` is treated as the successful branch
- `Map` transforms only the success value
- `Bind` chains computations that also return `Either`
- once a computation becomes `Left`, later binds do not run
- `Match` forces the caller to handle both branches

This makes `Either` a strong fit for recoverable domain and application errors.

It is not intended to replace exceptions for programmer mistakes or unrecoverable system failures.

## Main API

`Either<TLeft, TRight>` currently provides:

- `Either.Left<L, R>(value)`
- `Either.Right<L, R>(value)`
- `IsLeft`
- `IsRight`
- `Match`
- `Map`
- `MapLeft`
- `Bind`
- `GetOrElse`
- `OrElse`
- `Tap`
- `TapLeft`
- `ToMaybe`
- `ToResult`

## Creating values

### Left

```csharp
var either = Either.Left<string, int>("validation failed");

Console.WriteLine(either.IsLeft);  // true
Console.WriteLine(either.IsRight); // false
```

### Right

```csharp
var either = Either.Right<string, int>(42);

Console.WriteLine(either.IsLeft);  // false
Console.WriteLine(either.IsRight); // true
```

## Branching with `Match`

`Match` is the canonical way to handle both branches explicitly.

```csharp
var either = Either.Right<string, int>(21);

var text = either.Match(
    onLeft: error => $"Error: {error}",
    onRight: value => $"Value: {value * 2}"
);
```

Because both handlers are required, the caller must deal with success and failure in one place.

## Mapping success with `Map`

Because `Either` is right-biased, `Map` only transforms the `Right` branch:

```csharp
var result = Either.Right<string, int>(21)
    .Map(value => value * 2);
```

If the value is already `Left`, `Map` does nothing and preserves the failure branch.

## Chaining with `Bind`

`Bind` is what makes `Either` a monad in practice.

It lets you sequence computations that each return `Either`.

```csharp
Either<string, int> Parse(string input)
    => int.TryParse(input, out var value)
        ? Either.Right<string, int>(value)
        : Either.Left<string, int>("Input is not a valid integer");

Either<string, int> EnsurePositive(int value)
    => value > 0
        ? Either.Right<string, int>(value)
        : Either.Left<string, int>("Value must be positive");

var result = Parse("10")
    .Bind(EnsurePositive)
    .Map(value => value * 2);
```

If `Parse` fails, `EnsurePositive` is never called.

That short-circuiting behavior is the key semantic guarantee.

## Validation example

```csharp
Either<string, string> ValidateName(string? name)
{
    if (string.IsNullOrWhiteSpace(name))
        return Either.Left<string, string>("Name is required");

    if (name.Length > 100)
        return Either.Left<string, string>("Name is too long");

    return Either.Right<string, string>(name.Trim());
}
```

Usage:

```csharp
var response = ValidateName(input).Match(
    onLeft: error => $"Validation failed: {error}",
    onRight: validName => $"Accepted: {validName}"
);
```

## Recovery helpers

`GetOrElse` and `OrElse` let you provide fallbacks without losing the explicit branch model.

```csharp
var fallbackValue = Either.Left<string, int>("missing")
    .GetOrElse(error => error.Length);

var recovered = Either.Left<string, int>("missing")
    .OrElse(error => Either.Right<string, int>(error.Length));
```

The delegate-based overloads are usually the better choice when the fallback depends on the left value or is expensive to compute.

## Side effects with `Tap`

`Tap` and `TapLeft` are useful when you need logging, metrics, or tracing without changing the value.

```csharp
var result = Either.Right<string, int>(42)
    .Tap(value => Console.WriteLine($"Success: {value}"))
    .TapLeft(error => Console.WriteLine($"Failure: {error}"));
```

These methods are observational only and return the original `Either`.

## Conversions

### `Maybe<T>` to `Either<TLeft, TRight>`

```csharp
Maybe<int> maybe = 42;

var either = maybe.ToEither(() => "No value");
```

### `Either<TLeft, TRight>` to `Maybe<TRight>`

```csharp
var maybe = Either.Right<string, int>(42).ToMaybe();
```

### `Either<TLeft, TRight>` to `Result<TRight>`

```csharp
var result = Either.Left<string, int>("Customer not found")
    .ToResult(error => Message.Create(error, MessageType.NotFound));
```

This conversion is intentionally one-way in v1. `Result<T>` can represent success without a value, which does not map cleanly back into `Either`.

## When to use `Either` vs exceptions

Use `Either<TLeft, TRight>` when:

- failure is expected and recoverable
- the failure value is part of the domain or application flow
- you want success and failure visible in the method signature
- you want to compose several recoverable steps with `Bind`

Prefer exceptions when:

- the failure is not part of the normal contract
- the problem is a programmer bug
- the system is in an invalid or unrecoverable state

## When to use `Either`, `Maybe`, or `Result`

Use `Maybe<T>` when:

- the main question is whether a value exists
- absence is normal
- you do not need failure details

Use `Either<TLeft, TRight>` when:

- you need an explicit success/failure split
- the failure value has a meaningful type
- you want right-biased functional composition

Use `Result` or `Result<T>` when:

- you want structured message collections
- you need application or HTTP-oriented error categories
- warnings, codes, and sources matter as part of the outcome

## Summary

`Either<TLeft, TRight>` helps make recoverable failure explicit.

Its main practical advantages are:

- clear success/failure contracts
- right-biased composition with `Map` and `Bind`
- predictable short-circuiting on failure
- explicit handling through `Match`
