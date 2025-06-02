using Funcfy.Monads.Enums;
using Funcfy.Monads.Extensions;
using System.Runtime.Serialization;

namespace Funcfy.Monads;

/// <summary>
/// The <see cref="Result"/> class represents the outcome of an operation, encapsulating messages that indicate success or failure.
/// This model is useful for operations that do not return a value but need to communicate the result of the operation through messages.
/// </summary>
[DataContract]
public class Result
{
    /// <summary>
    /// Gets the collection of messages associated with the result.
    /// </summary>
    [DataMember]
    public IReadOnlyList<Message> Messages => _messages;

    /// <summary>
    /// Indicates whether the result has any error messages.
    /// A message is considered an error when its type has the category "Error"
    /// </summary>
    /// <remarks>
    /// Messages of type <see cref="MessageType.Warning"/>, <see cref="MessageType.Info"/>, and <see cref="MessageType.None"/> are not considered errors.
    /// Messages of type <see cref="MessageType.BusinessError"/>, <see cref="MessageType.BadRequest"/>, <see cref="MessageType.Unauthorized"/>, <see cref="MessageType.Forbidden"/>, <see cref="MessageType.NotFound"/>, and <see cref="MessageType.Conflict"/> are considered errors.
    /// </remarks>
    [DataMember]
    public bool Failed => _messages.Any(message => message.RepresentsAnError());

    /// <summary>
    /// Indicates whether the result is successful, which is true if there are no error messages.
    /// Messages of type <see cref="MessageType.Warning"/>, <see cref="MessageType.Info"/>, <see cref="MessageType.None"/> are not considered errors.
    /// </summary>
    [DataMember]
    public bool IsSuccessful => !Failed;

    private readonly List<Message> _messages = [];

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    internal protected Result() { }

    #endregion

    #region Factory Methods

    /// <summary>
    /// Creates a new instance of the <see cref="Result"/> class with no messages.
    /// </summary>
    /// <returns>The created <see cref="Result"/> instance.</returns>
    public static Result Create() => new();

    /// <summary>
    /// Creates a new instance of the <see cref="Result"/> class representing a successful operation with no messages.
    /// </summary>
    /// <returns>The created <see cref="Result"/> instance.</returns>
    public static Result Success() => Create();

    /// <summary>
    /// Creates a new instance of the <see cref="Result"/> class representing a failure with the specified message.
    /// </summary>
    /// <param name="message">The message that indicates the failure of the operation.</param>
    /// <returns>The created <see cref="Result"/> instance.</returns>
    public static Result Failure(Message message) => Create().AddMessage(message);

    /// <summary>
    /// Adds a message to the result, indicating the outcome of an operation.
    /// </summary>
    /// <param name="message">The message to add to the result. This message can represent an informational, warning, or error condition.</param>
    /// <returns>The current <see cref="Result"/> instance with the added message.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="message"/> is null.</exception>
    public Result AddMessage(Message message)
    {
        ArgumentNullException.ThrowIfNull(message);

        _messages.Add(message);

        return this;
    }

    #endregion
}
