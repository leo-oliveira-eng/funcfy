using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.MaybeTests;

public class ImplicitConversionUnitTests
{
    [Fact]
    public void ImplicitConversion_ShouldReturnInstance()
    {
        // Arrange
        const int value = 1;

        // Act
        Maybe<int> response = value;

        // Assert
        response.HasValue.ShouldBeTrue();
        response.Value.ShouldBe(value);
    }
}
