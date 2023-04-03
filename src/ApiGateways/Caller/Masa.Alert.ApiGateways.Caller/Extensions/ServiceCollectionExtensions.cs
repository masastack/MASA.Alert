namespace Masa.Alert.ApiGateways.Caller.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAlertApiGateways(this IServiceCollection services, Action<AlertApiOptions> configure)
    {
        services.AddSingleton<IResponseMessage, AlertResponseMessage>();
        var options = new AlertApiOptions();
        configure.Invoke(options);
        services.AddSingleton(options);
        services.AddStackCaller(Assembly.Load("Masa.Alert.ApiGateways.Caller"), jwtTokenValidatorOptions =>
        {
            jwtTokenValidatorOptions.AuthorityEndpoint = options.AuthorityEndpoint;
        }, clientRefreshTokenOptions =>
        {
            clientRefreshTokenOptions.ClientId = options.ClientId;
            clientRefreshTokenOptions.ClientSecret = options.ClientSecret;
        });
        return services;
    }
}