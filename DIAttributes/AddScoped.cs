using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mdk.DIAttributes;

/// <inheritdoc />
public class AddScoped : DIAttribute
{
    public AddScoped() : base(ServiceLifetime.Scoped) { }

    public AddScoped(Type serviceType) : base(ServiceLifetime.Scoped, serviceType) { }

    public AddScoped(Type serviceType, Type implementationType) : base(ServiceLifetime.Scoped, serviceType, implementationType) { }

}

/// <inheritdoc />
public class AddScoped<ServiceType> : DIAttribute
{
    public AddScoped() : base(ServiceLifetime.Scoped, typeof(ServiceType)) { }
}

/// <inheritdoc />
public class AddScoped<ServiceType, ImplementationType> : DIAttribute
{
    public AddScoped() : base(ServiceLifetime.Scoped, typeof(ServiceType), typeof(ImplementationType)) { }
}