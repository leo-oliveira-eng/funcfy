using Funcfy.Monads.Enums;

namespace Funcfy.Monads.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="Result{TValue}"/> class to facilitate message handling.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Merges messages from the origin <see cref="Result"/> into the target <see cref="Result{TValue}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that may be present in the result.</typeparam>
    /// <param name="target">Result to which messages will be added.</param>
    /// <param name="origin">The origin <see cref="Result"/> from which messages will be merged.</param>
    /// <returns>
    /// Target <see cref="Result{TValue}"/> with messages from the origin <see cref="Result"/> added to it.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="target"/> or <paramref name="origin"/> is null.
    /// </exception>
    public static Result<TValue> MergeMessagesFrom<TValue>(this Result<TValue> target, Result origin)
    {
        ArgumentNullException.ThrowIfNull(target, nameof(target));
        ArgumentNullException.ThrowIfNull(origin, nameof(origin));

        foreach (var message in origin.Messages)
        {
            target.AddMessage(message);
        }

        return target;
    }

    /// <summary>
    /// Adds an informational message to the result.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that may be present in the result.</typeparam>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the informational message added to its messages collection.</returns>
    public static Result<TValue> WithInformation<TValue>(this Result<TValue> result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.Info, code, source));

    /// <summary>
    /// Adds a warning message to the result.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that may be present in the result.</typeparam>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the warning message added to its messages collection.</returns>
    public static Result<TValue> WithWarning<TValue>(this Result<TValue> result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.Warning, code, source));

    /// <summary>
    /// Adds a business error message to the result.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that may be present in the result.</typeparam>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the business error message added to its messages collection.</returns>
    public static Result<TValue> WithBusinessError<TValue>(this Result<TValue> result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.BusinessError, code, source));

    /// <summary>
    /// Adds a bad request message to the result.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that may be present in the result.</typeparam>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the bad request message added to its messages collection.</returns>
    public static Result<TValue> WithBadRequest<TValue>(this Result<TValue> result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.BadRequest, code, source));

    /// <summary>
    /// Adds an unauthorized message to the result.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that may be present in the result.</typeparam>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the unauthorized message added to its messages collection.</returns>
    public static Result<TValue> WithUnauthorized<TValue>(this Result<TValue> result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.Unauthorized, code, source));

    /// <summary>
    /// Adds a forbidden message to the result.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that may be present in the result.</typeparam>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the forbidden message added to its messages collection.</returns>
    public static Result<TValue> WithForbidden<TValue>(this Result<TValue> result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.Forbidden, code, source));

    /// <summary>
    /// Adds a not found message to the result.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that may be present in the result.</typeparam>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>The same <see cref="Result{TValue}"/> with the not found message added to its messages collection.</returns>
    public static Result<TValue> WithNotFound<TValue>(this Result<TValue> result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.NotFound, code, source));

    /// <summary>
    /// Adds a conflict message to the result.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that may be present in the result.</typeparam>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>The same <see cref="Result{TValue}"/> with the conflict message added to its messages collection.</returns>
    public static Result<TValue> WithConflict<TValue>(this Result<TValue> result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.Conflict, code, source));

    /// <summary>
    /// Adds a server error message to the result.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that may be present in the result.</typeparam>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the server error message added to its messages collection.</returns>
    public static Result<TValue> WithServerError<TValue>(this Result<TValue> result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.ServerError, code, source));

    /// <summary>
    /// Adds a service unavailable message to the result.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that may be present in the result.</typeparam>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the service unavailable message added to its messages collection.</returns>
    public static Result<TValue> WithServiceUnavailable<TValue>(this Result<TValue> result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.ServiceUnavailable, code, source));
}
