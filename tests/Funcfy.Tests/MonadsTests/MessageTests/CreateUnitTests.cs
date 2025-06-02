using Funcfy.Monads;
using Funcfy.Monads.Enums;

namespace Funcfy.Tests.MonadsTests.MessageTests;

public class CreateUnitTests
{
    [Fact]
    public void Create_WhenCalledWithValidParameters_ShouldReturnMessage()
    {
        // Arrange
        var content = "Test message";
        var type = MessageType.Info;
        var code = "TEST_CODE";
        var source = "TestSource";
        
        // Act
        var message = Message.Create(content, type, code, source);

        // Assert
        message.ShouldNotBeNull();
        message.Content.ShouldBe(content);
        message.Type.ShouldBe(type);
        message.Code.ShouldBe(code);
        message.Source.ShouldBe(source);
    }

    [Fact]
    public void Create_WhenCalledWithNullContent_ShouldThrowArgumentNullException()
    {
        // Arrange
        string? content = null;
        var type = MessageType.ServiceUnavailable;

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => Message.Create(content!, type));
    }
}
