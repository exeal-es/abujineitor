namespace Exeal.Abujineitor;

public class InjectorShould
{
    [Fact]
    public void ReturnRegisteredInstance()
    {
        // Arrange
        Injector injector = new Injector();
        var registeredInstance = new TestService();
        injector.Register(registeredInstance);

        // Act
        var returnedInstance = injector.GetService<TestService>();

        // Assert
        Assert.Same(returnedInstance, registeredInstance);
    }
}

public class TestService
{
}