using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mdk.DIAttributes;

/// <inheritdoc />
public class AddSingleton : DIAttribute
{
    public AddSingleton() : base(ServiceLifetime.Singleton) { }

    public AddSingleton(Type serviceType) : base(ServiceLifetime.Singleton, serviceType) { }

    public AddSingleton(Type serviceType, Type implementationType) : base(ServiceLifetime.Singleton, serviceType, implementationType) { }
}

/// <inheritdoc />
public class AddSingleton<ServiceType>()
    : DIAttribute(ServiceLifetime.Singleton, typeof(ServiceType))
{ }

/// <inheritdoc />
public class AddSingleton<ServiceType, ImplementationType>()
    : DIAttribute(ServiceLifetime.Singleton, typeof(ServiceType), typeof(ImplementationType))
{ }
