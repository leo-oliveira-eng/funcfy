using Funcfy.Monads;
using Funcfy.Monads.Enums;

namespace Funcfy.Tests.MonadsTests.EmptyResultTests;

public class MatchUnitTests
{
    [Fact]
    public void Match_WhenSuccessful_ShouldReturnSuccessResult()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var matchResult = result.Match(
            onSuccess: () => 1,
            onFailure: _ => 0
        );

        // Assert
        matchResult.ShouldBe(1);
    }

    [Fact]
    public void Match_WhenFailure_ShouldReturnFailureResult()
    {
        // Arrange
        var result = Result.Failure(Message.Create("An error occurred", MessageType.BusinessError));

        // Act
        var matchResult = result.Match(
            onSuccess: () => 0,
            onFailure: messages => messages.Count
        );

        // Assert
        matchResult.ShouldBe(1);
    }

    [Fact]
    public void Match_WhenSuccessful_ShouldExecuteSuccessAction()
    {
        // Arrange
        var result = Result.Success();
        var actionExecuted = false;

        // Act
        result.Match(
            onSuccess: () => actionExecuted = true,
            onFailure: _ => { }
        );

        // Assert
        actionExecuted.ShouldBeTrue();
    }

    [Fact]
    public void Match_WhenFailure_ShouldExecuteFailureAction()
    {
        // Arrange
        var result = Result.Failure(Message.Create("An error occurred", MessageType.BusinessError));
        var actionExecuted = false;

        // Act
        result.Match(
            onSuccess: () => { },
            onFailure: messages => actionExecuted = messages.Any(message => message.Type == MessageType.BusinessError)
        );

        // Assert
        actionExecuted.ShouldBeTrue();
    }
}
