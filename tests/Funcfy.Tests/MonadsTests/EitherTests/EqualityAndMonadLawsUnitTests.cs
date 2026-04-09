using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.EitherTests;

public class EqualityAndMonadLawsUnitTests
{
    [Fact]
    public void Equality_WhenSameBranchAndSamePayload_ShouldBeEqual()
    {
        // Arrange
        var first = Either.Left<string, int>("failure");
        var second = Either.Left<string, int>("failure");

        // Act & Assert
        first.ShouldBe(second);
    }

    [Fact]
    public void Equality_WhenDifferentBranchesWithDefaultPayload_ShouldNotBeEqual()
    {
        // Arrange
        var left = Either.Left<int, int>(default);
        var right = Either.Right<int, int>(default);

        // Act & Assert
        left.ShouldNotBe(right);
    }

    [Fact]
    public void Equality_WhenDifferentBranchesWithEqualPayloads_ShouldNotBeEqual()
    {
        // Arrange
        var left = Either.Left<int, int>(42);
        var right = Either.Right<int, int>(42);

        // Act & Assert
        left.ShouldNotBe(right);
    }

    [Fact]
    public void MonadLeftIdentity_ShouldHold()
    {
        // Arrange
        const int value = 10;
        Either<string, int> Step(int input) => Either.Right<string, int>(input + 5);

        // Act
        var leftSide = Either.Right<string, int>(value).Bind(Step);
        var rightSide = Step(value);

        // Assert
        leftSide.ShouldBe(rightSide);
    }

    [Fact]
    public void MonadRightIdentity_ShouldHoldForRight()
    {
        // Arrange
        var either = Either.Right<string, int>(10);

        // Act
        var result = either.Bind(value => Either.Right<string, int>(value));

        // Assert
        result.ShouldBe(either);
    }

    [Fact]
    public void MonadRightIdentity_ShouldHoldForLeft()
    {
        // Arrange
        var either = Either.Left<string, int>("failure");

        // Act
        var result = either.Bind(value => Either.Right<string, int>(value));

        // Assert
        result.ShouldBe(either);
    }

    [Fact]
    public void MonadAssociativity_ShouldHoldForRight()
    {
        // Arrange
        var either = Either.Right<string, int>(10);
        Either<string, int> StepOne(int input) => Either.Right<string, int>(input + 5);
        Either<string, int> StepTwo(int input) => Either.Right<string, int>(input * 2);

        // Act
        var leftSide = either.Bind(StepOne).Bind(StepTwo);
        var rightSide = either.Bind(value => StepOne(value).Bind(StepTwo));

        // Assert
        leftSide.ShouldBe(rightSide);
    }

    [Fact]
    public void MonadAssociativity_ShouldHoldForLeft()
    {
        // Arrange
        var either = Either.Left<string, int>("failure");
        Either<string, int> StepOne(int input) => Either.Right<string, int>(input + 5);
        Either<string, int> StepTwo(int input) => Either.Right<string, int>(input * 2);

        // Act
        var leftSide = either.Bind(StepOne).Bind(StepTwo);
        var rightSide = either.Bind(value => StepOne(value).Bind(StepTwo));

        // Assert
        leftSide.ShouldBe(rightSide);
    }
}
