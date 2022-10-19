namespace Masa.Alert.ApiGateways.Caller;

public class AlertCaller : BaseDaprCaller
{
    TokenProvider _tokenProvider;
    protected override string AppId { get; set; } = App.APP;

    public override string? Name { get; set; } = nameof(AlertCaller);

    public AlertCaller(IServiceProvider serviceProvider
        , TokenProvider tokenProvider)
        : base(serviceProvider)
    {
        _tokenProvider = tokenProvider;
    }

    protected override void ConfigHttpRequestMessage(HttpRequestMessage requestMessage)
    {
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenProvider.AccessToken);
        base.ConfigHttpRequestMessage(requestMessage);
    }
}
