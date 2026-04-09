using Funcfy.Monads;
using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;

namespace Funcfy.Tests.MonadsTests.EitherTests;

public class ConversionUnitTests
{
    [Fact]
    public void ToEither_WhenMaybeIsFull_ShouldReturnRight()
    {
        // Arrange
        Maybe<int> maybe = 42;

        // Act
        var either = maybe.ToEither(() => "missing");

        // Assert
        either.IsRight.ShouldBeTrue();
        either.Match(left => left.Length, right => right).ShouldBe(42);
    }

    [Fact]
    public void ToEither_WhenMaybeIsEmpty_ShouldReturnLeft()
    {
        // Arrange
        var maybe = Maybe<int>.Empty();

        // Act
        var either = maybe.ToEither(() => "missing");

        // Assert
        either.IsLeft.ShouldBeTrue();
        either.Match(left => left, right => right.ToString()).ShouldBe("missing");
    }

    [Fact]
    public void ToMaybe_WhenEitherIsRight_ShouldReturnFullMaybe()
    {
        // Arrange
        var either = Either.Right<string, int>(42);

        // Act
        var maybe = either.ToMaybe();

        // Assert
        maybe.IsFull.ShouldBeTrue();
        maybe.Value.ShouldBe(42);
    }

    [Fact]
    public void ToMaybe_WhenEitherIsLeft_ShouldReturnEmptyMaybe()
    {
        // Arrange
        var either = Either.Left<string, int>("missing");

        // Act
        var maybe = either.ToMaybe();

        // Assert
        maybe.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void ToResult_WhenEitherIsRight_ShouldReturnSuccessfulResult()
    {
        // Arrange
        var either = Either.Right<string, int>(42);

        // Act
        var result = either.ToResult(left => Message.Create(left, MessageType.BusinessError));

        // Assert
        result.IsSuccessful.ShouldBeTrue();
        result.Data.IsFull.ShouldBeTrue();
        result.Data.Value.ShouldBe(42);
        result.Messages.ShouldBeEmpty();
    }

    [Fact]
    public void ToResult_WhenEitherIsLeft_ShouldReturnFailureResult()
    {
        // Arrange
        var either = Either.Left<string, int>("missing");

        // Act
        var result = either.ToResult(left => Message.Create(left, MessageType.NotFound, code: "NF001"));

        // Assert
        result.Failed.ShouldBeTrue();
        result.Data.IsEmpty.ShouldBeTrue();
        result.Messages.Count.ShouldBe(1);
        result.Messages[0].Content.ShouldBe("missing");
        result.Messages[0].Type.ShouldBe(MessageType.NotFound);
        result.Messages[0].Code.ShouldBe("NF001");
    }
}
