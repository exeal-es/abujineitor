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
    
    [Fact]
    public void FailWhenAttemptingToGetNotRegisteredType()
    {
        // Arrange
        Injector injector = new Injector();

        // Act
        Action action = () => injector.GetService<TestService>();

        // Assert
        Assert.Throws<InjectorException>(action);
    }
}

public class TestService
{
}