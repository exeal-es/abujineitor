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
    
    [Fact]
    public void SupportMultipleTypes()
    {
        // Arrange
        Injector injector = new Injector();
        var testInstance = new TestService();
        var anotherTestInstance = new AnotherTestService();
        injector.Register(testInstance);
        injector.Register(anotherTestInstance);

        // Act
        var returnedInstance = injector.GetService<TestService>();

        // Assert
        Assert.Same(returnedInstance, testInstance);
    }
}

public class AnotherTestService
{
}

public class TestService
{
}