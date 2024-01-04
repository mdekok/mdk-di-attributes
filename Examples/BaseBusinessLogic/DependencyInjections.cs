using Mdk.DIAttributes;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessBaseLogic;

public static class DependencyInjections
{
    public static IServiceCollection AddBusinessBaseLogicServices(this IServiceCollection services)
        => services.RegisterByAttributes<BusinessBaseLogicServices>();
}

internal sealed class BusinessBaseLogicServices { }