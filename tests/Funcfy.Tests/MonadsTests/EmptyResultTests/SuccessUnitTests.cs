using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.EmptyResultTests;
public class SuccessUnitTests
{
    [Fact]
    public void Success_ShouldReturnEmptyResult()
    {
        // Arrange & Act
        var result = Result.Success();

        // Assert
        result.ShouldNotBeNull();
        result.Messages.ShouldBeEmpty();
        result.IsSuccessful.ShouldBeTrue();
        result.Failed.ShouldBeFalse();
    }
}
