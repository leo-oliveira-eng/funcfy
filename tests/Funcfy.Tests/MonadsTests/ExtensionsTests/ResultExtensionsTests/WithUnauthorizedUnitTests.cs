using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.ResultExtensionsTests;

public class WithUnauthorizedUnitTests
{
    [Fact]
    public void WithUnauthorized_WhenCalledWithValidResult_AddUnauthorizedErrorMessage()
    {
        // Arrange
        var result = Result<string>.Create();
        var messageContent = "Test message";

        // Act
        result.WithUnauthorized(messageContent);

        // Assert
        result.Messages.ShouldContain(m => m.Content == messageContent && m.Type == MessageType.Unauthorized);
    }
}
