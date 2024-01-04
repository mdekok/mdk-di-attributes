using BusinessLogic;
using Mdk.DIAttributes;

namespace MinimalApi;

public static class DependencyInjections
{
    public static IServiceCollection AddMinimalApiAppServices(this IServiceCollection services)
        => services
            .RegisterByAttributes<MinimalApiAppServices>()
            .AddBusinessLogicServices();
}

internal sealed class MinimalApiAppServices { }
