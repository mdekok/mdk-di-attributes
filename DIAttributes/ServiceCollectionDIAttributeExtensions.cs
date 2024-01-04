using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mdk.DIAttributes;

/// <summary>Extension methods on IServiceCollection to register services runtime through attributes.</summary>
public static class ServiceCollectionDIAttributeExtensions
{
    /// <summary>Registers services by searching for DIAttributes in the assembly of T.</summary>
    /// <param name="services">The services.</param>
    /// <returns>The services including possibly added registrations.</returns>
    public static IServiceCollection RegisterByAttributes<T>(this IServiceCollection services)
    {
        return services.RegisterByAttributes(typeof(T).Assembly);

        // Don't use Assembly.GetCallingAssembly() because GetCallingAssembly doesn't always return the expected assembly
        // See: https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly.getcallingassembly#remarks
    }

    /// <summary>Registers services by searching for DIAttributes in the given assembly.</summary>
    /// <param name="services">The services.</param>
    /// <param name="assembly">The assembly to search for DIAttributes.</param>
    /// <returns>The services including possibly added registrations.</returns>
    public static IServiceCollection RegisterByAttributes(this IServiceCollection services, Assembly assembly)
    {
        // Prevent duplicate processing.
        if (AssembliesProcessed.Contains(assembly))
            return services;

        foreach (Type type in assembly.GetTypes())
            foreach (DIAttribute attribute in type.GetCustomAttributes<DIAttribute>(false))
            {
                // The implementation type is explicitly set or if not, the same as the attributed type.
                Type implementationType = attribute.ImplementationType ?? type;
                // The service type is explicitly set or if not, the same as the implementation type.
                Type serviceType = attribute.ServiceType ?? implementationType;

                services.Add(new ServiceDescriptor(serviceType, implementationType, attribute.ServiceLifetime));
            }

        AssembliesProcessed.Add(assembly);

        return services;
    }

    public static readonly List<Assembly> AssembliesProcessed = [];

    /// <summary>Dumps metadate of registered services to console.</summary>
    /// <param name="services">The service collection.</param>
    /// <param name="serviceLifetime">The service lifetime to filter on if assigned.</param>
    public static void DumpRegistry(this IServiceCollection services, ServiceLifetime? serviceLifetime = null)
    {
        foreach (ServiceDescriptor serviceDescriptor in services
            .Where(descriptor => serviceLifetime is null || descriptor.Lifetime == serviceLifetime))
        {
            Console.WriteLine($"{serviceDescriptor.ImplementationType} implements {serviceDescriptor.ServiceType} ({serviceDescriptor.Lifetime})");
        }
    }
}