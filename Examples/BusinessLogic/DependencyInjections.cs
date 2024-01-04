using BusinessBaseLogic;
using Mdk.DIAttributes;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic;

public static class DependencyInjections
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        => services
            .RegisterByAttributes<BusinessLogicServices>()
            .AddBusinessBaseLogicServices();
}

internal sealed class BusinessLogicServices { }