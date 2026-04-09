using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.EitherTests;

public class CreateUnitTests
{
    [Fact]
    public void Left_WhenCalled_ShouldCreateLeftInstance()
    {
        // Arrange
        const string error = "invalid";

        // Act
        var either = Either.Left<string, int>(error);

        // Assert
        either.IsLeft.ShouldBeTrue();
        either.IsRight.ShouldBeFalse();
        either.Match(left => left, right => right.ToString()).ShouldBe(error);
    }

    [Fact]
    public void Right_WhenCalled_ShouldCreateRightInstance()
    {
        // Arrange
        const int value = 42;

        // Act
        var either = Either.Right<string, int>(value);

        // Assert
        either.IsRight.ShouldBeTrue();
        either.IsLeft.ShouldBeFalse();
        either.Match(left => left.Length, right => right).ShouldBe(value);
    }
}
