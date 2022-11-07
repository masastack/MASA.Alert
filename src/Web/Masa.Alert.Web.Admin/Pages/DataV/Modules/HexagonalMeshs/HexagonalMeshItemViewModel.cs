﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using Masa.Alert.Web.Admin.Pages.DataV.Modules.LinkTrackingTopologys;

namespace Masa.Alert.Web.Admin.Pages.DataV.Modules.HexagonalMeshs;

public class HexagonalMeshItemViewModel
{
    public string Name { get; set; } = default!;

    public LinkTrackingTopologyStatuses State { get; set; }
}
