// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Components;

public class SelectItem<TValue>
{
    public TValue Value { get; set; }

    public string Text { get; set; }

    public SelectItem(TValue value, string text)
    {
        Value = value;
        Text = text;
    }
}