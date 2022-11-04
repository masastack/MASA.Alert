// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.DataV.Modules.HexagonalMeshs;

public class HexagonalMeshViewModel
{
    public string Key { get; set; } = default!;

    public int Q { get; set; }

    public int R { get; set; }

    public string Name { get; set; } = default!;

    public LinkTrackingTopologyStatuses State { get; set; }

    public List<HexagonalMeshItemViewModel> Items { get; set; } = new();
}
