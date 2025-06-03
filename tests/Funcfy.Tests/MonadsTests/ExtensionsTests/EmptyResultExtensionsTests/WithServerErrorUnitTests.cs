using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.EmptyResultExtensionsTests;

public class WithServerErrorUnitTests
{
    [Fact]
    public void WithServerError_WhenCalled_ShouldReturnResultWithServerErrorMessage()
    {
        // Arrange
        var result = Result.Create();
        var serverErrorMessage = "This is a server error message";

        // Act
        var updatedResult = result.WithServerError(serverErrorMessage);

        // Assert
        updatedResult.ShouldNotBeNull();
        updatedResult.Messages.ShouldNotBeEmpty();
        updatedResult.Messages[0].Content.ShouldBe(serverErrorMessage);
        updatedResult.Messages[0].Type.ShouldBe(MessageType.ServerError);
    }
}
