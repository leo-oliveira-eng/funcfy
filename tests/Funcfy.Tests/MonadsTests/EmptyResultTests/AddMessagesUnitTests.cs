using Funcfy.Monads;
using Funcfy.Monads.Enums;

namespace Funcfy.Tests.MonadsTests.EmptyResultTests;

public class AddMessagesUnitTests
{
    [Fact]
    public void AddMessages_WhenCalled_ShouldAddMessagesToResult()
    {
        // Arrange
        var result = Result.Create();
        var message = Message.Create("Message 1", MessageType.Info);

        // Act
        result.AddMessage(message);

        // Assert
        result.Messages.ShouldContain(m => m.Content == "Message 1" && m.Type == MessageType.Info);
    }

    [Fact]
    public void AddMessages_WhenMessageIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var result = Result.Create();

        // Act & Assert
        Should.Throw<ArgumentNullException>(() =>
        {            
            result.AddMessage(null!);
        });
    }
}
