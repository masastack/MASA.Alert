﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Dtos;

public class AlarmRuleRecordDto : AuditEntityDto<Guid, Guid>
{
    public Guid AlarmRuleId { get; set; }

    public ConcurrentDictionary<string, long> AggregateResult { get; set; } = new();

    public bool IsTrigger { get; set; }

    public int ConsecutiveCount { get; set; }

    public List<RuleResultItemDto> RuleResultItems { get; set; } = new();

    public DateTimeOffset ExcuteTime { get; set; }
}
