using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.ResultExtensionsTests;

public class WithServiceUnavailable
{
    [Fact]
    public void WithServiceUnavailable_WhenCalledWithValidResult_AddServiceUnavailableErrorMessage()
    {
        // Arrange
        var result = Result<string>.Create();
        var messageContent = "Test message";

        // Act
        result.WithServiceUnavailable(messageContent);

        // Assert
        result.Messages.ShouldContain(m => m.Content == messageContent && m.Type == MessageType.ServiceUnavailable);
    }
}
