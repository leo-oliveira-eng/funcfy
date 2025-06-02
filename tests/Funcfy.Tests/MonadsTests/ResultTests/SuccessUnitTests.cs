using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.ResultTests;

public class SuccessUnitTests
{
    [Fact]
    public void Success_WhenCalled_ShouldReturnEmptyResult()
    {
        // Arrange & Act
        var result = Result<int>.Success();

        // Assert
        result.ShouldNotBeNull();
        result.Data.IsFull.ShouldBeFalse();
        result.Messages.ShouldBeEmpty();
    }

    [Fact]
    public void Success_WithValue_WhenCalled_ShouldReturnResultWithValue()
    {
        // Arrange
        var value = 1;

        // Act
        var result = Result<int>.Success(value);

        // Assert
        result.ShouldNotBeNull();
        result.Data.IsFull.ShouldBeTrue();
        result.Data.Value.ShouldBe(value);
        result.Messages.ShouldBeEmpty();
    }
}
