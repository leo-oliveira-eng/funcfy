using Funcfy.Monads.Extensions;
using System.Runtime.Serialization;

namespace Funcfy.Monads;

/// <summary>
/// Provides factory methods for creating <see cref="Either{TLeft, TRight}"/> instances.
/// </summary>
public static class Either
{
    /// <summary>
    /// Creates a new <see cref="Either{TLeft, TRight}"/> in the left state.
    /// </summary>
    /// <typeparam name="TLeft">The type carried by the left branch.</typeparam>
    /// <typeparam name="TRight">The type carried by the right branch.</typeparam>
    /// <param name="value">The value to store in the left branch.</param>
    /// <returns>A new left <see cref="Either{TLeft, TRight}"/>.</returns>
    public static Either<TLeft, TRight> Left<TLeft, TRight>(TLeft value) => Either<TLeft, TRight>.CreateLeft(value);

    /// <summary>
    /// Creates a new <see cref="Either{TLeft, TRight}"/> in the right state.
    /// </summary>
    /// <typeparam name="TLeft">The type carried by the left branch.</typeparam>
    /// <typeparam name="TRight">The type carried by the right branch.</typeparam>
    /// <param name="value">The value to store in the right branch.</param>
    /// <returns>A new right <see cref="Either{TLeft, TRight}"/>.</returns>
    public static Either<TLeft, TRight> Right<TLeft, TRight>(TRight value) => Either<TLeft, TRight>.CreateRight(value);
}

/// <summary>
/// Represents a recoverable computation that can return either a left value or a right value.
/// By convention, left represents failure and right represents success.
/// </summary>
/// <typeparam name="TLeft">The type carried by the left branch.</typeparam>
/// <typeparam name="TRight">The type carried by the right branch.</typeparam>
[DataContract]
public sealed record Either<TLeft, TRight>
{
    /// <summary>
    /// Indicates whether the current instance is in the left state.
    /// </summary>
    [DataMember]
    public bool IsLeft => Branch == EitherBranch.Left;

    /// <summary>
    /// Indicates whether the current instance is in the right state.
    /// </summary>
    [DataMember]
    public bool IsRight => Branch == EitherBranch.Right;

    [DataMember]
    private EitherBranch Branch { get; init; }

    [DataMember]
    private TLeft? LeftValue { get; init; }

    [DataMember]
    private TRight? RightValue { get; init; }

    private Either() { }

    private Either(EitherBranch branch, TLeft? leftValue = default, TRight? rightValue = default)
    {
        Branch = branch;
        LeftValue = leftValue;
        RightValue = rightValue;
    }

    internal static Either<TLeft, TRight> CreateLeft(TLeft value) => new(EitherBranch.Left, leftValue: value);

    internal static Either<TLeft, TRight> CreateRight(TRight value) => new(EitherBranch.Right, rightValue: value);

    /// <summary>
    /// Pattern matches over the current instance and returns the value produced by the matching branch.
    /// </summary>
    /// <typeparam name="TResult">The type returned by the selected branch.</typeparam>
    /// <param name="onLeft">Function executed when the instance is left.</param>
    /// <param name="onRight">Function executed when the instance is right.</param>
    /// <returns>The result produced by the selected branch.</returns>
    public TResult Match<TResult>(Func<TLeft, TResult> onLeft, Func<TRight, TResult> onRight)
    {
        ArgumentNullException.ThrowIfNull(onLeft);
        ArgumentNullException.ThrowIfNull(onRight);

        return Branch switch
        {
            EitherBranch.Left => onLeft(LeftValue!),
            EitherBranch.Right => onRight(RightValue!),
            _ => throw new InvalidOperationException("The Either instance is in an invalid state.")
        };
    }

    /// <summary>
    /// Pattern matches over the current instance and executes the matching action.
    /// </summary>
    /// <param name="onLeft">Action executed when the instance is left.</param>
    /// <param name="onRight">Action executed when the instance is right.</param>
    public void Match(Action<TLeft> onLeft, Action<TRight> onRight)
    {
        ArgumentNullException.ThrowIfNull(onLeft);
        ArgumentNullException.ThrowIfNull(onRight);

        Match(onLeft.WrapAsFunc(), onRight.WrapAsFunc());
    }

    /// <summary>
    /// Transforms the right value while preserving the left branch.
    /// </summary>
    /// <typeparam name="TResult">The type produced by the mapping function.</typeparam>
    /// <param name="mapper">Function used to transform the right value.</param>
    /// <returns>A new <see cref="Either{TLeft, TResult}"/> containing the mapped right value, or the original left value.</returns>
    public Either<TLeft, TResult> Map<TResult>(Func<TRight, TResult> mapper)
    {
        ArgumentNullException.ThrowIfNull(mapper);

        return Match(
            onLeft: left => Either.Left<TLeft, TResult>(left),
            onRight: right => Either.Right<TLeft, TResult>(mapper(right))
        );
    }

    /// <summary>
    /// Transforms the left value while preserving the right branch.
    /// </summary>
    /// <typeparam name="TResult">The type produced by the mapping function.</typeparam>
    /// <param name="mapper">Function used to transform the left value.</param>
    /// <returns>A new <see cref="Either{TResult, TRight}"/> containing the mapped left value, or the original right value.</returns>
    public Either<TResult, TRight> MapLeft<TResult>(Func<TLeft, TResult> mapper)
    {
        ArgumentNullException.ThrowIfNull(mapper);

        return Match(
            onLeft: left => Either.Left<TResult, TRight>(mapper(left)),
            onRight: right => Either.Right<TResult, TRight>(right)
        );
    }

    /// <summary>
    /// Chains computations that return <see cref="Either{TLeft, TResult}"/>, short-circuiting when the current instance is left.
    /// </summary>
    /// <typeparam name="TResult">The type carried by the bound right value.</typeparam>
    /// <param name="binder">Function used to continue the computation when the instance is right.</param>
    /// <returns>The result produced by the binder, or the original left value.</returns>
    public Either<TLeft, TResult> Bind<TResult>(Func<TRight, Either<TLeft, TResult>> binder)
    {
        ArgumentNullException.ThrowIfNull(binder);

        return Match(
            onLeft: left => Either.Left<TLeft, TResult>(left),
            onRight: right => binder(right) ?? throw new InvalidOperationException("The binder returned null.")
        );
    }

    /// <summary>
    /// Returns the right value when present, otherwise returns the provided fallback value.
    /// </summary>
    /// <param name="fallback">Fallback value used when the instance is left.</param>
    /// <returns>The right value, or <paramref name="fallback"/> when the instance is left.</returns>
    public TRight GetOrElse(TRight fallback)
        => Match(
            onLeft: _ => fallback,
            onRight: right => right
        );

    /// <summary>
    /// Returns the right value when present, otherwise computes a fallback value from the left branch.
    /// </summary>
    /// <param name="onLeft">Function used to produce a fallback value from the left branch.</param>
    /// <returns>The right value, or the value produced by <paramref name="onLeft"/> when the instance is left.</returns>
    public TRight GetOrElse(Func<TLeft, TRight> onLeft)
    {
        ArgumentNullException.ThrowIfNull(onLeft);

        return Match(
            onLeft: onLeft,
            onRight: right => right
        );
    }

    /// <summary>
    /// Returns the current instance when it is right; otherwise computes an alternative instance from the left value.
    /// </summary>
    /// <param name="onLeft">Function used to compute the fallback instance.</param>
    /// <returns>The current instance when it is right, otherwise the fallback instance.</returns>
    public Either<TLeft, TRight> OrElse(Func<TLeft, Either<TLeft, TRight>> onLeft)
    {
        ArgumentNullException.ThrowIfNull(onLeft);

        return Match(
            onLeft: left => onLeft(left) ?? throw new InvalidOperationException("The fallback returned null."),
            onRight: _ => this
        );
    }

    /// <summary>
    /// Returns the current instance when it is right; otherwise computes an alternative instance.
    /// </summary>
    /// <param name="fallback">Function used to compute the fallback instance.</param>
    /// <returns>The current instance when it is right, otherwise the fallback instance.</returns>
    public Either<TLeft, TRight> OrElse(Func<Either<TLeft, TRight>> fallback)
    {
        ArgumentNullException.ThrowIfNull(fallback);

        return Match(
            onLeft: _ => fallback() ?? throw new InvalidOperationException("The fallback returned null."),
            onRight: _ => this
        );
    }

    /// <summary>
    /// Executes a side effect when the instance is right and returns the original instance.
    /// </summary>
    /// <param name="onRight">Action executed when the instance is right.</param>
    /// <returns>The original <see cref="Either{TLeft, TRight}"/> instance.</returns>
    public Either<TLeft, TRight> Tap(Action<TRight> onRight)
    {
        ArgumentNullException.ThrowIfNull(onRight);

        Match(
            onLeft: _ => { },
            onRight: onRight
        );

        return this;
    }

    /// <summary>
    /// Executes a side effect when the instance is left and returns the original instance.
    /// </summary>
    /// <param name="onLeft">Action executed when the instance is left.</param>
    /// <returns>The original <see cref="Either{TLeft, TRight}"/> instance.</returns>
    public Either<TLeft, TRight> TapLeft(Action<TLeft> onLeft)
    {
        ArgumentNullException.ThrowIfNull(onLeft);

        Match(
            onLeft: onLeft,
            onRight: _ => { }
        );

        return this;
    }

    [DataContract]
    private enum EitherBranch : byte
    {
        [EnumMember]
        Left = 0,

        [EnumMember]
        Right = 1
    }
}
