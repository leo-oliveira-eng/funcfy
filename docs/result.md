# Result

## What problem `Result` solves

Many operations need to tell you more than just "a value exists" or "a value is missing".

They may need to communicate:

- success
- failure
- warnings
- validation problems
- authorization problems
- not-found conditions

Returning only a raw value does not express that well.

Throwing exceptions for every expected business outcome also tends to make application code noisy and harder to reason about.

`Result` and `Result<T>` give you a structured way to model operation outcomes.

## The theory, in practical terms

A result type is useful when an operation has two dimensions:

- whether it succeeded
- what information should travel with that outcome

In `funcfy`:

- `Result` models outcome plus messages, without a return value
- `Result<T>` models outcome plus messages, with optional data

This is especially helpful in service-layer code, validation flows, and API-oriented applications, where failure is often expected and meaningful, not exceptional.

## The building blocks

### `Message`

`Message` carries:

- `Content`
- `Type`
- `Code`
- `Source`

### `MessageType`

The library supports informational, warning, and error-oriented categories, including:

- `Info`
- `Warning`
- `BusinessError`
- `BadRequest`
- `Unauthorized`
- `Forbidden`
- `NotFound`
- `Conflict`
- `ServerError`
- `ServiceUnavailable`

That makes result handling much richer than a simple `bool`.

## `Result`

Use non-generic `Result` when an operation does not need to return a payload.

Main API:

- `Create()`
- `Success()`
- `Failure(message)`
- `Messages`
- `Failed`
- `IsSuccessful`
- `Match`

### Example

```csharp
var result = Result.Success()
    .WithInformation("Customer updated");
```

Branching:

```csharp
var status = result.Match(
    onSuccess: () => "ok",
    onFailure: messages => $"failed: {messages[0].Content}"
);
```

### Why this is useful

This style makes application code easier to read than passing booleans and out parameters around.

Instead of:

```csharp
bool ok = UpdateCustomer(customer, out var errorMessage);
```

you can express the full outcome:

```csharp
var result = UpdateCustomer(customer);
```

and inspect it in a meaningful way.

## `Result<T>`

Use `Result<T>` when you need outcome semantics and data.

Main API:

- `Create()`
- `Create(value)`
- `Success()`
- `Success(value)`
- `Failure(...)`
- `Data` as `Maybe<T>`
- `SetValue(value)`
- `Match`

An important design detail:

`Result<T>.Success()` is valid even when `Data` is empty.

That means:

- the operation may succeed
- but still not have a payload

Because of that, the success branch of `Match` receives `Maybe<T>`, not raw `T`.

### Example

```csharp
var result = Result<string>.Success("funcfy");

var output = result.Match(
    onSuccess: maybe => maybe.Match(
        onFull: value => value.ToUpperInvariant(),
        onEmpty: () => "SUCCESS_WITHOUT_VALUE"
    ),
    onFailure: messages => messages[0].Content
);
```

## Advantages in practice

### 1. Explicit business outcomes

A result can express things that a boolean cannot:

```csharp
var result = Result.Failure(
    Message.Create("Customer not found", MessageType.NotFound, code: "CUST_404")
);
```

Now you have:

- the outcome
- the category
- the message text
- an optional code

all in one place.

### 2. Rich validation flows

Results make validation accumulation more natural:

```csharp
var result = Result.Create();

if (string.IsNullOrWhiteSpace(command.Name))
    result.WithBadRequest("Name is required", code: "NAME_REQUIRED");

if (command.Age < 18)
    result.WithBusinessError("Minimum age is 18", code: "AGE_INVALID");
```

That is much easier to extend than throwing multiple exceptions or short-circuiting too early.

### 3. Better service-layer APIs

Instead of returning a nullable payload and relying on conventions:

```csharp
public async Task<Customer?> GetCustomerAsync(Guid id);
```

you can return:

```csharp
public async Task<Result<Customer>> GetCustomerAsync(Guid id);
```

and then represent:

- success with data
- success without data
- not found
- unauthorized
- conflict

without changing the method shape.

## Fluent message helpers

Both `Result` and `Result<T>` support fluent helpers like:

- `WithInformation`
- `WithWarning`
- `WithBusinessError`
- `WithBadRequest`
- `WithUnauthorized`
- `WithForbidden`
- `WithNotFound`
- `WithConflict`
- `WithServerError`
- `WithServiceUnavailable`

Example:

```csharp
var result = Result<string>.Create()
    .WithWarning("Using cached value")
    .SetValue("cached-response");
```

## Example: result extension helpers in a service

The fluent extension helpers are useful when a service needs to accumulate business and validation messages while keeping a single return type:

```csharp
public Result ValidateProfile(UpdateProfileCommand command)
{
    var result = Result.Create();

    if (string.IsNullOrWhiteSpace(command.DisplayName))
        result.WithBadRequest("Display name is required", code: "DISPLAY_NAME_REQUIRED", source: nameof(command.DisplayName));

    if (command.DisplayName?.Length > 100)
        result.WithBusinessError("Display name is too long", code: "DISPLAY_NAME_TOO_LONG", source: nameof(command.DisplayName));

    if (result.IsSuccessful)
        result.WithInformation("Profile validation passed", code: "PROFILE_VALIDATED");

    return result;
}
```

That same pattern works with `Result<T>` when the operation also needs to return data.

## `Match` in practice

`Match` helps keep the handling logic close to the result.

### Functional style

```csharp
var response = result.Match(
    onSuccess: maybe => maybe.Match(
        onFull: value => new { ok = true, value },
        onEmpty: () => new { ok = true, value = (string?)null }
    ),
    onFailure: messages => new { ok = false, error = messages[0].Content }
);
```

### Action style

```csharp
result.Match(
    onSuccess: maybe =>
    {
        // continue pipeline
    },
    onFailure: messages =>
    {
        _logger.LogWarning("Operation failed: {Message}", messages[0].Content);
    }
);
```

## Example: application service

```csharp
public async Task<Result<CustomerDto>> GetCustomerAsync(Guid id)
{
    var customer = await _repository.FindByIdAsync(id);

    return customer.Match(
        onFull: entity => Result<CustomerDto>.Success(new CustomerDto(entity.Id, entity.Name)),
        onEmpty: () => Result<CustomerDto>.Failure(
            Message.Create("Customer not found", MessageType.NotFound, code: "CUSTOMER_NOT_FOUND")
        )
    );
}
```

At the call site:

```csharp
var result = await service.GetCustomerAsync(id);

var text = result.Match(
    onSuccess: maybe => maybe.Match(
        onFull: dto => dto.Name,
        onEmpty: () => "No payload"
    ),
    onFailure: messages => messages[0].Content
);
```

## When to use `Result` vs `Maybe`

Use `Maybe<T>` when:

- you only care whether a value exists
- absence is normal
- you do not need structured failure details

Use `Result` or `Result<T>` when:

- you need explicit success/failure modeling
- you want messages, categories, codes, or sources
- you want easier integration with application and HTTP workflows

## Summary

`Result` helps move application code away from hidden conventions.

Its main practical advantages are:

- richer method contracts
- structured business and HTTP-like outcomes
- easier branching with `Match`
- better composition between services, validation, and controller code
