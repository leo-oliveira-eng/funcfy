using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.ResultExtensionsTests;

public class MergeMessagesFromUnitTests
{
    [Fact]
    public void MergeMessagesFrom_WhenCalledWithNullOrigin_ThrowsArgumentNullException()
    {
        // Arrange
        var target = Result<int>.Create();
        Result? origin = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => target.MergeMessagesFrom(origin!));
    }

    [Fact]
    public void MergeMessagesFrom_WhenCalledWithNullTarget_ThrowsArgumentNullException()
    {         
        // Arrange
        Result<int>? target = null;
        var origin = Result.Create().AddMessage(Message.Create("Test message"));

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => target!.MergeMessagesFrom(origin));
    }

    [Fact]
    public void MergeMessagesFrom_WhenCalledWithValidOrigin_MergesMessages()
    {
        // Arrange
        var target = Result<int>.Create();
        var origin = Result.Create().AddMessage(Message.Create("Test message"));

        // Act
        var result = target.MergeMessagesFrom(origin);

        // Assert
        result.Messages.ShouldContain(m => m.Content.Equals("Test message") && m.Type.Equals(MessageType.BusinessError));
    }
}
