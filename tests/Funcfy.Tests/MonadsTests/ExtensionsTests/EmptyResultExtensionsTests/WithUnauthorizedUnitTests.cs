using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.EmptyResultExtensionsTests;

public class WithUnauthorizedUnitTests
{
    [Fact]
    public void WithUnauthorized_WhenCalled_ShouldReturnResultWithUnauthorizedMessage()
    {
        // Arrange
        var result = Result.Create();
        var unauthorizedMessage = "This is an unauthorized message";

        // Act
        var updatedResult = result.WithUnauthorized(unauthorizedMessage);

        // Assert
        updatedResult.ShouldNotBeNull();
        updatedResult.Messages.ShouldNotBeEmpty();
        updatedResult.Messages[0].Content.ShouldBe(unauthorizedMessage);
        updatedResult.Messages[0].Type.ShouldBe(MessageType.Unauthorized);
    }
}
