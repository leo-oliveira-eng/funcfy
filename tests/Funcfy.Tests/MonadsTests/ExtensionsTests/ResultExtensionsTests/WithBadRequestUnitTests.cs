using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.ResultExtensionsTests;

public class WithBadRequestUnitTests
{
    [Fact]
    public void WithBadRequest_WhenCalledWithValidResult_AddsBadRequestMessage()
    {
        // Arrange
        var result = Result<string>.Create();
        var messageContent = "Test bad request message";

        // Act
        var updatedResult = result.WithBadRequest(messageContent);

        // Assert
        updatedResult.Messages.ShouldContain(m => m.Content == messageContent && m.Type == MessageType.BadRequest);
    }
}
