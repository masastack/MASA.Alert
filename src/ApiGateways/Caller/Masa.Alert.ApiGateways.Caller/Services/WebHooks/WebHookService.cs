namespace Masa.Alert.ApiGateways.Caller.Services.WebHooks;

public class WebHookService : ServiceBase
{
    protected override string BaseUrl { get; set; }

    public WebHookService(ICaller caller) : base(caller)
    {
        BaseUrl = "api/v1/WebHooks";
    }

    public async Task<WebHookDto> GetAsync(Guid id)
    {
        return await GetAsync<WebHookDto>($"{id}");
    }

    public async Task<PaginatedListDto<WebHookDto>> GetListAsync(GetWebHookInputDto inputDto)
    {
        return await GetAsync<GetWebHookInputDto, PaginatedListDto<WebHookDto>>(string.Empty, inputDto) ?? new();
    }

    public async Task CreateAsync(WebHookUpsertDto inputDto)
    {
        await PostAsync(string.Empty, inputDto);
    }

    public async Task UpdateAsync(Guid id, WebHookUpsertDto inputDto)
    {
        await PutAsync($"{id}", inputDto);
    }

    public async Task DeleteAsync(Guid id)
    {
        await DeleteAsync($"{id}");
    }

    public async Task TestAsync(Guid id, WebHookTestDto inputDto)
    {
        await PutAsync($"{id}/test", inputDto);
    }
}
