// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmHistories.Queries;

public record GetAlarmHistoryQuery(Guid AlarmHistoryId) : Query<AlarmHistoryDto>
{
    public override AlarmHistoryDto Result { get; set; } = new();
}