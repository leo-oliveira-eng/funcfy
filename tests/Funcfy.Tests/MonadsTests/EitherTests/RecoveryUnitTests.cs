using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.EitherTests;

public class RecoveryUnitTests
{
    [Fact]
    public void GetOrElse_WithValueFallback_WhenLeft_ShouldReturnFallback()
    {
        // Arrange
        var either = Either.Left<string, int>("missing");

        // Act
        var value = either.GetOrElse(99);

        // Assert
        value.ShouldBe(99);
    }

    [Fact]
    public void GetOrElse_WithDelegate_WhenLeft_ShouldUseLeftValue()
    {
        // Arrange
        var either = Either.Left<string, int>("missing");

        // Act
        var value = either.GetOrElse(left => left.Length);

        // Assert
        value.ShouldBe(7);
    }

    [Fact]
    public void GetOrElse_WhenRight_ShouldReturnRightValue()
    {
        // Arrange
        var either = Either.Right<string, int>(42);

        // Act
        var value = either.GetOrElse(99);

        // Assert
        value.ShouldBe(42);
    }

    [Fact]
    public void GetOrElse_WithDelegate_WhenRight_ShouldReturnRightValueWithoutInvokingFallback()
    {
        // Arrange
        var either = Either.Right<string, int>(42);
        var fallbackCallCount = 0;

        // Act
        var value = either.GetOrElse(left =>
        {
            fallbackCallCount++;
            return left.Length;
        });

        // Assert
        fallbackCallCount.ShouldBe(0);
        value.ShouldBe(42);
    }

    [Fact]
    public void OrElse_WithLeftDependentFallback_WhenLeft_ShouldReturnFallback()
    {
        // Arrange
        var either = Either.Left<string, int>("missing");

        // Act
        var recovered = either.OrElse(left => Either.Right<string, int>(left.Length));

        // Assert
        recovered.IsRight.ShouldBeTrue();
        recovered.Match(left => left.Length, right => right).ShouldBe(7);
    }

    [Fact]
    public void OrElse_WithLeftDependentFallback_WhenFallbackReturnsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Left<string, int>("missing");

        // Act & Assert
        Should.Throw<InvalidOperationException>(() => either.OrElse(_ => null!));
    }

    [Fact]
    public void OrElse_WithParameterlessFallback_WhenLeft_ShouldReturnFallback()
    {
        // Arrange
        var either = Either.Left<string, int>("missing");

        // Act
        var recovered = either.OrElse(() => Either.Right<string, int>(99));

        // Assert
        recovered.IsRight.ShouldBeTrue();
        recovered.Match(left => left.Length, right => right).ShouldBe(99);
    }

    [Fact]
    public void OrElse_WithParameterlessFallback_WhenFallbackReturnsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Left<string, int>("missing");

        // Act & Assert
        Should.Throw<InvalidOperationException>(() => either.OrElse(() => null!));
    }

    [Fact]
    public void OrElse_WhenRight_ShouldReturnSameInstance()
    {
        // Arrange
        var either = Either.Right<string, int>(42);

        // Act
        var recovered = either.OrElse(() => Either.Right<string, int>(99));

        // Assert
        ReferenceEquals(either, recovered).ShouldBeTrue();
        recovered.Match(left => left.Length, right => right).ShouldBe(42);
    }

    [Fact]
    public void OrElse_WhenRight_ShouldBeLazy()
    {
        // Arrange
        var either = Either.Right<string, int>(42);
        var fallbackCallCount = 0;

        // Act
        var recovered = either.OrElse(() =>
        {
            fallbackCallCount++;
            return Either.Right<string, int>(99);
        });

        // Assert
        fallbackCallCount.ShouldBe(0);
        recovered.Match(left => left.Length, right => right).ShouldBe(42);
    }

    [Fact]
    public void OrElse_WithLeftDependentFallback_WhenRight_ShouldBeLazy()
    {
        // Arrange
        var either = Either.Right<string, int>(42);
        var fallbackCallCount = 0;

        // Act
        var recovered = either.OrElse(left =>
        {
            fallbackCallCount++;
            return Either.Right<string, int>(left.Length);
        });

        // Assert
        fallbackCallCount.ShouldBe(0);
        recovered.Match(left => left.Length, right => right).ShouldBe(42);
    }
}
