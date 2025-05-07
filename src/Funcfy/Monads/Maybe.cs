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
    public bool HasValue => Value is not null;

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

    #region Methods

    /// <summary>
    /// Creates a new instance of the <see cref="Maybe{TValue}"/> class with the specified value.
    /// </summary>
    /// <param name="value">
    /// The value to wrap.
    /// </param>
    /// <returns>
    /// A <see cref="Maybe{TValue}"/> where <see cref="HasValue"/> is <c>true</c> and <see cref="Value"/> equals <paramref name="value"/>.
    /// </returns>
    public static Maybe<TValue> Create(TValue value) => new(value);

    /// <summary>
    /// Creates a new instance of the <see cref="Maybe{TValue}"/> class with no value.
    /// </summary>
    /// <returns>
    /// A <see cref="Maybe{TValue}"/> where <see cref="HasValue"/> is <c>false</c>.
    /// </returns>
    public static Maybe<TValue> Create() => new();

    #endregion

    #region Conversion Operators

    /// <summary>
    /// Implicitly converts a <see cref="TValue"/> to a <see cref="Maybe{TValue}"/>.
    /// </summary>
    /// <param name="value">
    /// A <see cref="Maybe{TValue}"/> instance containing <paramref name="value"/>.
    /// </param>
    public static implicit operator Maybe<TValue>(TValue value) => Create(value);

    #endregion
}
