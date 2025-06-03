using Funcfy.Monads;
using Funcfy.Monads.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Funcfy.Tests.MonadsTests.ExtensionsTests.EmptyResultExtensionsTests;

public class ToActionResultUnitTests
{
    [Fact]
    public void ToActionResult_WhenCalledWithAnFailedResult_ShouldReturnAnActionResultWithError()
    {
        // Arrange
        var result = Result.Create().WithBadRequest("Any error message");

        // Act
        var actionResult = result.ToActionResult();

        // Assert
        actionResult.ShouldNotBeNull();
        actionResult.ShouldBeAssignableTo<BadRequestObjectResult>();
        ((BadRequestObjectResult)actionResult).StatusCode.ShouldBe(400);
        ((BadRequestObjectResult)actionResult).Value.ShouldBe(result);
    }

    [Fact]
    public void ToActionResult_WhenCalledWithASuccessfulResult_ShouldReturnAnActionResultWithSuccess()
    {
        // Arrange
        var result = Result.Create().WithInformation("Anything");

        // Act
        var actionResult = result.ToActionResult();

        // Assert
        actionResult.ShouldNotBeNull();
        actionResult.ShouldBeAssignableTo<OkObjectResult>();
        ((OkObjectResult)actionResult).StatusCode.ShouldBe(200);
        ((OkObjectResult)actionResult).Value.ShouldBe(result);
    }

    [Fact]
    public void ToActionResult_WhenCalledWithAnEmptyResult_ShouldReturnAnActionResultWithNoContent()
    {
        // Arrange
        var result = Result.Create();

        // Act
        var actionResult = result.ToActionResult();

        // Assert
        actionResult.ShouldNotBeNull();
        actionResult.ShouldBeAssignableTo<NoContentResult>();
        ((NoContentResult)actionResult).StatusCode.ShouldBe(204);
    }
}
