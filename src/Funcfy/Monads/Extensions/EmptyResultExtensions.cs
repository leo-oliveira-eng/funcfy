using Funcfy.Monads.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Funcfy.Monads.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="Result"/> class to facilitate message handling.
/// </summary>
public static class EmptyResultExtensions
{
    /// <summary>
    /// Merges messages from the origin <see cref="Result"/> into the target <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="target">Result to which messages will be added.</param>
    /// <param name="origin">The origin <see cref="Result"/> from which messages will be merged.</param>
    /// <returns>
    /// Target <see cref="Result{TValue}"/> with messages from the origin <see cref="Result"/> added to it.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="target"/> or <paramref name="origin"/> is null.
    /// </exception>
    public static Result MergeMessagesFrom(this Result target, Result origin)
    {
        ArgumentNullException.ThrowIfNull(target, nameof(target));
        ArgumentNullException.ThrowIfNull(origin, nameof(origin));

        foreach (var message in origin.Messages)
            target.AddMessage(message);

        return target;
    }

    /// <summary>
    /// Adds an informational message to the result.
    /// </summary>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the informational message added to its messages collection.</returns>
    public static Result WithInformation(this Result result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.Info, code, source));

    /// <summary>
    /// Adds a warning message to the result.
    /// </summary>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the warning message added to its messages collection.</returns>
    public static Result WithWarning(this Result result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.Warning, code, source));

    /// <summary>
    /// Adds a business error message to the result.
    /// </summary>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the business error message added to its messages collection.</returns>
    public static Result WithBusinessError(this Result result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.BusinessError, code, source));

    /// <summary>
    /// Adds a bad request message to the result.
    /// </summary>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the bad request message added to its messages collection.</returns>
    public static Result WithBadRequest(this Result result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.BadRequest, code, source));

    /// <summary>
    /// Adds an unauthorized message to the result.
    /// </summary>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the unauthorized message added to its messages collection.</returns>
    public static Result WithUnauthorized(this Result result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.Unauthorized, code, source));

    /// <summary>
    /// Adds a forbidden message to the result.
    /// </summary>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the forbidden message added to its messages collection.</returns>
    public static Result WithForbidden(this Result result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.Forbidden, code, source));

    /// <summary>
    /// Adds a not found message to the result.
    /// </summary>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>The same <see cref="Result{TValue}"/> with the not found message added to its messages collection.</returns>
    public static Result WithNotFound(this Result result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.NotFound, code, source));

    /// <summary>
    /// Adds a conflict message to the result.
    /// </summary>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>The same <see cref="Result{TValue}"/> with the conflict message added to its messages collection.</returns>
    public static Result WithConflict(this Result result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.Conflict, code, source));

    /// <summary>
    /// Adds a server error message to the result.
    /// </summary>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the server error message added to its messages collection.</returns>
    public static Result WithServerError(this Result result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.ServerError, code, source));

    /// <summary>
    /// Adds a service unavailable message to the result.
    /// </summary>
    /// <param name="result">The <see cref="Result{TValue}"/> to which the message will be added.</param>
    /// <param name="content">The textual content of the message.</param>
    /// <param name="code">The code associated with the message, which can be used for categorization or identification of the message.</param>
    /// <param name="source">Identifies the source of the message, which can be used to identify where the message originated from, such as a specific module or component.</param>
    /// <returns>Same <see cref="Result{TValue}"/> with the service unavailable message added to its messages collection.</returns>
    public static Result WithServiceUnavailable(this Result result, string content, string? code = null, string? source = null)
        => result.AddMessage(Message.Create(content, MessageType.ServiceUnavailable, code, source));

    /// <summary>
    /// Converts a <see cref="Result"/> to an <see cref="IActionResult"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result"/> to convert.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
    public static IActionResult ToActionResult(this Result result)
    {
        if (result.Failed)
            return result.ToErrorActionResult();

        return result.Messages.Any()
            ? new OkObjectResult(result)
            : new NoContentResult();
    }

    /// <summary>
    /// Converts a <see cref="Result"/> to an <see cref="IActionResult"/> representing an error response based on the first message type.
    /// </summary>
    /// <param name="result">The <see cref="Result"/> to convert.</param>
    /// <returns>An <see cref="IActionResult"/> representing the error response.</returns>
    public static IActionResult ToErrorActionResult(this Result result)
        => result?.Messages[0].Type switch
        {
            MessageType.BusinessError => new UnprocessableEntityObjectResult(result),
            MessageType.BadRequest => new BadRequestObjectResult(result),
            MessageType.Forbidden => new ObjectResult(result) { StatusCode = (int?)HttpStatusCode.Forbidden },
            MessageType.NotFound => new NotFoundObjectResult(result),
            MessageType.Unauthorized => new ObjectResult(result) { StatusCode = (int?)HttpStatusCode.Unauthorized },
            MessageType.ServerError => new ObjectResult(result) { StatusCode = (int?)HttpStatusCode.InternalServerError },
            MessageType.ServiceUnavailable => new ObjectResult(result) { StatusCode = (int?)HttpStatusCode.ServiceUnavailable },
            MessageType.Conflict => new ConflictObjectResult(result),
            _ => new StatusCodeResult((int)HttpStatusCode.InternalServerError)
        };
}
