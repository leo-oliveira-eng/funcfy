using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.ResultExtensionsTests;

public class WithNotFoundUnitTests
{
    [Fact]
    public void WithNotFound_WhenCalledWithValidResult_AddNotFoundErrorMessage()
    {
        // Arrange
        var result = Result<string>.Create();
        var messageContent = "Test message";

        // Act
        result.WithNotFound(messageContent);

        // Assert
        result.Messages.ShouldContain(m => m.Content == messageContent && m.Type == MessageType.NotFound);
    }
}
