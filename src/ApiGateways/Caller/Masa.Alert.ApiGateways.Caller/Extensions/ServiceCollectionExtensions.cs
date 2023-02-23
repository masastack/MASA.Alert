namespace Masa.Alert.ApiGateways.Caller.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCallers(this IServiceCollection services)
    {
        var assemblies = new List<Assembly>
        {
            typeof(ServiceCollectionExtensions).Assembly
        };
        services.AddAutoRegistrationCaller(assemblies.ToArray());
        return services;
    }
}