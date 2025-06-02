using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.MaybeTests;

public class MatchUnitTests
{
    [Fact]
    public void Match_Unit_WhenFull_ShouldReturnUnit()
    {
        // Arrange
        Maybe<int> maybe = 42;

        // Act
        var result = maybe.Match(
            onFull: value => value *= 2,
            onEmpty: () => default
        );

        // Assert
        result.ShouldBe(84);
    }

    [Fact]
    public void Match_Unit_WhenEmpty_ShouldReturnUnit()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();

        // Act
        var result = maybe.Match(
            onFull: value => value *= 2,
            onEmpty: () => 0
        );

        // Assert
        result.ShouldBe(0);
    }

    [Fact]
    public void Match_Unit_WhenFull_ShouldExecuteAction()
    {
        // Arrange
        Maybe<int> maybe = 42;
        bool actionExecuted = false;

        // Act
        maybe.Match(
            onFull: value => { actionExecuted = true; },
            onEmpty: () => { }
        );

        // Assert
        actionExecuted.ShouldBeTrue();
    }

    [Fact]
    public void Match_Unit_WhenEmpty_ShouldExecuteAction()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();
        bool actionExecuted = false;

        // Act
        maybe.Match(
            onFull: value => { },
            onEmpty: () => { actionExecuted = true; }
        );

        // Assert
        actionExecuted.ShouldBeTrue();
    }
}
