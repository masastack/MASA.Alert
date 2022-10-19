namespace Masa.Alert.ApiGateways.Caller.Extensions;

public class HeaderRequestMessage : IDaprRequestMessage
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HeaderRequestMessage(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<HttpRequestMessage> ProcessHttpRequestMessageAsync(HttpRequestMessage requestMessage)
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        }

        return await Task.FromResult(requestMessage);
    }
}