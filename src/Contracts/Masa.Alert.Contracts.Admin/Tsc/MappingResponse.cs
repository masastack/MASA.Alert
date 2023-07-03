// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Contracts.Admin.Tsc;

public class MappingResponse
{
    public string Name { get; set; } = string.Empty;

    public string DataType { get; set; } = string.Empty;

    public bool? IsKeyword { get; set; }

    public int? MaxLenth { get; set; }
}
