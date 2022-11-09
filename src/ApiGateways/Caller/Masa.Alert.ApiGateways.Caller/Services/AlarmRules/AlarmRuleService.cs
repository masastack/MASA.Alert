namespace Masa.Alert.ApiGateways.Caller.Services.AlarmRules;

public class AlarmRuleService : ServiceBase
{
    protected override string BaseUrl { get; set; }

    public AlarmRuleService(ICaller caller) : base(caller)
    {
        BaseUrl = "api/app";
    }

    public async Task<AlarmRuleDto> GetAsync(Guid id)
    {
        return await GetAsync<AlarmRuleDto>($"{id}");
    }

    public async Task<PaginatedListDto<AlarmRuleDto>> GetPageListAsync(GetAlarmRuleInputDto inputDto)
    {
        return await GetAsync<GetAlarmRuleInputDto, PaginatedListDto<AlarmRuleDto>>(string.Empty, inputDto) ?? new();
    }

    public async Task<IEnumerable<AlarmRuleDto>> GetListAsyncAsync(GetAlarmRuleInputDto inputDto)
    {
        return await GetAsync<GetAlarmRuleInputDto, IEnumerable<AlarmRuleDto>>(string.Empty, inputDto) ?? new List<AlarmRuleDto>();
    }

    public async Task CreateAsync(AlarmRuleUpsertDto inputDto)
    {
        await PostAsync(string.Empty, inputDto);
    }

    public async Task UpdateAsync(Guid id, AlarmRuleUpsertDto inputDto)
    {
        await PutAsync($"{id}", inputDto);
    }

    public async Task DeleteAsync(Guid id)
    {
        await DeleteAsync($"{id}");
    }
}
