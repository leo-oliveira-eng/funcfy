using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.EitherTests;

public class BindUnitTests
{
    [Fact]
    public void Bind_WhenRight_ShouldChainComputations()
    {
        // Arrange
        var either = Either.Right<string, int>(10);

        // Act
        var bound = either.Bind(value => Either.Right<string, int>(value + 5));

        // Assert
        bound.IsRight.ShouldBeTrue();
        bound.Match(left => left.Length, right => right).ShouldBe(15);
    }

    [Fact]
    public void Bind_WhenRightAndBinderReturnsLeft_ShouldReturnLeft()
    {
        // Arrange
        var either = Either.Right<string, int>(10);

        // Act
        var bound = either.Bind(_ => Either.Left<string, int>("failure"));

        // Assert
        bound.IsLeft.ShouldBeTrue();
        bound.Match(left => left, right => right.ToString()).ShouldBe("failure");
    }

    [Fact]
    public void Bind_WhenLeft_ShouldShortCircuit()
    {
        // Arrange
        var either = Either.Left<string, int>("invalid");
        var binderCallCount = 0;

        // Act
        var bound = either.Bind(value =>
        {
            binderCallCount++;
            return Either.Right<string, int>(value * 2);
        });

        // Assert
        binderCallCount.ShouldBe(0);
        bound.IsLeft.ShouldBeTrue();
        bound.Match(left => left, right => right.ToString()).ShouldBe("invalid");
    }

    [Fact]
    public void Bind_WhenBinderReturnsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Right<string, int>(10);

        // Act & Assert
        Should.Throw<InvalidOperationException>(() => either.Bind<int>(_ => null!));
    }
}
