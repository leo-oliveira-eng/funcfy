# ASP.NET Core Integration

## Why this matters

A lot of application code eventually needs to translate domain or service outcomes into HTTP responses.

Without a result abstraction, controllers often become full of repetitive branching:

- if it failed, return `BadRequest`
- if it was unauthorized, return `Unauthorized`
- if it was not found, return `NotFound`
- otherwise return `Ok`

That logic is necessary, but it can quickly take over the controller and make the real intent harder to see.

`funcfy` helps by letting you carry outcome information through the service layer and convert it near the HTTP boundary.

## The theory, kept practical

A controller is an adapter.

Its job is usually not to invent business outcome logic, but to translate application outcomes into HTTP responses.

When your service returns a structured `Result`, the controller can stay thin:

- service decides the outcome
- controller translates the outcome to HTTP

That separation gives you:

- cleaner controllers
- more reusable service code
- more consistent HTTP mapping

## What `funcfy` provides today

`EmptyResultExtensions` includes:

- `ToActionResult()`
- `ToErrorActionResult()`

These are currently defined for the non-generic `Result` type.

## How HTTP mapping works

`ToErrorActionResult()` maps the first error message type to an ASP.NET Core result.

Current mappings include:

- `BusinessError` → `422 Unprocessable Entity`
- `BadRequest` → `400 Bad Request`
- `Unauthorized` → `401 Unauthorized`
- `Forbidden` → `403 Forbidden`
- `NotFound` → `404 Not Found`
- `Conflict` → `409 Conflict`
- `ServerError` → `500 Internal Server Error`
- `ServiceUnavailable` → `503 Service Unavailable`

`ToActionResult()` behaves like this:

- failed result → `ToErrorActionResult()`
- successful result with messages → `200 OK`
- successful result with no messages → `204 No Content`

## Example: controller without helpers

```csharp
[HttpPost]
public async Task<IActionResult> Create(CreateCustomerCommand command)
{
    var result = await _service.CreateAsync(command);

    if (result.Failed)
    {
        var type = result.Messages[0].Type;

        if (type == MessageType.BadRequest)
            return BadRequest(result);

        if (type == MessageType.NotFound)
            return NotFound(result);

        return StatusCode(500, result);
    }

    if (result.Messages.Any())
        return Ok(result);

    return NoContent();
}
```

This works, but the controller now owns a lot of translation noise.

## Example: controller with `ToActionResult()`

```csharp
[HttpPost]
public async Task<IActionResult> Create(CreateCustomerCommand command)
{
    var result = await _service.CreateAsync(command);
    return result.ToActionResult();
}
```

That is a lot easier to scan.

## Advantages in practice

### 1. Thin controllers

Controllers can focus on:

- receiving input
- calling the application layer
- returning translated output

instead of re-implementing response mapping everywhere.

### 2. Consistent error handling

If the team uses result message types consistently, response translation becomes standardized.

That helps avoid situations where one controller returns `400` and another returns `422` for the same kind of business rule failure.

### 3. Better service/controller separation

The service can stay HTTP-agnostic:

```csharp
public async Task<Result> CreateAsync(CreateCustomerCommand command)
{
    if (string.IsNullOrWhiteSpace(command.Name))
        return Result.Failure(Message.Create("Name is required", MessageType.BadRequest));

    // business logic here
    return Result.Success().WithInformation("Customer created");
}
```

The controller becomes a thin translation layer:

```csharp
[HttpPost]
public async Task<IActionResult> Create(CreateCustomerCommand command)
{
    var result = await _service.CreateAsync(command);
    return result.ToActionResult();
}
```

## Example: richer outcomes from the service layer

```csharp
public async Task<Result> DisableUserAsync(Guid id)
{
    var user = await _repository.FindByIdAsync(id);

    if (user.IsEmpty)
        return Result.Failure(Message.Create("User not found", MessageType.NotFound));

    if (!await _authorization.CanDisableAsync(id))
        return Result.Failure(Message.Create("Operation not allowed", MessageType.Forbidden));

    await _repository.DisableAsync(id);

    return Result.Success()
        .WithInformation("User disabled");
}
```

Controller:

```csharp
[HttpPost("{id:guid}/disable")]
public async Task<IActionResult> Disable(Guid id)
{
    var result = await _service.DisableUserAsync(id);
    return result.ToActionResult();
}
```

This keeps the important rules in the application layer while still producing useful HTTP responses.

## Current limitation

The built-in action-result helpers currently focus on non-generic `Result`.

That still gives you a strong pattern:

- use `Result<T>` in services where data matters
- translate into a non-generic `Result` when the HTTP response is command-oriented
- or manually shape the HTTP response at the controller boundary when you need payload-specific output

The roadmap already points toward richer HTTP integration for generic results.

## Recommended usage pattern

For ASP.NET Core applications, a practical pattern is:

1. use `Maybe<T>` for optional lookups
2. use `Result` or `Result<T>` in application services
3. keep HTTP translation at the controller edge
4. standardize `MessageType` usage across the application

That gives you the benefits of functional-style modeling without making the controller layer harder to work with.

## Summary

The ASP.NET Core integration is useful because it turns structured application outcomes into consistent HTTP responses with very little controller code.

Its main practical advantages are:

- thinner controllers
- more explicit service outcomes
- more consistent response mapping
- better separation between business logic and HTTP translation
