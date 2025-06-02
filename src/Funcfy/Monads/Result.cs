using Funcfy.Monads.Enums;
using System.Runtime.Serialization;

namespace Funcfy.Monads;

/// <summary>
/// The <see cref="Result{TValue}"/> class represents the outcome of an operation that returns a value.
/// </summary>
/// <typeparam name="TValue">The type of the value that may be present in the result.</typeparam>
[DataContract]
public class Result<TValue> : Result
{
    /// <summary>
    /// Gets the value of the result, wrapped in a <see cref="Maybe{TValue}"/> instance.
    /// </summary>
    [DataMember]
    public Maybe<TValue> Data { get; private set; } = Maybe<TValue>.Create();

    #region Constructors

    private Result() : base() { }

    private Result(TValue value) => SetValue(value);

    #endregion

    #region Factory Methods

    /// <summary>
    /// Creates a new instance of the <see cref="Result{TValue}"/> class with no value and no messages.
    /// </summary>
    /// <returns>Creates a new instance of the <see cref="Result{TValue}"/> class with no value and no messages.</returns>
    public static new Result<TValue> Create() => new();

    /// <summary>
    /// Creates a new instance of the <see cref="Result{TValue}"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value to wrap in the result.</param>
    /// <returns>A new instance of the <see cref="Result{TValue}"/> class where <see cref="Data"/> encapsulates <paramref name="value"/>.</returns>
    public static Result<TValue> Create(TValue value) => new(value);

    /// <summary>
    /// Creates a new instance of the <see cref="Result{TValue}"/> class representing a successful operation with no messages and no value.
    /// </summary>
    /// <returns>A new instance of the <see cref="Result{TValue}"/> class representing a successful operation with no messages and no value.</returns>
    public static new Result<TValue> Success() => new();

    /// <summary>
    /// Creates a new instance of the <see cref="Result{TValue}"/> class representing a successful operation with the specified value.
    /// </summary>
    /// <param name="value">The value to wrap in the result.</param>
    /// <returns>A new instance of the <see cref="Result{TValue}"/> class representing a successful operation with the specified value.</returns>
    public static Result<TValue> Success(TValue value) => new(value);

    /// <summary>
    /// Creates a new instance of the <see cref="Result{TValue}"/> class representing a failure with the specified message.
    /// </summary>
    /// <param name="message">
    /// The message that indicates the failure of the operation. 
    /// This message will be added to the result's messages collection.
    /// </param>
    /// <returns>A new instance of the <see cref="Result{TValue}"/> class representing a failure with the specified message. </returns>
    public static new Result<TValue> Failure(Message message) => Create().AddMessage(message);

    /// <summary>
    /// Creates a new instance of the <see cref="Result{TValue}"/> class representing a failure with the specified content, type, code, and source.
    /// </summary>
    /// <param name="content">Textual content of the message.</param>
    /// <param name="type">The type of the message, which can be informational, warning, or error.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">The source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>A new instance of the <see cref="Result{TValue}"/> class representing a failure with the specified content, type, code, and source.</returns>
    public static Result<TValue> Failure(string content, MessageType type = MessageType.BusinessError, string? code = null, string? source = null)
    {
        var message = Message.Create(content, type, code, source);
        return Create().AddMessage(message);
    }

    #endregion

    #region Other Methods

    /// <summary>
    /// Adds a message to the result's messages collection.
    /// </summary>
    /// <param name="message">The message to add to the result.</param>
    /// <returns>Same <see cref="Result{TValue}"/> instance with the added message.</returns>
    public new Result<TValue> AddMessage(Message message)
    {
        base.AddMessage(message);
        return this;
    }

    /// <summary>
    /// Sets the value of the result.
    /// </summary>
    /// <param name="value">The value to set in the result.</param>
    /// <returns>Same <see cref="Result{TValue}"/> instance with the value set.</returns>
    public Result<TValue> SetValue(TValue value)
    {
        Data = Maybe<TValue>.Create(value);
        return this;
    }

    #endregion

    #region Conversion Operators

    /// <summary>
    /// Implicitly converts a value to a <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="value">The value to convert to a <see cref="Result{TValue}"/>.</param>
    public static implicit operator Result<TValue>(TValue value) => Create(value);

    #endregion
}
