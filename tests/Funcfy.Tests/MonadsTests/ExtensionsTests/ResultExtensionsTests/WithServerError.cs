using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.ResultExtensionsTests;

public class WithServerError
{
    [Fact]
    public void WithServerError_WhenCalledWithValidResult_AddServerErrorMessage()
    {
        // Arrange
        var result = Result<string>.Create();
        var messageContent = "Test server error message";

        // Act
        result.WithServerError(messageContent);

        // Assert
        result.Messages.ShouldContain(m => m.Content == messageContent && m.Type == MessageType.ServerError);
    }
}
