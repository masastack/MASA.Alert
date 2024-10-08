﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using Masa.BuildingBlocks.Authentication.Identity;
using Masa.BuildingBlocks.StackSdks.Auth.Contracts;

namespace Masa.Alert.Web.Admin;

public abstract class AdminCompontentBase : Masa.Blazor.Core.MasaComponentBase
{
    private GlobalConfig? _globalConfig;
    private AlertCaller? _alertCaller;
    private NavigationManager? _navigationManager;

    [Inject]
    public IPopupService PopupService { get; set; } = default!;

    [Inject]
    public I18n I18n { get; set; } = default!;

    [Inject]
    public GlobalConfig GlobalConfig
    {
        get
        {
            return _globalConfig ?? throw new Exception("please Inject GlobalConfig!");
        }
        set
        {
            _globalConfig = value;
        }
    }

    [Inject]
    public NavigationManager NavigationManager
    {
        get
        {
            return _navigationManager ?? throw new Exception("please Inject NavigationManager!");
        }
        set
        {
            _navigationManager = value;
        }
    }

    [Inject]
    public JsInitVariables JsInitVariables { get; set; } = default!;

    [CascadingParameter(Name = "Culture")]
    private string Culture { get; set; } = null!;

    [Inject]
    public AlertCaller AlertCaller
    {
        get
        {
            return _alertCaller ?? throw new Exception("please Inject AlertCaller!");
        }
        set
        {
            _alertCaller = value;
        }
    }

    [Inject]
    public MasaGlobalConfig MasaGlobalConfig { get; set; } = default!;

    [Inject]
    public IMultiEnvironmentUserContext MultiEnvironmentUserContext { get; set; } = default!;

    [Inject]
    public MasaUser MasaUser { get; set; } = null!;

    protected bool IsPage = false;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        MasaGlobalConfig.CurrentTeamId = MasaUser.CurrentTeamId;
    }

    public bool Loading
    {
        get => GlobalConfig.Loading;
        set => GlobalConfig.Loading = value;
    }

    public string LoadingText
    {
        get => GlobalConfig.LoadingText;
        set => GlobalConfig.LoadingText = value;
    }

    protected virtual string? PageName { get; set; }

    public string T(string key)
    {
        if (string.IsNullOrEmpty(key)) return key;
        if (PageName is not null) return I18n.T(PageName, key, false) ?? I18n.T(key, false) ?? key;
        else return I18n.T(key, true);
    }

    public string T(string formatkey, params string[] args)
    {
        return string.Format(T(formatkey), args);
    }

    public HubConnection? HubConnection { get; set; }

    public async Task UpsertSuccessfulMessage(Guid entityId, string displayName)
    {
        if (entityId == default)
        {
            await SuccessMessageAsync(T("CreateSuccessfulMessage", displayName));
        }
        else
        {
            await SuccessMessageAsync(T("EditSuccessfulMessage", displayName));
        }
    }

    public async Task ConfirmAsync(string messgae, Func<Task> callback, AlertTypes type = AlertTypes.Warning)
    {
        if (await PopupService.SimpleConfirmAsync(I18n.T("OperationConfirmation"), messgae, type)) await callback.Invoke();
    }

    public async Task SuccessMessageAsync(string message)
    {
        await PopupService.EnqueueSnackbarAsync(message, AlertTypes.Success);
    }

    public async Task WarningAsync(string message)
    {
        await PopupService.EnqueueSnackbarAsync(message, AlertTypes.Warning);
    }

    public async Task ErrorMessageAsync(string message)
    {
        await PopupService.EnqueueSnackbarAsync(message, AlertTypes.Error);
    }

    protected async Task HandleErrorAsync(Exception exception)
    {
        await InvokeAsync(async () =>
        {
            await ErrorMessageAsync(exception.Message);
            Loading = false;
            StateHasChanged();
        });
    }

    public List<KeyValuePair<string, TEnum>> GetEnumMap<TEnum>() where TEnum : struct, Enum
    {
        return Enum.GetValues<TEnum>().Select(e => new KeyValuePair<string, TEnum>(e.ToString(), e)).ToList();
    }

    public List<KeyValuePair<string, bool>> GetBooleanMap()
    {
        return new()
        {
            new(T("Enable"), true),
            new(T("Disabled"), false)
        };
    }

    public async Task Throttle(Func<Task> callback, int wait = 500, bool immediate = true)
    {
        if (immediate)
        {
            if (!GlobalConfig.ThrottleFlag)
            {
                GlobalConfig.ThrottleFlag = true;
                await callback.Invoke();
                await Task.Delay(wait);
                GlobalConfig.ThrottleFlag = false;
            }
        }
        else if (!GlobalConfig.ThrottleFlag)
        {
            GlobalConfig.ThrottleFlag = true;
            await Task.Delay(wait);
            GlobalConfig.ThrottleFlag = false;
            await callback.Invoke();
        }
    }
}
