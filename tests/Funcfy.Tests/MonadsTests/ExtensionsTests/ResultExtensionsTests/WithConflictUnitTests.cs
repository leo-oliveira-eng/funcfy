using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.ResultExtensionsTests;

public class WithConflictUnitTests
{
    [Fact]
    public void WithConflict_WhenCalledWithValidResult_AddConflictErrorMessage()
    {
        // Arrange
        var result = Result<string>.Create();
        var messageContent = "Test message";

        // Act
        result.WithConflict(messageContent);

        // Assert
        result.Messages.ShouldContain(m => m.Content == messageContent && m.Type == MessageType.Conflict);
    }
}
