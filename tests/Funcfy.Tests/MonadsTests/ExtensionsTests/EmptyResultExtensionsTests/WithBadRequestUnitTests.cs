using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.EmptyResultExtensionsTests;

public class WithBadRequestUnitTests
{
    [Fact]
    public void WithBadRequest_WhenCalled_ShouldReturnResultWithBadRequestMessage()
    {
        // Arrange
        var result = Result.Create();
        var badRequestMessage = "This is a bad request message";

        // Act
        var updatedResult = result.WithBadRequest(badRequestMessage);

        // Assert
        updatedResult.ShouldNotBeNull();
        updatedResult.Messages.ShouldNotBeEmpty();
        updatedResult.Messages[0].Content.ShouldBe(badRequestMessage);
        updatedResult.Messages[0].Type.ShouldBe(MessageType.BadRequest);
    }
}
