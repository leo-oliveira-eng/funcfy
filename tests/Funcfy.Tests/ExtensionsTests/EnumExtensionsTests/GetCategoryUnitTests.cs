using Funcfy.Extensions;
using Funcfy.Monads.Enums;

namespace Funcfy.Tests.ExtensionsTests.EnumExtensionsTests;

public class GetCategoryUnitTests
{
    [Fact]
    public void GetCategory_WhenCalledWithNull_ThrowsArgumentNullException()
    {
        // Arrange  
        Enum? value = null;

        // Act & Assert  
        Assert.Throws<ArgumentNullException>(() => value!.GetCategory());
    }

    [Fact]
    public void GetCategory_WhenCalledWithValidEnum_ReturnsCategory()
    {
        // Arrange  
        var value = MessageType.BusinessError;

        // Act  
        var category = value.GetCategory();

        // Assert  
        category.ShouldBe("Error");
    }

    [Fact]
    public void GetCategory_WhenCalledWithEnumWithoutCategory_ReturnsEmptyString()
    {
        // Arrange  
        var value = MessageType.Info;

        // Act  
        var category = value.GetCategory();

        // Assert  
        category.ShouldBe(string.Empty);
    }

    [Fact]
    public void GetCategory_WhenCalledWithEnumWithoutCategoryAttribute_ReturnsEmptyString()
    {
        // Arrange  
        var value = unchecked((MessageType)999);

        // Act  
        var category = value.GetCategory();

        // Assert  
        category.ShouldBe(string.Empty);
    }
}
