using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.EitherTests;

public class MapUnitTests
{
    [Fact]
    public void Map_WhenRight_ShouldTransformRightValue()
    {
        // Arrange
        var either = Either.Right<string, int>(21);

        // Act
        var mapped = either.Map(value => value * 2);

        // Assert
        mapped.IsRight.ShouldBeTrue();
        mapped.Match(left => left.Length, right => right).ShouldBe(42);
    }

    [Fact]
    public void Map_WhenLeft_ShouldPreserveLeftValue()
    {
        // Arrange
        var either = Either.Left<string, int>("invalid");

        // Act
        var mapped = either.Map(value => value * 2);

        // Assert
        mapped.IsLeft.ShouldBeTrue();
        mapped.Match(left => left, right => right.ToString()).ShouldBe("invalid");
    }

    [Fact]
    public void MapLeft_WhenLeft_ShouldTransformLeftValue()
    {
        // Arrange
        var either = Either.Left<string, int>("invalid");

        // Act
        var mapped = either.MapLeft(left => left.ToUpperInvariant());

        // Assert
        mapped.IsLeft.ShouldBeTrue();
        mapped.Match(left => left, right => right.ToString()).ShouldBe("INVALID");
    }

    [Fact]
    public void MapLeft_WhenRight_ShouldPreserveRightValue()
    {
        // Arrange
        var either = Either.Right<string, int>(42);

        // Act
        var mapped = either.MapLeft(left => left.Length);

        // Assert
        mapped.IsRight.ShouldBeTrue();
        mapped.Match(left => left, right => right).ShouldBe(42);
    }
}
