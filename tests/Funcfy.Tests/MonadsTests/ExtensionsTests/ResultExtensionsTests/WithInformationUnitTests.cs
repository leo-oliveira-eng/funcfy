using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.ResultExtensionsTests;

public class WithInformationUnitTests
{
    [Fact]
    public void WithInformation_WhenCalledWithValidResult_AddsInformationMessage()
    {
        // Arrange
        var result = Result<string>.Create();
        var messageContent = "Test information message";

        // Act
        var updatedResult = result.WithInformation(messageContent);

        // Assert
        updatedResult.Messages.ShouldContain(m => m.Content == messageContent && m.Type == MessageType.Info);
    }
}
