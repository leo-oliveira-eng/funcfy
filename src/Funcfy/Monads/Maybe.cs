using Funcfy.Monads.Extensions;
using System.Runtime.Serialization;

namespace Funcfy.Monads;

/// <summary>
/// Represents a value that may or may not be present.
/// </summary>
/// <typeparam name="TValue">The type of the value that may be present.</typeparam>
[DataContract]
public sealed record Maybe<TValue>
{
    #region Properties

    /// <summary>
    /// Indicates whether the <see cref="Maybe{TValue}"/> instance has a value.
    /// </summary>
    [DataMember]
    public bool IsFull => !IsEmpty;

    /// <summary>
    /// Indicates whether the <see cref="Maybe{TValue}"/> instance has a value.
    /// </summary>
    [DataMember]
    public bool IsEmpty => Value is null || Value.Equals(default(TValue));

    /// <summary>
    /// The value of the <see cref="Maybe{TValue}"/> instance.
    /// </summary>
    [DataMember]
    public TValue? Value { get; init; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Maybe{TValue}"/> class.
    /// </summary>
    private Maybe() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Maybe{TValue}"/> class with the specified value.
    /// </summary>
    /// <param name="value">
    /// The value to wrap in a <see cref="Maybe{TValue}"/>.
    /// </param>
    private Maybe(TValue value) => Value = value;

    #endregion

    #region Factory Methods

    /// <summary>
    /// Creates a new instance of the <see cref="Maybe{TValue}"/> class with the specified value.
    /// </summary>
    /// <param name="value">
    /// The value to wrap.
    /// </param>
    /// <returns>
    /// A <see cref="Maybe{TValue}"/> where <see cref="IsFull"/> and <see cref="Value"/> equals <paramref name="value"/>.
    /// </returns>
    public static Maybe<TValue> Create(TValue value) => new(value);

    /// <summary>
    /// Creates a new instance of the <see cref="Maybe{TValue}"/> class with no value.
    /// </summary>
    /// <returns>
    /// A <see cref="Maybe{TValue}"/> where <see cref="IsEmpty"/>.
    /// </returns>
    public static Maybe<TValue> Create() => new();

    #endregion

    #region Aliases

    /// <summary>
    /// Alias for <see cref="Create()"/>
    /// returns an empty Maybe.
    /// </summary>
    public static Maybe<TValue> Empty() => Create();

    /// <summary>
    /// Alias for <see cref="Create(TValue)"/>
    /// returns a full Maybe containing the specified value.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    public static Maybe<TValue> Full(TValue value) => Create(value);

    #endregion

    #region Match

    /// <summary>
    /// Executes the specified function if the <see cref="Maybe{TValue}"/> instance has a value, otherwise executes another function.
    /// </summary>
    /// <typeparam name="TResult">Type of the result returned by the function.</typeparam>
    /// <param name="onFull">Function to execute if the <see cref="Maybe{TValue}"/> instance has a value.</param>
    /// <param name="onEmpty">Function to execute if the <see cref="Maybe{TValue}"/> instance does not have a value.</param>
    /// <returns>
    /// Returns the result of the executed function based on whether the <see cref="Maybe{TValue}"/> instance has a value or not.
    /// </returns>
    public TResult Match<TResult>(Func<TValue, TResult> onFull, Func<TResult> onEmpty)
    {
        ArgumentNullException.ThrowIfNull(onFull);
        ArgumentNullException.ThrowIfNull(onEmpty);

        return IsFull
            ? onFull(Value!)
            : onEmpty();
    }

    /// <summary>
    /// Executes the specified action if the <see cref="Maybe{TValue}"/> instance has a value, otherwise executes another action.
    /// </summary>
    /// <param name="onFull">The action to execute if the <see cref="Maybe{TValue}"/> instance has a value.</param>
    /// <param name="onEmpty">The action to execute if the <see cref="Maybe{TValue}"/> instance does not have a value.</param>
    public void Match(Action<TValue> onFull, Action onEmpty)
    {
        ArgumentNullException.ThrowIfNull(onFull);
        ArgumentNullException.ThrowIfNull(onEmpty);

        Match(onFull.WrapAsFunc(), onEmpty.WrapAsFunc());
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Transforms the wrapped value when the current instance is full.
    /// </summary>
    /// <typeparam name="TResult">The type produced by the mapping function.</typeparam>
    /// <param name="mapper">Function used to transform the wrapped value.</param>
    /// <returns>
    /// A new <see cref="Maybe{TResult}"/> carrying the transformed value when full; otherwise an empty <see cref="Maybe{TResult}"/>.
    /// </returns>
    public Maybe<TResult> Map<TResult>(Func<TValue, TResult> mapper)
    {
        ArgumentNullException.ThrowIfNull(mapper);

        return Match(
            onFull: value => Maybe<TResult>.Create(mapper(value)),
            onEmpty: Maybe<TResult>.Empty
        );
    }

    /// <summary>
    /// Chains computations that return <see cref="Maybe{TValue}"/>, short-circuiting when the current instance is empty.
    /// </summary>
    /// <typeparam name="TResult">The type carried by the bound value.</typeparam>
    /// <param name="binder">Function used to continue the computation when the instance is full.</param>
    /// <returns>
    /// The result produced by the binder when full; otherwise an empty <see cref="Maybe{TResult}"/>.
    /// </returns>
    public Maybe<TResult> Bind<TResult>(Func<TValue, Maybe<TResult>> binder)
    {
        ArgumentNullException.ThrowIfNull(binder);

        return Match(
            onFull: value => binder(value) ?? throw new InvalidOperationException("The binder returned null."),
            onEmpty: Maybe<TResult>.Empty
        );
    }

    /// <summary>
    /// Returns the wrapped value when present, otherwise returns the provided fallback value.
    /// </summary>
    /// <param name="fallback">Fallback value used when the current instance is empty.</param>
    /// <returns>The wrapped value when full; otherwise <paramref name="fallback"/>.</returns>
    public TValue GetOrElse(TValue fallback)
        => Match(
            onFull: value => value,
            onEmpty: () => fallback
        );

    /// <summary>
    /// Returns the wrapped value when present, otherwise computes a fallback value.
    /// </summary>
    /// <param name="fallback">Function used to compute the fallback value when the current instance is empty.</param>
    /// <returns>The wrapped value when full; otherwise the value produced by <paramref name="fallback"/>.</returns>
    public TValue GetOrElse(Func<TValue> fallback)
    {
        ArgumentNullException.ThrowIfNull(fallback);

        return Match(
            onFull: value => value,
            onEmpty: fallback
        );
    }

    /// <summary>
    /// Returns the current instance when it is full; otherwise computes a fallback instance.
    /// </summary>
    /// <param name="fallback">Function used to compute the fallback instance when the current instance is empty.</param>
    /// <returns>The current instance when full; otherwise the fallback instance.</returns>
    public Maybe<TValue> OrElse(Func<Maybe<TValue>> fallback)
    {
        ArgumentNullException.ThrowIfNull(fallback);

        return Match(
            onFull: _ => this,
            onEmpty: () => fallback() ?? throw new InvalidOperationException("The fallback returned null.")
        );
    }

    /// <summary>
    /// Executes a side effect when the current instance is full and returns the original instance.
    /// </summary>
    /// <param name="onFull">Action executed when the current instance is full.</param>
    /// <returns>The original <see cref="Maybe{TValue}"/> instance.</returns>
    public Maybe<TValue> Tap(Action<TValue> onFull)
    {
        ArgumentNullException.ThrowIfNull(onFull);

        Match(
            onFull: onFull,
            onEmpty: () => { }
        );

        return this;
    }

    #endregion

    #region Conversion Operators

    /// <summary>
    /// Implicitly converts a <typeparamref name="TValue"/> to a <see cref="Maybe{TValue}"/>.
    /// </summary>
    /// <param name="value"><see cref="Maybe{TValue}"/> instance containing <paramref name="value"/>.</param>
    public static implicit operator Maybe<TValue>(TValue value) => Create(value);

    #endregion
}
