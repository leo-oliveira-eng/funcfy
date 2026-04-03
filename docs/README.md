# funcfy Documentation

## Overview

`funcfy` is a small .NET library focused on practical functional-style primitives for application code.

Today the project is centered on:

- `Maybe<T>` for optional values
- `Result` and `Result<T>` for operation outcomes
- `Message` and `MessageType` for structured success, warning, and error information
- extension helpers for building and combining results
- ASP.NET Core integration through `IActionResult` conversion helpers

## Guides

- [Maybe](./maybe.md)
- [Result](./result.md)
- [ASP.NET Core](./aspnetcore.md)

## Current API Surface

### `Maybe<T>`

Use `Maybe<T>` when a value may or may not exist.

Main capabilities:

- `Create`, `Empty`, and `Full`
- `IsEmpty`, `IsFull`, and `Value`
- implicit conversion from `T` to `Maybe<T>`
- `Match` for branching without manual null-like checks

Example:

```csharp
Maybe<int> maybe = 42;

var text = maybe.Match(
    onFull: value => $"Value: {value}",
    onEmpty: () => "No value"
);
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
