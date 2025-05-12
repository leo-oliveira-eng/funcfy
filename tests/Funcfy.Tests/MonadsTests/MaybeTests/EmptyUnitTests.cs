using Funcfy.Monads;

namespace Funcfy.Tests.MonadsTests.MaybeTests;

public class EmptyUnitTests
{
    [Fact]
    public void Empty_ShouldReturnInstanceWithNoValue()
    {
        // Act
        var response = Maybe<int?>.Empty();

        // Assert
        response.IsEmpty.ShouldBeTrue();
        response.Value.ShouldBeNull();
    }
}
