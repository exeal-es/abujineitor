namespace Exeal.Abujineitor;

public class InjectorShould
{
    private readonly Injector injector;

    public InjectorShould()
    {
        injector = new Injector();
    }

    [Fact]
    public void ReturnRegisteredInstance()
    {
        var registeredInstance = new TestService();
        injector.Register(registeredInstance);

        var returnedInstance = injector.GetService<TestService>();

        Assert.Same(returnedInstance, registeredInstance);
    }
    
    [Fact]
    public void FailWhenAttemptingToGetNotRegisteredType()
    {
        Action action = () => injector.GetService<TestService>();

        Assert.Throws<InjectorException>(action);
    }
    
    [Fact]
    public void SupportMultipleTypes()
    {
        var testInstance = new TestService();
        var anotherTestInstance = new AnotherTestService();
        injector.Register(testInstance);
        injector.Register(anotherTestInstance);

        var returnedInstance = injector.GetService<TestService>();

        Assert.Same(returnedInstance, testInstance);
    }
    
    [Fact]
    public void RegisterAType()
    {
        injector.Register<TestService>();

        var returnedInstance = injector.GetService<TestService>();

        Assert.IsType<TestService>(returnedInstance);
    }
}

public class AnotherTestService
{
}

public class TestService
{
}