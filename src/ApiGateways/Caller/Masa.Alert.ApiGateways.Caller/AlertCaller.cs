using Microsoft.AspNetCore.Builder.Extensions;

namespace Masa.Alert.ApiGateways.Caller;

public class AlertCaller : StackHttpClientCaller
{
    private const string DEFAULT_SCHEME = "Bearer";

    private AlarmRuleService? _alarmRuleService;
    private AlarmHistoryService? _alarmHistoryService;
    private AlarmRuleRecordService? _alarmRuleRecordService;
    private WebHookService? _webHookService;
    private JwtTokenValidator _jwtTokenValidator;
    AlertApiOptions _options;

    public AlarmRuleService AlarmRuleService => _alarmRuleService ??= new(Caller);
    public AlarmHistoryService AlarmHistoryService => _alarmHistoryService ??= new(Caller);
    public AlarmRuleRecordService AlarmRuleRecordService => _alarmRuleRecordService ??= new(Caller);
    public WebHookService WebHookService => _webHookService ??= new(Caller);
    protected override string BaseAddress { get; set; }

    public AlertCaller(AlertApiOptions options)
    {
        BaseAddress = options.AlertServiceBaseAddress;
        _options = options;
    }
}
