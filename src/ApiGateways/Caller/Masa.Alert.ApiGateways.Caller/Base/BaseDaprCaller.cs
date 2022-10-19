namespace Masa.Alert.ApiGateways.Caller.Base;

public abstract class BaseDaprCaller : DaprCallerBase
{
    protected BaseDaprCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

        if (webHostEnvironment.IsDevelopment())
        {
            var appIdSuffix = NetworkUtils.GetPhysicalAddress();

            AppId = $"{AppId}-{appIdSuffix}";
        }
        else if (!webHostEnvironment.IsProduction())
        {
            AppId = $"{AppId}-{webHostEnvironment.EnvironmentName.ToLower()}";
        }
    }

    protected override DefaultDaprClientBuilder UseDapr()
    {
        return base.UseDapr().AddHttpRequestMessage<HeaderRequestMessage>();
    }
}