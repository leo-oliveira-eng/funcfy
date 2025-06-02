using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.ResultTests;

public class CreateUnitTests
{
    [Fact]
    public void Create_WhenCalled_ShouldReturnEmptyResult()
    {
        // Arrange & Act
        var result = Result<int>.Create();

        // Assert
        result.ShouldNotBeNull();
        result.Data.IsFull.ShouldBeFalse();
        result.Messages.ShouldBeEmpty();
    }

    [Fact]
    public void Create_WithValue_WhenCalled_ShouldReturnResultWithValue()
    {
        // Arrange
        var value = 1;

        // Act
        var result = Result<int>.Create(value);

        // Assert
        result.ShouldNotBeNull();
        result.Data.IsFull.ShouldBeTrue();
        result.Data.Value.ShouldBe(value);
        result.Messages.ShouldBeEmpty();
    }
}
