using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.ResultTests;

public class SetValueUnitTests
{
    [Fact]
    public void SetValue_WhenCalled_ShouldReturnResultWithValue()
    {
        // Arrange
        var result = Result<int>.Create();
        var value = 1;

        // Act
        var updatedResult = result.SetValue(value);

        // Assert
        updatedResult.ShouldNotBeNull();
        updatedResult.Data.IsFull.ShouldBeTrue();
        updatedResult.Data.Value.ShouldBe(value);
        updatedResult.Messages.ShouldBeEmpty();
    }
}
