using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.ResultExtensionsTests;

public class WithBusinessErrorUnitTests
{
    [Fact]
    public void WithBusinessError_WhenCalledWithValidResult_AddsBusinessErrorMessage()
    {
        // Arrange
        var result = Result<string>.Create();
        var messageContent = "Test business error message";

        // Act
        var updatedResult = result.WithBusinessError(messageContent);

        // Assert
        updatedResult.Messages.ShouldContain(m => m.Content == messageContent && m.Type == MessageType.BusinessError);
    }
}
