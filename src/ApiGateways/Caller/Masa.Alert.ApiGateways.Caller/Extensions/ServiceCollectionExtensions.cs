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

    public static IServiceCollection AddJwtTokenValidator(this IServiceCollection services,
        Action<JwtTokenValidatorOptions> jwtTokenValidatorOptions, Action<ClientRefreshTokenOptions> clientRefreshTokenOptions)
    {
        var options = new JwtTokenValidatorOptions();
        jwtTokenValidatorOptions.Invoke(options);
        services.AddSingleton(options);
        var refreshTokenOptions = new ClientRefreshTokenOptions();
        clientRefreshTokenOptions.Invoke(refreshTokenOptions);
        services.AddSingleton(refreshTokenOptions);
        services.AddScoped<JwtTokenValidator>();
        return services;
    }
}