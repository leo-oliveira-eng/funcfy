using System.ComponentModel;

namespace Funcfy.Monads.Enums;

/// <summary>
/// Represents the type of a message in the context of a result or operation.
/// </summary>
public enum MessageType : byte
{
    /// <summary>
    /// Represents no specific message type.
    /// </summary>
    None = 0,

    /// <summary>
    /// Represents an informational message.
    /// </summary>
    Info = 1,

    /// <summary>
    /// Represents a warning message.
    /// </summary>
    Warning = 2,

    /// <summary>
    /// Represents an error message. 
    /// Error messages of this type are typically used to indicate that the request was valid, 
    /// but the server encountered an issue that prevents it from fulfilling the request. 
    /// This could include business logic errors, validation failures, or other conditions that prevent the operation from being completed successfully.
    /// </summary>
    [Category("Error")]
    BusinessError = 3,

    /// <summary>
    /// Represents an error message that is related to the request, such as validation errors or bad requests.
    /// </summary>
    [Category("Error")]
    BadRequest = 4,

    /// <summary>
    /// Represents an error message indicating that the user is not authorized to perform the requested action.
    /// </summary>
    [Category("Error")]
    Unauthorized = 5,

    /// <summary>
    /// Represents an error message indicating that the user does not have permission to access the requested resource.
    /// </summary>
    [Category("Error")]
    Forbidden = 6,

    /// <summary>
    /// Represents an error message indicating that the requested resource was not found.
    /// </summary>
    [Category("Error")]
    NotFound = 7,

    /// <summary>
    /// Represents an error message indicating a conflict, such as when a resource already exists or when there is a version conflict.
    /// </summary>
    [Category("Error")]
    Conflict = 8,

    /// <summary>
    /// Represents an error message indicating that the server encountered an unexpected condition that prevented it from fulfilling the request.
    /// </summary>
    [Category("Error")]
    InternalServerError = 9,

    /// <summary>
    /// Represents an error message indicating that the server is currently unable to handle the request due to temporary overload or maintenance.
    /// </summary>
    [Category("Error")]
    ServiceUnavailable = 10
}
