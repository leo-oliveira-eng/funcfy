using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.ResultTests;

public class ImplicitConversionUnitTests
{
    [Fact]
    public void ImplicitConversion_FromResultToValue_ShouldReturnValue()
    {
        // Arrange
        int value = 1;
        Result<int> Response() => value;

        // Act
        var response = Response();

        // Assert
        response.ShouldNotBeNull();
        response.Data.IsFull.ShouldBeTrue();
        response.Data.Value.ShouldBe(value);
        response.Messages.ShouldBeEmpty();
        response.ShouldBeOfType<Result<int>>();
    }
}
