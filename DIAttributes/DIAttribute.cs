using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mdk.DIAttributes;

/// <summary>Dependency Injection Attribute for registering service and implementation types including lifetime.</summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public abstract class DIAttribute : Attribute
{
    /// <summary>Initializes a new instance of the <see cref="DIAttribute" /> class.</summary>
    /// <param name="serviceLifetime">The service lifetime: Singleton, Scoped or Transient.</param>
    /// <param name="serviceType">Service type to register, if null the type of the class decorated by the attribute is implied.</param>
    /// <param name="implementationType">Implementation type, if null the type of the class decorated by the attribute is implied.</param>
    public DIAttribute(ServiceLifetime serviceLifetime, Type? serviceType = null, Type? implementationType = null)
    {
        ServiceLifetime = serviceLifetime;
        ServiceType = serviceType;
        ImplementationType = implementationType;
    }

    /// <summary>Gets the lifetime of the instances created.</summary>
    public ServiceLifetime ServiceLifetime { get; }

    /// <summary>Gets the service type registered.</summary>
    public Type? ServiceType { get; }

    /// <summary>Gets the implementation type registered.</summary>
    public Type? ImplementationType { get; }
}