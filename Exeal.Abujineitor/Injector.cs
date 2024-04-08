namespace Exeal.Abujineitor;

public class Injector
{
    private object instance;

    public void Register<T>(T instance) where T : class
    {
        this.instance = instance;
    }

    public T GetService<T>() where T : class
    {
        if (instance == null)
            throw new InjectorException();
        
        return instance as T;
    }
}