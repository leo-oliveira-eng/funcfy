namespace Funcfy.Monads.Extensions;

/// <summary>
/// Provides conversion helpers for <see cref="Either{TLeft, TRight}"/>.
/// </summary>
public static class EitherExtensions
{
    /// <summary>
    /// Converts a <see cref="Maybe{TRight}"/> to an <see cref="Either{TLeft, TRight}"/>.
    /// </summary>
    /// <typeparam name="TLeft">The type carried by the left branch.</typeparam>
    /// <typeparam name="TRight">The type carried by the right branch.</typeparam>
    /// <param name="maybe">The optional value to convert.</param>
    /// <param name="onEmpty">Function used to produce the left value when the maybe is empty.</param>
    /// <returns>A right either when the maybe is full; otherwise a left either.</returns>
    public static Either<TLeft, TRight> ToEither<TLeft, TRight>(this Maybe<TRight> maybe, Func<TLeft> onEmpty)
    {
        ArgumentNullException.ThrowIfNull(maybe);
        ArgumentNullException.ThrowIfNull(onEmpty);

        return maybe.Match(
            onFull: value => Either.Right<TLeft, TRight>(value),
            onEmpty: () => Either.Left<TLeft, TRight>(onEmpty())
        );
    }

    /// <summary>
    /// Converts an <see cref="Either{TLeft, TRight}"/> to a <see cref="Maybe{TRight}"/> by discarding the left value.
    /// </summary>
    /// <typeparam name="TLeft">The type carried by the left branch.</typeparam>
    /// <typeparam name="TRight">The type carried by the right branch.</typeparam>
    /// <param name="either">The either value to convert.</param>
    /// <returns>A full maybe when the either is right; otherwise an empty maybe.</returns>
    public static Maybe<TRight> ToMaybe<TLeft, TRight>(this Either<TLeft, TRight> either)
    {
        ArgumentNullException.ThrowIfNull(either);

        return either.Match(
            onLeft: _ => Maybe<TRight>.Empty(),
            onRight: Maybe<TRight>.Full
        );
    }

    /// <summary>
    /// Converts an <see cref="Either{TLeft, TRight}"/> to a <see cref="Result{TRight}"/>.
    /// </summary>
    /// <typeparam name="TLeft">The type carried by the left branch.</typeparam>
    /// <typeparam name="TRight">The type carried by the right branch.</typeparam>
    /// <param name="either">The either value to convert.</param>
    /// <param name="mapLeft">Function used to map the left value to a <see cref="Message"/>.</param>
    /// <returns>A successful result for a right either, or a failure result for a left either.</returns>
    public static Result<TRight> ToResult<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, Message> mapLeft)
    {
        ArgumentNullException.ThrowIfNull(either);
        ArgumentNullException.ThrowIfNull(mapLeft);

        return either.Match(
            onLeft: left =>
            {
                var message = mapLeft(left);
                ArgumentNullException.ThrowIfNull(message);
                return Result<TRight>.Failure(message);
            },
            onRight: Result<TRight>.Success
        );
    }
}
