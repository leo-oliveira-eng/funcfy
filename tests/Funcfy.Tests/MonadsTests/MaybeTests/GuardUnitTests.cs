using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.MaybeTests;

public class GuardUnitTests
{
    [Fact]
    public void Match_WhenOnFullFuncIsNull_ShouldThrow()
    {
        // Arrange
        Maybe<int> maybe = 42;

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => maybe.Match<string>(null!, () => "empty"));
    }

    [Fact]
    public void Match_WhenOnEmptyFuncIsNull_ShouldThrow()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => maybe.Match(value => value.ToString(), null!));
    }

    [Fact]
    public void Match_ActionOverload_WhenOnFullActionIsNull_ShouldThrow()
    {
        // Arrange
        Maybe<int> maybe = 42;

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => maybe.Match(null!, () => { }));
    }

    [Fact]
    public void Match_ActionOverload_WhenOnEmptyActionIsNull_ShouldThrow()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => maybe.Match(_ => { }, null!));
    }

    [Fact]
    public void Map_WhenMapperIsNull_ShouldThrow()
    {
        // Arrange
        Maybe<int> maybe = 42;

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => maybe.Map<string>(null!));
    }

    [Fact]
    public void Bind_WhenBinderIsNull_ShouldThrow()
    {
        // Arrange
        Maybe<int> maybe = 42;

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => maybe.Bind<int>(null!));
    }

    [Fact]
    public void GetOrElse_WhenDelegateIsNull_ShouldThrow()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => maybe.GetOrElse(null!));
    }

    [Fact]
    public void OrElse_WhenFallbackIsNull_ShouldThrow()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => maybe.OrElse(null!));
    }

    [Fact]
    public void Tap_WhenActionIsNull_ShouldThrow()
    {
        // Arrange
        Maybe<int> maybe = 42;

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => maybe.Tap(null!));
    }
}
