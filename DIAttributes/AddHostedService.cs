using System;

namespace Mdk.DIAttributes;

/// <summary>Dependency Injection Attribute for registering hosted service types.</summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class AddHostedService<HostedServiceType> : Attribute
{
    /// <summary>Gets the hosted service type registered.</summary>
    public Type? ServiceType { get; } = typeof(HostedServiceType);
}
