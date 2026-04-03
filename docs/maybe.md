# Maybe

## What problem `Maybe<T>` solves

In everyday application code, we often need to represent "a value may exist, or it may not".

The most common ways to model that are:

- `null`
- sentinel values such as `0`, `Guid.Empty`, or `""`
- exceptions for normal absence

Those approaches tend to hide intent. They force readers to remember conventions instead of letting the type system express what is actually happening.

`Maybe<T>` makes absence explicit.

Instead of saying "this method returns a `T`, but sometimes that `T` is not really there", the API says:

- you will receive either a full value
- or an empty value container

That small shift has practical advantages:

- clearer method contracts
- less accidental null handling
- easier branching at the call site
- better readability in services and repositories

## The theory, in small doses

`Maybe<T>` is an "optional value" abstraction.

In functional programming terms, it helps you model computations where a value may be missing, without falling back to unchecked null semantics.

The important practical idea is simple:

- `T` means "a value is expected"
- `Maybe<T>` means "a value may or may not be present"

You do not need deep category theory to benefit from it. The win is mostly about making intent explicit and removing ambiguity from APIs.

## Main API

`Maybe<T>` currently provides:

- `Create()`
- `Create(value)`
- `Empty()`
- `Full(value)`
- `IsEmpty`
- `IsFull`
- `Value`
- implicit conversion from `T` to `Maybe<T>`
- `Match`

## Creating values

### Empty value

```csharp
var maybe = Maybe<int>.Empty();

Console.WriteLine(maybe.IsEmpty); // true
Console.WriteLine(maybe.IsFull);  // false
```

### Full value

```csharp
var maybe = Maybe<string>.Full("funcfy");

Console.WriteLine(maybe.IsEmpty); // false
Console.WriteLine(maybe.IsFull);  // true
Console.WriteLine(maybe.Value);   // funcfy
```

### Implicit conversion

```csharp
Maybe<int> maybe = 42;
```

This is equivalent to:

```csharp
var maybe = Maybe<int>.Create(42);
```

## Branching with `Match`

`Match` is one of the biggest practical advantages of using `Maybe<T>`.

It lets you handle both branches in one place:

```csharp
Maybe<int> maybe = 42;

var text = maybe.Match(
    onFull: value => $"Found: {value}",
    onEmpty: () => "No value"
);
```

That keeps the handling explicit and local.

You can also use the action overload:

```csharp
maybe.Match(
    onFull: value => Console.WriteLine($"Processing {value}"),
    onEmpty: () => Console.WriteLine("Nothing to process")
);
```

## Advantages in practice

### 1. Better repository/service contracts

Without `Maybe<T>`:

```csharp
public Task<User?> FindByIdAsync(Guid id);
```

This works, but the caller must remember that `null` is a normal outcome.

With `Maybe<T>`:

```csharp
public Task<Maybe<User>> FindByIdAsync(Guid id);
```

Now the contract itself tells the truth.

Caller code becomes more intentional:

```csharp
var user = await repository.FindByIdAsync(id);

var response = user.Match(
    onFull: value => $"User found: {value.Name}",
    onEmpty: () => "User not found"
);
```

### 2. Fewer hidden conventions

Sentinel values often age badly:

```csharp
// Does 0 mean "not found" or is 0 a valid value?
int customerLevel = GetCustomerLevel();
```

With `Maybe<int>`, the contract is unambiguous:

```csharp
Maybe<int> customerLevel = GetCustomerLevel();
```

### 3. Cleaner branching than repeated `if` checks

`Match` reduces scattered conditionals:

```csharp
var discount = maybeDiscount.Match(
    onFull: value => value,
    onEmpty: () => 0m
);
```

This reads as a complete decision instead of a half-remembered null check.

## Example: service layer

```csharp
public async Task<Maybe<Order>> FindOrderAsync(Guid id)
{
    var entity = await _dbContext.Orders.FindAsync(id);

    return entity is null
        ? Maybe<Order>.Empty()
        : Maybe<Order>.Full(entity);
}
```

Usage:

```csharp
var order = await service.FindOrderAsync(orderId);

var message = order.Match(
    onFull: value => $"Order total: {value.Total}",
    onEmpty: () => "Order not found"
);
```

## When to use `Maybe<T>` vs `Result<T>`

Use `Maybe<T>` when:

- the main question is whether a value exists
- absence is normal and expected
- you do not need structured failure details

Use `Result<T>` when:

- you need success/failure semantics
- you want warnings or structured messages
- you need application or HTTP-oriented error categories

## Summary

`Maybe<T>` is valuable because it turns implicit absence into explicit intent.

That gives you:

- clearer APIs
- safer calling code
- easier branching
- less reliance on `null` or sentinel conventions
