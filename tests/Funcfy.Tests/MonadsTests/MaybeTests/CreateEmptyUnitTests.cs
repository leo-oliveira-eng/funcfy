using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.MaybeTests;

public class CreateEmptyUnitTests
{
    [Fact]
    public void CreateEmpty_ShouldReturnInstance()
    {
        // Arrange
        // Act
        var response = Maybe<int>.Create();

        // Assert
        response.IsEmpty.ShouldBeTrue();
        response.Value.ShouldBe(0);
    }

    [Fact]
    public void CreateEmpty_ShouldReturnInstanceWithDefaultValue()
    {
        // Arrange
        // Act
        var response = Maybe<int?>.Create();

        // Assert
        response.IsEmpty.ShouldBeTrue();
        response.Value.ShouldBeNull();
    }
}
