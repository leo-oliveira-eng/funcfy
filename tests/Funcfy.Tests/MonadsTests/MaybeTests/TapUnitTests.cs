using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.MaybeTests;

public class TapUnitTests
{
    [Fact]
    public void Tap_WhenFull_ShouldExecuteActionAndReturnSameInstance()
    {
        // Arrange
        Maybe<int> maybe = 21;
        var observed = 0;

        // Act
        var tapped = maybe.Tap(value => observed = value * 2);

        // Assert
        observed.ShouldBe(42);
        ReferenceEquals(maybe, tapped).ShouldBeTrue();
        tapped.Value.ShouldBe(21);
    }

    [Fact]
    public void Tap_WhenEmpty_ShouldNotExecuteAction()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();
        var wasExecuted = false;

        // Act
        var tapped = maybe.Tap(_ => wasExecuted = true);

        // Assert
        wasExecuted.ShouldBeFalse();
        ReferenceEquals(maybe, tapped).ShouldBeTrue();
        tapped.IsEmpty.ShouldBeTrue();
        tapped.Value.ShouldBe(0);
    }
}
