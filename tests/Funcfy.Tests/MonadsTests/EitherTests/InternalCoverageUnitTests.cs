using Funcfy.Monads;
using System.Reflection;

namespace Funcfy.Tests.MonadsTests.EitherTests;

public class InternalCoverageUnitTests
{
    [Fact]
    public void WithExpression_ShouldCreateEqualButDistinctCopy()
    {
        // Arrange
        var original = Either.Right<string, int>(42);

        // Act
        var copy = original with { };

        // Assert
        copy.ShouldBe(original);
        ReferenceEquals(copy, original).ShouldBeFalse();
    }

    [Fact]
    public void Match_WhenEitherIsInInvalidState_ShouldThrow()
    {
        // Arrange
        var either = CreateInvalidEither<string, int>();

        // Act & Assert
        Should.Throw<InvalidOperationException>(() => either.Match(left => left, right => right.ToString()));
    }

    private static Either<TLeft, TRight> CreateInvalidEither<TLeft, TRight>()
    {
        var type = typeof(Either<TLeft, TRight>);

        var either = (Either<TLeft, TRight>)Activator.CreateInstance(type, nonPublic: true)!;

        var branchField = type.GetField("<Branch>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
        branchField.ShouldNotBeNull();

        var branchType = branchField!.FieldType;
        var invalidBranch = Enum.ToObject(branchType, 2);
        branchField.SetValue(either, invalidBranch);

        return either;
    }
}
