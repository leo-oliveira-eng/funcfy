using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.MaybeTests;

public class MapUnitTests
{
    [Fact]
    public void Map_WhenFull_ShouldTransformValue()
    {
        // Arrange
        Maybe<int> maybe = 21;

        // Act
        var mapped = maybe.Map(value => value * 2);

        // Assert
        mapped.IsFull.ShouldBeTrue();
        mapped.Value.ShouldBe(42);
    }

    [Fact]
    public void Map_WhenEmpty_ShouldPreserveEmptyState()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();

        // Act
        var mapped = maybe.Map(value => value * 2);

        // Assert
        mapped.IsEmpty.ShouldBeTrue();
        mapped.Value.ShouldBe(0);
    }
}
