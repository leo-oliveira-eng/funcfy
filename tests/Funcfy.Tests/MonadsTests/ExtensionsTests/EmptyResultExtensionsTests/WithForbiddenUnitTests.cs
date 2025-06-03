using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.EmptyResultExtensionsTests;

public class WithForbiddenUnitTests
{
    [Fact]
    public void WithForbidden_WhenCalled_ShouldReturnResultWithForbiddenMessage()
    {
        // Arrange
        var result = Result.Create();
        var forbiddenMessage = "This is a forbidden message";

        // Act
        var updatedResult = result.WithForbidden(forbiddenMessage);

        // Assert
        updatedResult.ShouldNotBeNull();
        updatedResult.Messages.ShouldNotBeEmpty();
        updatedResult.Messages[0].Content.ShouldBe(forbiddenMessage);
        updatedResult.Messages[0].Type.ShouldBe(MessageType.Forbidden);
    }
}
