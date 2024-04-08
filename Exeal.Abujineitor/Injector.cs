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
        return instance as T;
    }
}