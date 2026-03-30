using Funcfy.Monads;
using Funcfy.Monads.Enums;

namespace Funcfy.Tests.MonadsTests.ResultTests;

public class MatchUnitTests
{
    [Fact]
    public void Match_WhenSuccessfulWithValue_ShouldReturnSuccessResult()
    {
        // Arrange
        var result = Result<int>.Success(42);

        // Act
        var matchResult = result.Match(
            onSuccess: maybe => maybe.Match(value => value * 2, () => 0),
            onFailure: _ => -1
        );

        // Assert
        matchResult.ShouldBe(84);
    }

    [Fact]
    public void Match_WhenSuccessfulWithoutValue_ShouldReturnSuccessResult()
    {
        // Arrange
        var result = Result<int>.Success();

        // Act
        var matchResult = result.Match(
            onSuccess: maybe => maybe.Match(value => value * 2, () => 0),
            onFailure: _ => -1
        );

        // Assert
        matchResult.ShouldBe(0);
    }

    [Fact]
    public void Match_WhenFailure_ShouldReturnFailureResult()
    {
        // Arrange
        var result = Result<int>.Failure("An error occurred", MessageType.BusinessError);

        // Act
        var matchResult = result.Match(
            onSuccess: _ => 0,
            onFailure: messages => messages.Count
        );

        // Assert
        matchResult.ShouldBe(1);
    }

    [Fact]
    public void Match_WhenSuccessfulWithValue_ShouldExecuteSuccessAction()
    {
        // Arrange
        var result = Result<int>.Success(42);
        var actionExecuted = false;

        // Act
        result.Match(
            onSuccess: maybe => actionExecuted = maybe.IsFull,
            onFailure: _ => { }
        );

        // Assert
        actionExecuted.ShouldBeTrue();
    }

    [Fact]
    public void Match_WhenSuccessfulWithoutValue_ShouldExecuteSuccessAction()
    {
        // Arrange
        var result = Result<int>.Success();
        var actionExecuted = false;

        // Act
        result.Match(
            onSuccess: maybe => actionExecuted = maybe.IsEmpty,
            onFailure: _ => { }
        );

        // Assert
        actionExecuted.ShouldBeTrue();
    }

    [Fact]
    public void Match_WhenFailure_ShouldExecuteFailureAction()
    {
        // Arrange
        var result = Result<int>.Failure("An error occurred", MessageType.BusinessError);
        var actionExecuted = false;

        // Act
        result.Match(
            onSuccess: _ => { },
            onFailure: messages => actionExecuted = messages.Count == 1
        );

        // Assert
        actionExecuted.ShouldBeTrue();
    }
}
