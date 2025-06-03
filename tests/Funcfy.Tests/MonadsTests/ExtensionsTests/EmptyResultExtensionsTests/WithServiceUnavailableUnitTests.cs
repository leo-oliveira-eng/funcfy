using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.EmptyResultExtensionsTests;

public class WithServiceUnavailableUnitTests
{
    [Fact]
    public void WithServiceUnavailable_WhenCalled_ShouldReturnResultWithServiceUnavailableMessage()
    {
        // Arrange
        var result = Result.Create();
        var serviceUnavailableMessage = "This is a service unavailable message";

        // Act
        var updatedResult = result.WithServiceUnavailable(serviceUnavailableMessage);

        // Assert
        updatedResult.ShouldNotBeNull();
        updatedResult.Messages.ShouldNotBeEmpty();
        updatedResult.Messages[0].Content.ShouldBe(serviceUnavailableMessage);
        updatedResult.Messages[0].Type.ShouldBe(MessageType.ServiceUnavailable);
    }
}
