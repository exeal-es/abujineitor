﻿namespace Exeal.Abujineitor;

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
        var parameters = new List<object>();

        foreach (var parameterInfo in constructorInfos.First().GetParameters())
        {
            var parameterType = parameterInfo.ParameterType;
            var dependency = GetService(parameterType);
            parameters.Add(dependency);
        }
        
        var instance = (T)Activator.CreateInstance(type, parameters.ToArray());
        Register(instance);
    }
}