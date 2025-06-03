using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.EmptyResultExtensionsTests;

public class WithWarningUnitTests
{
    [Fact]
    public void WithWarning_WhenCalled_ShouldReturnResultWithWarningMessage()
    {
        // Arrange
        var result = Result.Create();
        var warningMessage = "This is a warning message";

        // Act
        var updatedResult = result.WithWarning(warningMessage);

        // Assert
        updatedResult.ShouldNotBeNull();
        updatedResult.Messages.ShouldNotBeEmpty();
        updatedResult.Messages[0].Content.ShouldBe(warningMessage);
        updatedResult.Messages[0].Type.ShouldBe(MessageType.Warning);
    }
}
