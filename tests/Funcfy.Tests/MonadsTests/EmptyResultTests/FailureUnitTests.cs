using Funcfy.Monads;
using Funcfy.Monads.Enums;

namespace Funcfy.Tests.MonadsTests.EmptyResultTests;

public class FailureUnitTests
{
    [Fact]
    public void Failure_WhenCalled_ShouldReturnResultWithErrorMessage()
    {
        // Arrange
        var errorMessage = "An error occurred";
        var message = Message.Create(errorMessage, MessageType.BusinessError);

        // Act
        var result = Result.Failure(message);

        // Assert
        result.Failed.ShouldBeTrue();
        result.Messages.ShouldContain(m => m.Content == errorMessage && m.Type == MessageType.BusinessError);
    }
}
