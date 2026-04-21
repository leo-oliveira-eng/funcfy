using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.MaybeTests;

public class BindUnitTests
{
    [Fact]
    public void Bind_WhenFull_ShouldChainComputations()
    {
        // Arrange
        Maybe<int> maybe = 10;

        // Act
        var bound = maybe.Bind(value => Maybe<int>.Full(value + 5));

        // Assert
        bound.IsFull.ShouldBeTrue();
        bound.Value.ShouldBe(15);
    }

    [Fact]
    public void Bind_WhenFullAndBinderReturnsEmpty_ShouldReturnEmpty()
    {
        // Arrange
        Maybe<int> maybe = 10;

        // Act
        var bound = maybe.Bind(_ => Maybe<int>.Empty());

        // Assert
        bound.IsEmpty.ShouldBeTrue();
        bound.Value.ShouldBe(0);
    }

    [Fact]
    public void Bind_WhenEmpty_ShouldShortCircuit()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();
        var binderCallCount = 0;

        // Act
        var bound = maybe.Bind(value =>
        {
            binderCallCount++;
            return Maybe<int>.Full(value * 2);
        });

        // Assert
        binderCallCount.ShouldBe(0);
        bound.IsEmpty.ShouldBeTrue();
        bound.Value.ShouldBe(0);
    }

    [Fact]
    public void Bind_WhenBinderReturnsNull_ShouldThrow()
    {
        // Arrange
        Maybe<int> maybe = 10;

        // Act & Assert
        Should.Throw<InvalidOperationException>(() => maybe.Bind<int>(_ => null!));
    }
}
