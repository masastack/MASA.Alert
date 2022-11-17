namespace Masa.Alert.Application.Contracts.QueryContext;

public interface IAlertQueryContext
{
    public IQueryable<AlarmRuleRecordQueryModel> AlarmRuleRecordQueries { get; }
}
