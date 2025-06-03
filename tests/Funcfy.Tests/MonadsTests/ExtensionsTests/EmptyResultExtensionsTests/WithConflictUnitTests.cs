using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.EmptyResultExtensionsTests;

public class WithConflictUnitTests
{
    [Fact]
    public void WithConflict_WhenCalled_ShouldReturnResultWithConflictMessage()
    {
        // Arrange
        var result = Result.Create();
        var conflictMessage = "This is a conflict message";

        // Act
        var updatedResult = result.WithConflict(conflictMessage);

        // Assert
        updatedResult.ShouldNotBeNull();
        updatedResult.Messages.ShouldNotBeEmpty();
        updatedResult.Messages[0].Content.ShouldBe(conflictMessage);
        updatedResult.Messages[0].Type.ShouldBe(MessageType.Conflict);
    }
}
