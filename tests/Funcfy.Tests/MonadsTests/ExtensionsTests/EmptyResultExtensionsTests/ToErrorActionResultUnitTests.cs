using Funcfy.Monads;
using Funcfy.Monads.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.EmptyResultExtensionsTests;

public class ToErrorActionResultUnitTests
{
    [Fact]
    public void ToErrorActionResult_WhenCalledWithBusinessErrorResult_ShouldReturnUnprocessableEntityObjectResult()
    {
        // Arrange
        var result = Result.Create().WithBusinessError("This is a business error message");

        // Act
        var actionResult = result.ToErrorActionResult();

        // Assert
        actionResult.ShouldNotBeNull();
        actionResult.ShouldBeOfType<UnprocessableEntityObjectResult>();
        var objectResult = (UnprocessableEntityObjectResult)actionResult;
        objectResult.Value.ShouldNotBeNull();
        objectResult.Value.ShouldBe(result);
        objectResult.StatusCode.ShouldBe((int?)HttpStatusCode.UnprocessableEntity);
    }

    [Fact]
    public void ToErrorActionResult_WhenCalledWithBadRequestResult_ShouldReturnBadRequestObjectResult()
    {
        // Arrange
        var result = Result.Create().WithBadRequest("This is a bad request error message");

        // Act
        var actionResult = result.ToErrorActionResult();

        // Assert
        actionResult.ShouldNotBeNull();
        actionResult.ShouldBeOfType<BadRequestObjectResult>();
        var objectResult = (BadRequestObjectResult)actionResult;
        objectResult.Value.ShouldNotBeNull();
        objectResult.Value.ShouldBe(result);
        objectResult.StatusCode.ShouldBe((int?)HttpStatusCode.BadRequest);
    }

    [Fact]
    public void ToErrorActionResult_WhenCalledWithForbiddenResult_ShouldReturnForbiddenObjectResult()
    {
        // Arrange
        var result = Result.Create().WithForbidden("This is a forbidden error message");

        // Act
        var actionResult = result.ToErrorActionResult();

        // Assert
        actionResult.ShouldNotBeNull();
        actionResult.ShouldBeOfType<ObjectResult>();
        var objectResult = (ObjectResult)actionResult;
        objectResult.Value.ShouldNotBeNull();
        objectResult.Value.ShouldBe(result);
        objectResult.StatusCode.ShouldBe((int?)HttpStatusCode.Forbidden);
    }

    [Fact]
    public void ToErrorActionResult_WhenCalledWithUnauthorizedResult_ShouldReturnUnauthorizedObjectResult()
    {
        // Arrange
        var result = Result.Create().WithUnauthorized("This is an unauthorized error message");

        // Act
        var actionResult = result.ToErrorActionResult();

        // Assert
        actionResult.ShouldNotBeNull();
        actionResult.ShouldBeOfType<ObjectResult>();
        var objectResult = (ObjectResult)actionResult;
        objectResult.Value.ShouldNotBeNull();
        objectResult.Value.ShouldBe(result);
        objectResult.StatusCode.ShouldBe((int?)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public void ToErrorActionResult_WhenCalledWithNotFoundResult_ShouldReturnNotFoundObjectResult()
    {
        // Arrange
        var result = Result.Create().WithNotFound("This is a not found error message");

        // Act
        var actionResult = result.ToErrorActionResult();

        // Assert
        actionResult.ShouldNotBeNull();
        actionResult.ShouldBeOfType<NotFoundObjectResult>();
        var objectResult = (NotFoundObjectResult)actionResult;
        objectResult.Value.ShouldNotBeNull();
        objectResult.Value.ShouldBe(result);
        objectResult.StatusCode.ShouldBe((int?)HttpStatusCode.NotFound);
    }

    [Fact]
    public void ToErrorActionResult_WhenCalledWithConflictResult_ShouldReturnConflictObjectResult()
    {
        // Arrange
        var result = Result.Create().WithConflict("This is a conflict error message");

        // Act
        var actionResult = result.ToErrorActionResult();

        // Assert
        actionResult.ShouldNotBeNull();
        actionResult.ShouldBeOfType<ConflictObjectResult>();
        var objectResult = (ConflictObjectResult)actionResult;
        objectResult.Value.ShouldNotBeNull();
        objectResult.Value.ShouldBe(result);
        objectResult.StatusCode.ShouldBe((int?)HttpStatusCode.Conflict);
    }

    [Fact]
    public void ToErrorActionResult_WhenCalledWithServerErrorResult_ShouldReturnObjectResult()
    {
        // Arrange
        var result = Result.Create().WithServerError("This is a server error message");

        // Act
        var actionResult = result.ToErrorActionResult();

        // Assert
        actionResult.ShouldNotBeNull();
        actionResult.ShouldBeOfType<ObjectResult>();
        var objectResult = (ObjectResult)actionResult;
        objectResult.Value.ShouldNotBeNull();
        objectResult.Value.ShouldBe(result);
        objectResult.StatusCode.ShouldBe((int?)HttpStatusCode.InternalServerError);
    }

    [Fact]
    public void ToErrorActionResult_WhenCalledWithServerUnavailableResult_ShouldReturnObjectResult()
    {
        // Arrange
        var result = Result.Create().WithServiceUnavailable("This is a server unavailable error message");

        // Act
        var actionResult = result.ToErrorActionResult();

        // Assert
        actionResult.ShouldNotBeNull();
        actionResult.ShouldBeOfType<ObjectResult>();
        var objectResult = (ObjectResult)actionResult;
        objectResult.Value.ShouldNotBeNull();
        objectResult.Value.ShouldBe(result);
        objectResult.StatusCode.ShouldBe((int?)HttpStatusCode.ServiceUnavailable);
    }

    [Fact]
    public void ToErrorActionResult_WhenCalledWithNullResult_ShouldReturnObjectResultWithInternalServerError()
    {
        // Arrange
        Result? result = null!;

        // Act
        var actionResult = result.ToErrorActionResult();

        // Assert
        actionResult.ShouldNotBeNull();
        actionResult.ShouldBeOfType<StatusCodeResult>();
        var statusCodeResult = (StatusCodeResult)actionResult;
        statusCodeResult.StatusCode.ShouldBe((int)HttpStatusCode.InternalServerError);
    }
}
