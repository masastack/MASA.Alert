namespace Masa.Alert.ApiGateways.Caller.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCallers(this IServiceCollection services, Action<AlertApiOptions>? configure = null)
    {
        var options = new AlertApiOptions("https://localhost:19701");
        configure?.Invoke(options);
        services.AddSingleton(options);

        var assemblies = new List<Assembly>
        {
            typeof(ServiceCollectionExtensions).Assembly
        };
        services.AddCaller(assemblies.ToArray());
        return services;
    }
}