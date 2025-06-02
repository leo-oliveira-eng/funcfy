using Funcfy.Monads;
using Funcfy.Monads.Enums;

namespace Funcfy.Tests.MonadsTests.ResultTests;

public class FailureUnitTests
{
    [Fact]
    public void Failure_WithMessage_WhenCalled_ShouldReturnResultWithMessage()
    {
        // Arrange
        var message = Message.Create("An error occurred", MessageType.BusinessError);

        // Act
        var result = Result<int>.Failure(message);

        // Assert
        result.ShouldNotBeNull();
        result.Data.IsFull.ShouldBeFalse();
        result.Messages.ShouldNotBeEmpty();
        result.Messages[0].Content.ShouldBe(message.Content);
    }

    [Fact]
    public void Failure_WithContent_WhenCalled_ShouldReturnResultWithMessage()
    {
        // Arrange
        var content = "An error occurred";
        var type = MessageType.BusinessError;
        var code = "ERR001";
        var source = "UnitTests";

        // Act
        var result = Result<int>.Failure(content, type, code, source);

        // Assert
        result.ShouldNotBeNull();
        result.Data.IsFull.ShouldBeFalse();
        result.Messages.ShouldNotBeEmpty();
        result.Messages[0].Content.ShouldBe(content);
        result.Messages[0].Type.ShouldBe(type);
        result.Messages[0].Code.ShouldBe(code);
        result.Messages[0].Source.ShouldBe(source);
    }
}
