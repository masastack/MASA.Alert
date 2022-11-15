// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Components.Modules.Alarm.Model;

public class AlarmPreviewChartDataItemModel
{
    public string Name { get; set; } = string.Empty;

    public List<List<long>> Data { get; set; } = new();
}
