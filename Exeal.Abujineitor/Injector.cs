namespace Exeal.Abujineitor;

public class Injector
{
    private Dictionary<Type, object> instances = new();

    public void Register<T>(T instance) where T : class
    {
        this.instances[typeof(T)] = instance;
    }

    public T GetService<T>() where T : class
    {
        var type = typeof(T);
        if (!instances.ContainsKey(type))
        {
            throw new InjectorException();
        }

        return this.instances[type] as T;
    }
}