using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.ResultExtensionsTests;

public class WithForbiddenUnitTests
{
    [Fact]
    public void WithForbidden_WhenCalledWithValidResult_AddForbiddenErrorMessage()
    {
        // Arrange
        var result = Result<string>.Create();
        var messageContent = "Test message";

        // Act
        result.WithForbidden(messageContent);

        // Assert
        result.Messages.ShouldContain(m => m.Content == messageContent && m.Type == MessageType.Forbidden);
    }
}
