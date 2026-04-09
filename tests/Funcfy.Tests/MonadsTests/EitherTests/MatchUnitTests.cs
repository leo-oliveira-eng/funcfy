using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.EitherTests;

public class MatchUnitTests
{
    [Fact]
    public void Match_WhenLeft_ShouldReturnLeftBranchResult()
    {
        // Arrange
        var either = Either.Left<string, int>("missing");

        // Act
        var result = either.Match(
            onLeft: left => left.ToUpperInvariant(),
            onRight: right => right.ToString()
        );

        // Assert
        result.ShouldBe("MISSING");
    }

    [Fact]
    public void Match_WhenRight_ShouldReturnRightBranchResult()
    {
        // Arrange
        var either = Either.Right<string, int>(21);

        // Act
        var result = either.Match(
            onLeft: left => left.Length,
            onRight: right => right * 2
        );

        // Assert
        result.ShouldBe(42);
    }

    [Fact]
    public void Match_ActionOverload_WhenLeft_ShouldExecuteLeftAction()
    {
        // Arrange
        var either = Either.Left<string, int>("failure");
        var wasExecuted = false;

        // Act
        either.Match(
            onLeft: _ => wasExecuted = true,
            onRight: _ => { }
        );

        // Assert
        wasExecuted.ShouldBeTrue();
    }

    [Fact]
    public void Match_ActionOverload_WhenRight_ShouldExecuteRightAction()
    {
        // Arrange
        var either = Either.Right<string, int>(10);
        var wasExecuted = false;

        // Act
        either.Match(
            onLeft: _ => { },
            onRight: _ => wasExecuted = true
        );

        // Assert
        wasExecuted.ShouldBeTrue();
    }
}
