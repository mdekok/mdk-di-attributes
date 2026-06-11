using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mdk.DIAttributes;

/// <inheritdoc />
public class AddKeyedTransient : DIAttribute
{
    public AddKeyedTransient(string key)
        : base(ServiceLifetime.Transient, key: key) { }

    public AddKeyedTransient(Type serviceType, string key)
        : base(ServiceLifetime.Transient, serviceType, key: key) { }

    public AddKeyedTransient(Type serviceType, Type implementationType, string key)
        : base(ServiceLifetime.Transient, serviceType, implementationType, key) { }
}

/// <inheritdoc />
public class AddKeyedTransient<ServiceType>(string key)
    : DIAttribute(ServiceLifetime.Transient, typeof(ServiceType), key: key)
{ }

/// <inheritdoc />
public class AddKeyedTransient<ServiceType, ImplementationType>(string key)
    : DIAttribute(ServiceLifetime.Transient, typeof(ServiceType), typeof(ImplementationType), key)
{ }
