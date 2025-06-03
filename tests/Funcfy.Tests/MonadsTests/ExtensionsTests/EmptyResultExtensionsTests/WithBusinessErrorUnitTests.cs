using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.EmptyResultExtensionsTests;

public class WithBusinessErrorUnitTests
{
    [Fact]
    public void WithBusinessError_WhenCalled_ShouldReturnResultWithBusinessErrorMessage()
    {
        // Arrange
        var result = Result.Create();
        var errorMessage = "This is a business error message";

        // Act
        var updatedResult = result.WithBusinessError(errorMessage);

        // Assert
        updatedResult.ShouldNotBeNull();
        updatedResult.Messages.ShouldNotBeEmpty();
        updatedResult.Messages[0].Content.ShouldBe(errorMessage);
        updatedResult.Messages[0].Type.ShouldBe(MessageType.BusinessError);
    }
}
