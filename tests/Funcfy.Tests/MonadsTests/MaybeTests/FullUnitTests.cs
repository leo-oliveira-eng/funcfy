using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.MaybeTests;

public class FullUnitTests
{
    [Fact]
    public void Full_ShouldReturnInstance()
    {
        // Arrange
        const int value = 1;

        // Act
        var response = Maybe<int>.Full(value);

        // Assert
        response.IsFull.ShouldBeTrue();
        response.Value.ShouldBe(value);
    }
}
