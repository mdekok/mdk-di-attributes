[![Build](https://github.com/mdekok/mdk-di-attributes/actions/workflows/Build.yml/badge.svg)](https://github.com/mdekok/mdk-di-attributes/actions/workflows/Build.yml)
[![Build, pack and publish](https://github.com/mdekok/mdk-di-attributes/actions/workflows/BuildPackPublish.yml/badge.svg)](https://github.com/mdekok/mdk-di-attributes/actions/workflows/BuildPackPublish.yml)
[![Nuget](https://img.shields.io/nuget/v/Mdk.DIAttributes?logo=nuget)](https://www.nuget.org/packages/Mdk.DIAttributes)

# Summary
The DIAttributes package is designed to help clean up your service registration code when using the default Dependency Injection (DI) container in .NET. It allows you to use custom attributes to register your services, keeping the DI metadata close to the implementation classes.
The package includes a reflection-based strategy to register services using these attributes. However, if you prefer a source generator strategy, you can use the [Mdk.DISourceGenerator](https://www.nuget.org/packages/Mdk.DISourceGenerator/) package.
- Installation: The package is available on NuGet as [Mdk.DIAttributes](https://www.nuget.org/packages/Mdk.DIAttributes/).
- Attribute Usage: The package provides attributes like AddScoped, AddSingleton, and AddTransient for different lifetimes. You can use these attributes on your classes to register them with the DI container. For example, ```[AddScoped] class MyClass { ... }``` is equivalent to ```services.AddScoped<MyClass>();```.
- Registration using Reflection: The attributes need to be translated to actual registrations in the DI container. This can be done using reflection. The reflection method involves iterating over assemblies, types, and attributes of the application domain. However, this method has some downsides.
- Registration using a Source Generator: The [Mdk.DISourceGenerator](https://www.nuget.org/packages/Mdk.DISourceGenerator/) package provides a source generator that translates the attributes to registration code for the default DI container. This solves the issues associated with the reflection strategy.
	
# DIAttributes
If you have a lot of services registered in the default DI container, your registration code can become some sort of a mess.

Using custom attributes can make your registration much cleaner.
Attributes with registration information keep DI metadata close to the implementation classes the attributes are assigned to.
This also cleans up the list of registrations in startup code.
All that is left is a method call for translating the attributes to actual registrations in the default DI container.

This package includes a reflection code strategy to register services using attributes.
If you want a source generator as a better alternative strategy,
go to: [Mdk.DISourceGenerator](https://www.nuget.org/packages/Mdk.DISourceGenerator/)

- [Installation](#installation)
- [Attribute usage](#attribute-usage)
- [Attribute to registration translation](#attribute-to-registration-translation)
- [References](#references)

## Installation
The custom attributes and registration extension methods are available as a NuGet package:
[Mdk.DIAttributes](https://www.nuget.org/packages/Mdk.DIAttributes/)

## Attribute usage
Following examples focus on scoped registration. Use AddSingleton or AddTransient for other lifetimes.

### Simple classes and interfaces
```
[AddScoped]
class MyClass { ... }
```
corresponds to ```services.AddScoped<MyClass>();```
```
[AddScoped<IMyInterface>]
class MyClass: IMyInterface { ... }
```
corresponds to ```services.AddScoped<IMyInterface, MyClass>();```

Generic attributes require C# 11. If you are still on a earlier version use ```[AddScoped(typeof(IMyInterface))]```

#### Multiple attributes on one class
```
[AddScoped<IMyInterface1>]
[AddScoped<IMyInterface2>]
class MyClass: IMyInterface1, IMyInterface2 { ... }
```
corresponds to
```
services.AddScoped<IMyInterface1, MyClass>();
services.AddScoped<IMyInterface2, MyClass>();
```

### Generic classes and interfaces
#### Unbound generic registration:
```
[AddScoped]
class MyClass<T> { ... }
```
corresponds to ```services.AddScoped(typeof(MyClass<>));```
```
[AddScoped(typeof(IMyInterface<>))]
class MyClass<T>: IMyInterface<T> { ... }
```
corresponds to ```services.AddScoped(typeof(IMyInterface<>), typeof(MyClass<>));```

#### Bound generic registration:
```
[AddScoped<MyClass<int>>]
class MyClass<T> { ... }
```
corresponds to ```services.AddScoped<MyClass<int>>();```
```
[AddScoped<IMyInterface<int>>]
class MyClass<T>: IMyInterface<T> { ... }
```
corresponds to ```services.AddScoped<IMyInterface<int>, MyClass<int>>();```

#### Multiple generic type parameters
Multiple generic type parameters are also supported, for example:
```
[AddScoped]
class MyClass<T, U> { ... }
```
corresponds to ```services.AddScoped(typeof(MyClass<,>));```

## Attribute to registration translation
The assigned attributes need to be translated to actual registrations in the default DI container.

### Using reflection
A common way to query for attributes at runtime is using reflection.

Following extension method iterates over assemblies, types and attributes of the application domain,
but this does NOT always work:

```
public static IServiceCollection RegisterByAttributes(this IServiceCollection services)
{
    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        foreach (Type type in assembly.GetTypes())
            foreach (DIAttribute attribute in type.GetCustomAttributes<DIAttribute>(false))
            {
                // Register service based on attribute found.
            }

    return services;
}
```
AppDomain.GetAssemblies gets the assemblies that have been loaded into the execution context of the application domain.
At startup possibly not all assemblies are loaded yet, so possibly not all attributes are found.
So unfortunately this method is not reliable for doing runtime registrations.
Loading all assemblies at startup is also not a good idea, because it can impact the startup time of your application.

A better solution is targeting just the assemblies we know of that contain the attributes we are looking for:
```
public static IServiceCollection RegisterByAttributes<T>(this IServiceCollection services)
{
    Assembly assembly = typeof(T).Assembly;

    foreach (Type type in assembly.GetTypes())
        foreach (DIAttribute attribute in type.GetCustomAttributes<DIAttribute>(false))
        {
            // Register service based on attribute found.
        }

    return services;
}
```
For every assembly containing attributes we need to call this extension method, where T is a type in the assembly.
T can be any type, but you could also create a empty class in the assembly just for this purpose:

```
public static class DependencyInjections
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        => services
            .RegisterByAttributes<BusinessLogicServices>()
            .AddBusinessBaseLogicServices();
}

internal sealed class BusinessLogicServices { }
```

The examples section of the [GitHub repository](https://github.com/mdekok/mdk-di-attributes) contains a Blazor application and a Minimal API project,
in which this registration strategy is implemented.

A solution using reflection is not ideal because:
- We moved from a compile time solution to a runtime solution that impacts startup.
- We still need some code for every assembly containing the custom attributes.
- Registration code is replaced by code using reflection, which makes it less direct.

### Using a source generator
Source generators are a good alternative for the reflection strategy.
All issues mentioned above are solved by using a source generator.

[Mdk.DISourceGenerator](https://github.com/mdekok/mdk-di-sourcegenerator) is a GitHub repository that contains a source generator,
that translates the attributes to registration code for the default DI container.
The source generator is also available as a NuGet package: [Mdk.DISourceGenerator on NuGet](https://www.nuget.org/packages/Mdk.DISourceGenerator/)

## References
- [Attributes](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/) by Microsoft
