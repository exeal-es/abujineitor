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
    
    [Fact]
    public void ReturnSameInstanceWhenRequestingTheSameServiceTwice()
    {
        injector.Register<TestService>();

        var firstInstance = injector.GetService<TestService>();
        var secondInstance = injector.GetService<TestService>();

        Assert.Same(firstInstance, secondInstance);
    }
    
    [Fact]
    public void CanResolveServiceWithADependency()
    {
        injector.Register<Dependency>();
        injector.Register<ServiceWithDependency>();

        var dependency = injector.GetService<Dependency>();
        var serviceWithDependency = injector.GetService<ServiceWithDependency>();

        Assert.IsType<ServiceWithDependency>(serviceWithDependency);
        Assert.Same(dependency, serviceWithDependency.Dependency);
    }
    
    [Fact]
    public void CanResolveServiceWithTwoDependencies()
    {
        injector.Register<Dependency>();
        injector.Register<AnotherDependency>();
        injector.Register<ServiceWithTwoDependencies>();

        var firstDependency = injector.GetService<Dependency>();
        var secondDependency = injector.GetService<AnotherDependency>();
        var serviceWithDependencies = injector.GetService<ServiceWithTwoDependencies>();

        Assert.IsType<ServiceWithTwoDependencies>(serviceWithDependencies);
        Assert.Same(firstDependency, serviceWithDependencies.FirstDependency);
        Assert.Same(secondDependency, serviceWithDependencies.SecondDependency);
    }
}

public class ServiceWithTwoDependencies
{
    public Dependency FirstDependency { get; }
    public AnotherDependency SecondDependency { get; }
    
    public ServiceWithTwoDependencies(Dependency firstDependency, AnotherDependency secondDependency)
    {
        FirstDependency = firstDependency;
        SecondDependency = secondDependency;
    }
}

public class AnotherDependency
{
}

public class ServiceWithDependency
{
    public Dependency Dependency { get; }
    
    public ServiceWithDependency(Dependency dependency)
    {
        Dependency = dependency;
    }
}

public class Dependency
{
}

public class AnotherTestService
{
}

public class TestService
{
}