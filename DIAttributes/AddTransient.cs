using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mdk.DIAttributes;

/// <inheritdoc />
public class AddTransient : DIAttribute
{
    public AddTransient() : base(ServiceLifetime.Transient) { }

    public AddTransient(Type serviceType) : base(ServiceLifetime.Transient, serviceType) { }

    public AddTransient(Type serviceType, Type implementationType) : base(ServiceLifetime.Transient, serviceType, implementationType) { }
}

/// <inheritdoc />
public class AddTransient<ServiceType> : DIAttribute
{
    public AddTransient() : base(ServiceLifetime.Transient, typeof(ServiceType)) { }
}

/// <inheritdoc />
public class AddTransient<ServiceType, ImplementationType> : DIAttribute
{
    public AddTransient() : base(ServiceLifetime.Transient, typeof(ServiceType), typeof(ImplementationType)) { }
}