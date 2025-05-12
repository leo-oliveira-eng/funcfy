using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.MaybeTests;

public class CreateUnitTests
{
    [Fact]
    public void Create_ShouldReturnInstance()
    {
        // Arrange
        const int value = 1;

        // Act
        var response = Maybe<int>.Create(value);

        // Assert
        response.IsFull.ShouldBeTrue();
        response.Value.ShouldBe(value);
    }

    [Fact]
    public void Create_ShouldReturnInstanceWithDefaultValue()
    {
        // Arrange
        int? value = null;

        // Act
        var response = Maybe<int?>.Create(value);

        // Assert
        response.IsEmpty.ShouldBeTrue();
        response.Value.ShouldBeNull();
    }
}
