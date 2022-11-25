// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Components;

public class DefaultSimpleSelect<TValue> : SSimpleSelect<TValue>
{
    [EditorRequired]
    [Parameter]
    public List<SelectItem<TValue>> Items { get; set; } = new();

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        ValueTexts = Items.Select(x => (x.Value, x.Text)).ToList();
    }
}