<p align="center">
      <img  
        alt="funcfy - A lightweight, batteries-included functional programming toolkit for C#/.NET." 
        src="https://github.com/leo-oliveira-eng/funcfy/blob/main/images/logo.png"
        height="400px"
      />
</p>

# funcfy

A lightweight, batteries-included functional programming toolkit for C#/.NET.

## Features

📋 Currying & partial application  
🔄 Either, Maybe, Result types  
📋 Functor, Applicative, Monad interfaces  
📋 Lens utilities, compose, pipe

## Getting Started

Install via NuGet:

```bash
dotnet add package Funcfy --version 0.1.0-alpha
```

Import the core namespace:

```csharp
using Funcfy;
```

Example currying:

```csharp
var add = Func.Curry((int x, int y) => x + y);
var add5 = add(5);
Console.WriteLine(add5(3));  // 8
```

## Using Maybe<T>

The `Maybe<T>` type expresses optional values explicitly, avoiding null-reference surprises. Example:


  ```csharp
    public class RepositoryAsync<Entity> : SpecificMethods<Entity>, IRepositoryAsync<Entity>
    {
    ...

    public async Task<Maybe<Entity>> FindAsync(int id)
        => await DbSet.FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        
    ...
    }
        
  ```

The developer can act as follows when receiving the response:

  ```csharp

    ...

        var entity = await EntityRepository.FindAsync(entityId);

        if (entity.IsEmpty)
            return response.WithError("Not found");

    ...

  ```

### Creating Instances

- **Empty** (no value):

  ```csharp
  var empty = Maybe<int>.Empty();          // IsFull == false, IsEmpty == true
  ```

- **Full** (with a value):

  ```csharp
  var full = Maybe<string>.Full("hello");  // IsFull == true, Value == "hello"
  Maybe<int> also = 42;                      // implicit conversion wraps 42 as Full
  ```

### Inspecting Values

Use `IsFull` or `IsEmpty` before accessing `Value`:

```csharp
if (full.IsFull)
{
    Console.WriteLine(full.Value);
}
else
{
    Console.WriteLine("No value present.");
}
```

### Conversion Operators

Thanks to the implicit operator, you can assign raw values directly:

```csharp
Maybe<double> pi = 3.14;  // equivalent to Maybe<double>.Full(3.14)
```

> ⚠️ **Important:** Accessing `Value` when `IsEmpty` is `true` returns `null` for reference types or the default for value types. Always check `IsFull` first.

## Contributing

1. Fork the repo  
2. Create a feature branch (`git checkout -b feature/foo`)  
3. Commit changes (`git commit -m 'feat: add foo'`)  
4. Push to your branch (`git push origin feature/foo`)  
5. Open a Pull Request to develop branch

---

