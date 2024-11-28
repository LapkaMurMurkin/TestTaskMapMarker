using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Register<T>(T service)
    {
        _services[typeof(T)] = service;
    }

    public static T Get<T>()
    {
        if (_services.ContainsKey(typeof(T)))
            return (T)_services[typeof(T)];

        Debug.LogError($"Type \"{typeof(T)}\" is not registered");
        return default(T);
    }
}
