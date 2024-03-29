﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Contracts.Admin.Tsc;

public class LogDto
{
    [JsonPropertyName("@timestamp")]
    public DateTime Timestamp { get; set; }

    public string? TraceId { get; set; }

    public string? SpanId { get; set; }

    public int TraceFlags { get; set; }

    public string? SeverityText { get; set; }

    public int SeverityNumber { get; set; }

    public object? Body { get; set; }

    public Dictionary<string, object>? Resource { get; set; }

    public Dictionary<string, object>? Attributes { get; set; }
}
