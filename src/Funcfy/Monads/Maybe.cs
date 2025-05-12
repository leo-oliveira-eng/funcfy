using System.Runtime.Serialization;

namespace Funcfy.Monads;

/// <summary>
/// Represents a value that may or may not be present.
/// </summary>
/// <typeparam name="TValue">
/// The type of the value that may be present.
/// </typeparam>
[DataContract]
public sealed class Maybe<TValue>
{
    #region Properties

    /// <summary>
    /// Indicates whether the <see cref="Maybe{TValue}"/> instance has a value.
    /// </summary>
    [DataMember]
    public bool IsFull => Value is not null;

    /// <summary>
    /// Indicates whether the <see cref="Maybe{TValue}"/> instance has a value.
    /// </summary>
    [DataMember]
    public bool IsEmpty => !IsFull;

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

    #region Conversion Operators

    /// <summary>
    /// Implicitly converts a <typeparamref name="TValue"/> to a <see cref="Maybe{TValue}"/>.
    /// </summary>
    /// <param name="value">
    /// A <see cref="Maybe{TValue}"/> instance containing <paramref name="value"/>.
    /// </param>
    public static implicit operator Maybe<TValue>(TValue value) => Create(value);

    #endregion
}
