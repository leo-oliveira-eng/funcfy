using Funcfy.Monads.Enums;
using System.Runtime.Serialization;

namespace Funcfy.Monads;

/// <summary>
/// The <see cref="Message"/> class represents a message that can be associated with a result or operation.
/// </summary>
[DataContract]
public sealed record Message
{
    #region Properties

    /// <summary>
    /// The textual content of the message.
    /// </summary>
    [DataMember]
    public string Content { get; init; } = null!;

    /// <summary>
    /// The type of the message, which can be an information, warning, or some error.
    /// </summary>
    [DataMember]
    public MessageType Type { get; init; }

    /// <summary>
    /// The code associated with the message, which can be used for categorization or identification of the message.
    /// </summary>
    [DataMember]
    public string? Code { get; init; }

    /// <summary>
    /// The source of the message, which can be used to identify where the message originated from, such as a specific module or component.
    /// </summary>
    [DataMember]
    public string? Source { get; init; }

    #endregion

    #region Constructors

    private Message(string content, MessageType type, string? code = null, string? source = null)
    {
        Content = content;
        Type = type;
        Code = code;
        Source = source;
    }

    #endregion

    #region Factory Methods

    /// <summary>
    /// Creates a new instance of the <see cref="Message"/> class with the specified content, type, code, and source.
    /// </summary>
    /// <param name="content">Textual content of the message.</param>
    /// <param name="type">The type of the message, which can be informational, warning, or error.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">The source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>The created <see cref="Message"/> instance.</returns>
    public static Message Create(string content, MessageType type = MessageType.BusinessError, string? code = null, string? source = null)
    {
        ArgumentNullException.ThrowIfNull(content);
        return new Message(content, type, code, source);
    }

    #endregion
}
