using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.EitherTests;

public class TapUnitTests
{
    [Fact]
    public void Tap_WhenRight_ShouldExecuteActionAndReturnSameInstance()
    {
        // Arrange
        var either = Either.Right<string, int>(21);
        var observed = 0;

        // Act
        var tapped = either.Tap(value => observed = value * 2);

        // Assert
        observed.ShouldBe(42);
        ReferenceEquals(either, tapped).ShouldBeTrue();
        tapped.Match(left => left.Length, right => right).ShouldBe(21);
    }

    [Fact]
    public void Tap_WhenLeft_ShouldNotExecuteAction()
    {
        // Arrange
        var either = Either.Left<string, int>("failure");
        var wasExecuted = false;

        // Act
        var tapped = either.Tap(_ => wasExecuted = true);

        // Assert
        wasExecuted.ShouldBeFalse();
        ReferenceEquals(either, tapped).ShouldBeTrue();
        tapped.Match(left => left, right => right.ToString()).ShouldBe("failure");
    }

    [Fact]
    public void TapLeft_WhenLeft_ShouldExecuteActionAndReturnSameInstance()
    {
        // Arrange
        var either = Either.Left<string, int>("failure");
        string? observed = null;

        // Act
        var tapped = either.TapLeft(left => observed = left.ToUpperInvariant());

        // Assert
        observed.ShouldBe("FAILURE");
        ReferenceEquals(either, tapped).ShouldBeTrue();
        tapped.Match(left => left, right => right.ToString()).ShouldBe("failure");
    }

    [Fact]
    public void TapLeft_WhenRight_ShouldNotExecuteAction()
    {
        // Arrange
        var either = Either.Right<string, int>(42);
        var wasExecuted = false;

        // Act
        var tapped = either.TapLeft(_ => wasExecuted = true);

        // Assert
        wasExecuted.ShouldBeFalse();
        ReferenceEquals(either, tapped).ShouldBeTrue();
        tapped.Match(left => left.Length, right => right).ShouldBe(42);
    }
}
