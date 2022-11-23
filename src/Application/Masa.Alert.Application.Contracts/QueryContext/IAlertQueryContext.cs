namespace Masa.Alert.Application.Contracts.QueryContext;

public interface IAlertQueryContext
{
    public IQueryable<AlarmRuleQueryModel> AlarmRuleQueries { get; }

    public IQueryable<AlarmRuleRecordQueryModel> AlarmRuleRecordQueries { get; }

    public IQueryable<AlarmHistoryQueryModel> AlarmHistoryQueries { get; }

    public IQueryable<WebHookQueryModel> WebHookQueries { get; }
}
