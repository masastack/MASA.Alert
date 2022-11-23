// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.WebHooks.EventHandler;

public class PostWebHookEventHandler
{
    private readonly IWebHookRepository _repository;
    private IHttpClientFactory _httpClientFactory;
    private readonly ILogger<PostWebHookEventHandler> _logger;

    public PostWebHookEventHandler(IWebHookRepository repository, IHttpClientFactory httpClientFactor, ILogger<PostWebHookEventHandler> logger)
    {
        _repository = repository;
        _httpClientFactory = httpClientFactor;
        _logger = logger;
    }

    [EventHandler]
    public async Task HandleEventAsync(PostWebHookEvent eto)
    {
        var webHook = await _repository.FindAsync(x => x.Id == eto.WebHookId);
        Check.NotNull(webHook, "WebHook not found");

        var client = _httpClientFactory.CreateClient();
        var input = new StringContent(JsonSerializer.Serialize(new { eto.Handler }), Encoding.UTF8, "application/json");

        using var response = await client.PostAsync(webHook.Url, input);

        if (!response.IsSuccessStatusCode)
        {
            throw new UserFriendlyException(response.StatusCode.ToString());
        }

        try
        {
            var content = await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Post webHook fail");
            throw new UserFriendlyException(ex.Message);
        }
    }
}