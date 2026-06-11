using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mdk.DIAttributes;

/// <inheritdoc />
public class AddKeyedScoped : DIAttribute
{
    public AddKeyedScoped(string key)
        : base(ServiceLifetime.Scoped, key: key) { }

    public AddKeyedScoped(Type serviceType, string key)
        : base(ServiceLifetime.Scoped, serviceType, key: key) { }

    public AddKeyedScoped(Type serviceType, Type implementationType, string key)
        : base(ServiceLifetime.Scoped, serviceType, implementationType, key) { }
}

/// <inheritdoc />
public class AddKeyedScoped<ServiceType>(string key)
    : DIAttribute(ServiceLifetime.Scoped, typeof(ServiceType), key: key)
{ }

/// <inheritdoc />
public class AddKeyedScoped<ServiceType, ImplementationType>(string key)
    : DIAttribute(ServiceLifetime.Scoped, typeof(ServiceType), typeof(ImplementationType), key)
{ }
