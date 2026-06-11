using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mdk.DIAttributes;

/// <inheritdoc />
public class AddKeyedSingleton : DIAttribute
{
    public AddKeyedSingleton(string key) : base(ServiceLifetime.Singleton, key: key) { }

    public AddKeyedSingleton(Type serviceType, string key) : base(ServiceLifetime.Singleton, serviceType, key: key) { }

    public AddKeyedSingleton(Type serviceType, Type implementationType, string key) : base(ServiceLifetime.Singleton, serviceType, implementationType, key) { }
}

/// <inheritdoc />
public class AddKeyedSingleton<ServiceType>(string key)
    : DIAttribute(ServiceLifetime.Singleton, typeof(ServiceType), key: key)
{ }

/// <inheritdoc />
public class AddKeyedSingleton<ServiceType, ImplementationType>(string key)
    : DIAttribute(ServiceLifetime.Singleton, typeof(ServiceType), typeof(ImplementationType), key)
{ }
