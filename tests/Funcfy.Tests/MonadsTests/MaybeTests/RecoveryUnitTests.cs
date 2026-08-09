using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.MaybeTests;

public class RecoveryUnitTests
{
    [Fact]
    public void GetOrElse_WithValueFallback_WhenEmpty_ShouldReturnFallback()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();

        // Act
        var value = maybe.GetOrElse(99);

        // Assert
        value.ShouldBe(99);
    }

    [Fact]
    public void GetOrElse_WithDelegate_WhenEmpty_ShouldUseFallback()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();

        // Act
        var value = maybe.GetOrElse(() => 99);

        // Assert
        value.ShouldBe(99);
    }

    [Fact]
    public void GetOrElse_WhenFull_ShouldReturnWrappedValue()
    {
        // Arrange
        Maybe<int> maybe = 42;

        // Act
        var value = maybe.GetOrElse(99);

        // Assert
        value.ShouldBe(42);
    }

    [Fact]
    public void GetOrElse_WithDelegate_WhenFull_ShouldReturnWrappedValueWithoutInvokingFallback()
    {
        // Arrange
        Maybe<int> maybe = 42;
        var fallbackCallCount = 0;

        // Act
        var value = maybe.GetOrElse(() =>
        {
            fallbackCallCount++;
            return 99;
        });

        // Assert
        fallbackCallCount.ShouldBe(0);
        value.ShouldBe(42);
    }

    [Fact]
    public void OrElse_WhenEmpty_ShouldReturnFallback()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();

        // Act
        var recovered = maybe.OrElse(() => Maybe<int>.Full(99));

        // Assert
        recovered.IsFull.ShouldBeTrue();
        recovered.Value.ShouldBe(99);
    }

    [Fact]
    public void OrElse_WhenFallbackReturnsNull_ShouldThrow()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();

        // Act & Assert
        Should.Throw<InvalidOperationException>(() => maybe.OrElse(() => null!));
    }

    [Fact]
    public void OrElse_WhenFull_ShouldReturnSameInstance()
    {
        // Arrange
        Maybe<int> maybe = 42;

        // Act
        var recovered = maybe.OrElse(() => Maybe<int>.Full(99));

        // Assert
        ReferenceEquals(maybe, recovered).ShouldBeTrue();
        recovered.Value.ShouldBe(42);
    }

    [Fact]
    public void OrElse_WhenFull_ShouldBeLazy()
    {
        // Arrange
        Maybe<int> maybe = 42;
        var fallbackCallCount = 0;

        // Act
        var recovered = maybe.OrElse(() =>
        {
            fallbackCallCount++;
            return Maybe<int>.Full(99);
        });

        // Assert
        fallbackCallCount.ShouldBe(0);
        recovered.Value.ShouldBe(42);
    }
}
