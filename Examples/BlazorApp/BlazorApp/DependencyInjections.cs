using BusinessLogic;
using Mdk.DIAttributes;

namespace BlazorApp;

public static class DependencyInjections
{
    public static IServiceCollection AddBlazorAppServices(this IServiceCollection services)
        => services
            .RegisterByAttributes<BlazorAppServices>()
            .AddBusinessLogicServices();
}

internal sealed class BlazorAppServices { }
