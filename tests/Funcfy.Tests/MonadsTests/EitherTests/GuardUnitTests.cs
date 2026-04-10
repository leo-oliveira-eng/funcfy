using Funcfy.Monads;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.EitherTests;

public class GuardUnitTests
{
    [Fact]
    public void Match_WhenLeftFuncIsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Left<string, int>("failure");

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => either.Match<string>(null!, right => right.ToString()));
    }

    [Fact]
    public void Match_WhenRightFuncIsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Right<string, int>(42);

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => either.Match(left => left, null!));
    }

    [Fact]
    public void Match_ActionOverload_WhenLeftActionIsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Left<string, int>("failure");

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => either.Match(null!, _ => { }));
    }

    [Fact]
    public void Match_ActionOverload_WhenRightActionIsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Right<string, int>(42);

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => either.Match(_ => { }, null!));
    }

    [Fact]
    public void Map_WhenMapperIsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Right<string, int>(42);

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => either.Map<string>(null!));
    }

    [Fact]
    public void MapLeft_WhenMapperIsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Left<string, int>("failure");

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => either.MapLeft<int>(null!));
    }

    [Fact]
    public void Bind_WhenBinderIsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Right<string, int>(42);

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => either.Bind<int>(null!));
    }

    [Fact]
    public void GetOrElse_WhenDelegateIsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Left<string, int>("failure");

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => either.GetOrElse(null!));
    }

    [Fact]
    public void OrElse_WhenLeftDependentFallbackIsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Left<string, int>("failure");

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => either.OrElse((Func<string, Either<string, int>>)null!));
    }

    [Fact]
    public void OrElse_WhenParameterlessFallbackIsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Left<string, int>("failure");

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => either.OrElse((Func<Either<string, int>>)null!));
    }

    [Fact]
    public void Tap_WhenActionIsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Right<string, int>(42);

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => either.Tap(null!));
    }

    [Fact]
    public void TapLeft_WhenActionIsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Left<string, int>("failure");

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => either.TapLeft(null!));
    }

    [Fact]
    public void ToEither_WhenOnEmptyIsNull_ShouldThrow()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => maybe.ToEither<string, int>(null!));
    }

    [Fact]
    public void ToResult_WhenMapLeftIsNull_ShouldThrow()
    {
        // Arrange
        var either = Either.Left<string, int>("failure");

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => either.ToResult(null!));
    }
}
