// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Components.Modules.Subjects;

public partial class UserAutoComplete : AdminCompontentBase
{
    [Inject]
    public IAutoCompleteClient AutoCompleteClient { get; set; } = default!;

    [Inject]
    public IAuthClient AuthClient { get; set; } = default!;

    [Parameter]
    public List<Guid> Value { get; set; } = new();

    [Parameter]
    public EventCallback<List<Guid>> ValueChanged { get; set; }

    [Parameter]
    public string Label { get; set; } = "";

    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    public int Page { get; set; } = 1;

    [Parameter]
    public int PageSize { get; set; } = 10;

    bool _loading;

    protected List<UserSelectModel> Users { get; set; } = new();

    public string Search { get; set; } = "";

    protected override async Task OnParametersSetAsync()
    {
        await InitUsers();
        base.OnParametersSet();
    }

    private async Task InitUsers()
    {
        var list = await AuthClient.UserService.GetUsersAsync(Value.ToArray());
        Users = list.Select(x => new UserSelectModel(x.Id, x.Name, x.DisplayName, x.Account, x.PhoneNumber, x.Email, x.Avatar)).ToList();
        StateHasChanged();
    }

    private async Task Remove(UserSelectModel staff)
    {
        if (Readonly is false)
        {
            var value = new List<Guid>();
            value.AddRange(Value);
            value.Remove(staff.Id);
            await UpdateValueAsync(value);
        }
    }

    private async Task UpdateValueAsync(List<Guid> value)
    {
        if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(value);
        else Value = value;
    }

    private async Task QuerySelection(string search)
    {
        search = search.TrimStart(' ').TrimEnd(' ');
        Search = search;
        await Task.Delay(300);
        if (search != Search)
        {
            return;
        }

        _loading = true;
        var response = await AutoCompleteClient.GetBySpecifyDocumentAsync<UserSelectModel>(search, new AutoCompleteOptions
        {
            Page = Page,
            PageSize = PageSize,
        });

        var users = response.Data;
        Users = Users.UnionBy(users, user => user.Id).ToList();
        StateHasChanged();
        _loading = false;
    }
}
