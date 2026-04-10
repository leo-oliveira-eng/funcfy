using Funcfy.Monads;
using Funcfy.Monads.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.EmptyResultExtensionsTests;

public class ToActionResultIntegrationTests
{
    public static TheoryData<Func<Result>, int, string> ErrorResults =>
        new()
        {
            { () => Result.Create().WithBusinessError("Business rule failed"), StatusCodes.Status422UnprocessableEntity, "Business rule failed" },
            { () => Result.Create().WithBadRequest("Invalid request"), StatusCodes.Status400BadRequest, "Invalid request" },
            { () => Result.Create().WithUnauthorized("Authentication required"), StatusCodes.Status401Unauthorized, "Authentication required" },
            { () => Result.Create().WithForbidden("Access denied"), StatusCodes.Status403Forbidden, "Access denied" },
            { () => Result.Create().WithNotFound("Entity not found"), StatusCodes.Status404NotFound, "Entity not found" },
            { () => Result.Create().WithConflict("State conflict"), StatusCodes.Status409Conflict, "State conflict" },
            { () => Result.Create().WithServerError("Unexpected failure"), StatusCodes.Status500InternalServerError, "Unexpected failure" },
            { () => Result.Create().WithServiceUnavailable("Dependency unavailable"), StatusCodes.Status503ServiceUnavailable, "Dependency unavailable" }
        };

    [Fact]
    public async Task ToActionResult_WhenSuccessfulResultHasMessages_ShouldExecuteAsOkWithSerializedBody()
    {
        // Arrange
        var result = Result.Success().WithInformation("Customer created", code: "CUSTOMER_CREATED");

        // Act
        var (statusCode, responseBody) = await ExecuteAsync(result.ToActionResult());

        // Assert
        statusCode.ShouldBe(StatusCodes.Status200OK);
        responseBody.ShouldContain("Customer created");
        responseBody.ShouldContain("CUSTOMER_CREATED");
    }

    [Fact]
    public async Task ToActionResult_WhenSuccessfulResultHasNoMessages_ShouldExecuteAsNoContent()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var (statusCode, responseBody) = await ExecuteAsync(result.ToActionResult());

        // Assert
        statusCode.ShouldBe(StatusCodes.Status204NoContent);
        responseBody.ShouldBeEmpty();
    }

    [Theory]
    [MemberData(nameof(ErrorResults))]
    public async Task ToActionResult_WhenResultFailed_ShouldExecuteWithExpectedErrorStatusCodeAndSerializedBody(
        Func<Result> resultFactory,
        int expectedStatusCode,
        string expectedMessage)
    {
        // Arrange
        var result = resultFactory();

        // Act
        var (statusCode, responseBody) = await ExecuteAsync(result.ToActionResult());

        // Assert
        statusCode.ShouldBe(expectedStatusCode);
        responseBody.ShouldContain(expectedMessage);
    }

    private static async Task<(int StatusCode, string ResponseBody)> ExecuteAsync(IActionResult actionResult)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddControllers();

        using var serviceProvider = services.BuildServiceProvider();
        await using var responseBody = new MemoryStream();

        var httpContext = new DefaultHttpContext
        {
            RequestServices = serviceProvider,
            Response =
            {
                Body = responseBody
            }
        };

        var actionContext = new ActionContext(
            httpContext,
            new RouteData(),
            new ActionDescriptor()
        );

        await actionResult.ExecuteResultAsync(actionContext);

        responseBody.Position = 0;
        using var reader = new StreamReader(responseBody);
        return (httpContext.Response.StatusCode, await reader.ReadToEndAsync());
    }
}
