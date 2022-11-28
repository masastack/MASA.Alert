namespace Masa.Alert.ApiGateways.Caller;

public class AlertCaller : HttpClientCallerBase
{
    TokenProvider _tokenProvider;

    //protected override string AppId { get; set; } = App.APP;
    protected override string BaseAddress { get; set; }

    public override string? Name { get; set; } = nameof(AlertCaller);

    private AlarmRuleService? _alarmRuleService;
    private AlarmHistoryService? _alarmHistoryService;
    private AlarmRuleRecordService? _alarmRuleRecordService;
    private WebHookService? _webHookService;

    public AlarmRuleService AlarmRuleService => _alarmRuleService ??= new(Caller);
    public AlarmHistoryService AlarmHistoryService => _alarmHistoryService ??= new(Caller);
    public AlarmRuleRecordService AlarmRuleRecordService => _alarmRuleRecordService ??= new(Caller);
    public WebHookService WebHookService => _webHookService ??= new(Caller);

    public AlertCaller(IServiceProvider serviceProvider
        , TokenProvider tokenProvider
        , AlertApiOptions options)
        : base(serviceProvider)
    {
        _tokenProvider = tokenProvider;
        BaseAddress = options.ServiceBaseAddress;
    }

    protected override async Task ConfigHttpRequestMessageAsync(HttpRequestMessage requestMessage)
    {
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenProvider.AccessToken);
        await base.ConfigHttpRequestMessageAsync(requestMessage);
    }
}
