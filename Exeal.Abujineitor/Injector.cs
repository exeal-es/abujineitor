namespace Exeal.Abujineitor;

public class Injector
{
    private Dictionary<Type, object> instances = new();

    public void Register<T>(T instance) where T : class
    {
        this.instances[typeof(T)] = instance;
    }

    private object GetService(Type type)
    {
        if (!instances.ContainsKey(type))
        {
            throw new InjectorException();
        }

        return this.instances[type];
    }

    public T GetService<T>() where T : class
    {
        return (T)GetService(typeof(T));
    }

    public void Register<T>() where T : class
    {
        var type = typeof(T);

        var constructorInfos = type.GetConstructors();
        if (constructorInfos.First().GetParameters().Length == 2)
        {
            var firstParameterInfo = constructorInfos.First().GetParameters().First();
            var secondParameterInfo = constructorInfos.First().GetParameters().Last();

            var firstParameterType = firstParameterInfo.ParameterType;
            var secondParameterType = secondParameterInfo.ParameterType;

            var firstDependency = GetService(firstParameterType);
            var secondDependency = GetService(secondParameterType);
            List<object> parameters = new List<object>();
            parameters.Add(firstDependency);
            parameters.Add(secondDependency);
            var instance = (T)Activator.CreateInstance(type, parameters.ToArray());
            Register(instance);
        }
        else if (constructorInfos.First().GetParameters().Length == 1)
        {
            var parameterInfo = constructorInfos.First().GetParameters().First();
            var parameterType = parameterInfo.ParameterType;
            var dependency = GetService(parameterType);
            var instance = (T)Activator.CreateInstance(type, new object[] { dependency });
            Register(instance);
        }
        else
        {
            var instance = Activator.CreateInstance<T>();
            Register(instance);
        }
    }
}