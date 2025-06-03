using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.EmptyResultExtensionsTests;

public class WithNotFoundUnitTests
{
    [Fact]
    public void WithNotFound_WhenCalled_ShouldReturnResultWithNotFoundMessage()
    {
        // Arrange
        var result = Result.Create();
        var notFoundMessage = "This resource was not found";

        // Act
        var updatedResult = result.WithNotFound(notFoundMessage);

        // Assert
        updatedResult.ShouldNotBeNull();
        updatedResult.Messages.ShouldNotBeEmpty();
        updatedResult.Messages[0].Content.ShouldBe(notFoundMessage);
        updatedResult.Messages[0].Type.ShouldBe(MessageType.NotFound);
    }
}
