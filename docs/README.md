# funcfy Documentation

## Overview

`funcfy` is a small .NET library focused on practical functional-style primitives for application code.

Today the project is centered on:

- `Maybe<T>` for optional values
- `Either<TLeft, TRight>` for recoverable success/failure composition
- `Result` and `Result<T>` for operation outcomes
- `Message` and `MessageType` for structured success, warning, and error information
- extension helpers for building and combining results
- ASP.NET Core integration through `IActionResult` conversion helpers

## Guides

- [Maybe](./maybe.md)
- [Either](./either.md)
- [Result](./result.md)
- [ASP.NET Core](./aspnetcore.md)
- [Roadmap](./roadmap.md)
- [Changelog](../CHANGELOG.md)

If you are looking for concrete usage patterns first, start with:

- [Maybe service/controller examples](./maybe.md#example-service-layer)
- [Either validation and chaining examples](./either.md#validation-example)
- [Result service examples and extension helpers](./result.md#example-result-extension-helpers-in-a-service)
- [ASP.NET Core controller examples](./aspnetcore.md#example-controller-with-toactionresult)

## Current API Surface

### `Maybe<T>`

Use `Maybe<T>` when a value may or may not exist.

Main capabilities:

- `Create`, `Empty`, and `Full`
- `IsEmpty`, `IsFull`, and `Value`
- implicit conversion from `T` to `Maybe<T>`
- `Match` for branching without manual null-like checks
- `Map` and `Bind` for fluent optional composition
- `GetOrElse` and `OrElse` for fallback handling
- `Tap` for side effects on the full path

Unlike `Either<TLeft, TRight>`, `Maybe<T>` has no left payload, so helpers such as `MapLeft` and `TapLeft` are intentionally not part of this API.

Example:

```csharp
Maybe<int> maybe = 42;

var text = maybe
    .Map(value => value * 2)
    .GetOrElse(0)
    .ToString();
```

### `Result`

Use `Result` when an operation has no return value but still needs to communicate success, warnings, or failure.

Main capabilities:

- `Create`, `Success`, and `Failure`
- `Messages`, `Failed`, and `IsSuccessful`
- `Match` for success/failure branching
- fluent message helpers such as `WithWarning`, `WithBadRequest`, and `WithServerError`
- `ToActionResult` and `ToErrorActionResult`

Example:

```csharp
var result = Result.Success()
    .WithInformation("User created");

var status = result.Match(
    onSuccess: () => "ok",
    onFailure: messages => $"failed: {messages[0].Content}"
);
```

### `Either<TLeft, TRight>`

Use `Either<TLeft, TRight>` when an operation can fail in a recoverable, typed way and you want right-biased composition.

Main capabilities:

- `Either.Left` and `Either.Right`
- `IsLeft` and `IsRight`
- `Match`
- `Map`, `MapLeft`, and `Bind`
- `GetOrElse` and `OrElse`
- `Tap` and `TapLeft`
- conversion helpers to `Maybe<T>` and `Result<T>`

Example:

```csharp
var either = Either.Right<string, int>(21)
    .Map(value => value * 2);

var text = either.Match(
    onLeft: error => $"Error: {error}",
    onRight: value => $"Value: {value}"
);
```

### `Result<T>`

Use `Result<T>` when an operation may return a value and may also carry messages.

Main capabilities:

- `Create`, `Success`, `Failure`, and `SetValue`
- `Data` as `Maybe<T>`
- success/failure `Match` where the success branch receives `Maybe<T>`
- fluent typed message helpers inherited through result extensions

Example:

```csharp
var result = Result<string>.Success("funcfy");

var output = result.Match(
    onSuccess: maybe => maybe.Match(
        onFull: value => value.ToUpperInvariant(),
        onEmpty: () => "EMPTY"
    ),
    onFailure: messages => messages[0].Content
);
```


## Roadmap

The roadmap is maintained separately here:

- [Roadmap](./roadmap.md)
