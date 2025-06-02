using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.EmptyResultTests;

public class CreateUnitTests
{
    [Fact]
    public void Create_ShouldReturnEmptyResult()
    {
        // Arrange & Act
        var result = Result.Create();

        // Assert
        result.ShouldNotBeNull();
        result.Messages.ShouldBeEmpty();
        result.IsSuccessful.ShouldBeTrue();
        result.Failed.ShouldBeFalse();
    }
}
