namespace Masa.Alert.ApiGateways.Caller;

public class AlertCaller : DaprCallerBase
{
    private AlarmRuleService? _alarmRuleService;
    private AlarmHistoryService? _alarmHistoryService;
    private AlarmRuleRecordService? _alarmRuleRecordService;
    private WebHookService? _webHookService;

    public AlarmRuleService AlarmRuleService => _alarmRuleService ??= new(Caller);
    public AlarmHistoryService AlarmHistoryService => _alarmHistoryService ??= new(Caller);
    public AlarmRuleRecordService AlarmRuleRecordService => _alarmRuleRecordService ??= new(Caller);
    public WebHookService WebHookService => _webHookService ??= new(Caller);
    protected override string AppId { get; set; } = App.APP;

    public AlertCaller(AlertApiOptions options)
    {
        AppId = options.AppId;
    }

    protected override void UseDaprPost(MasaDaprClientBuilder masaDaprClientBuilder)
    {
        masaDaprClientBuilder.UseAuthentication(serviceProvider => new AuthenticationService(
                serviceProvider.GetRequiredService<TokenProvider>(),
                serviceProvider.GetRequiredService<JwtTokenValidator>(),
                serviceProvider.GetRequiredService<IMultiEnvironmentContext>()
            ));
        base.UseDaprPost(masaDaprClientBuilder);
    }
}
