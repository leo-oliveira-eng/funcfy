using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.ResultExtensionsTests;

public class WithWarningUnitTests
{
    [Fact]
    public void WithWarning_WhenCalledWithValidResult_AddsWarningMessage()
    {
        // Arrange
        var result = Result<string>.Create();
        var messageContent = "Test warning message";

        // Act
        var updatedResult = result.WithWarning(messageContent);

        // Assert
        updatedResult.Messages.ShouldContain(m => m.Content == messageContent && m.Type == MessageType.Warning);
    }
}
