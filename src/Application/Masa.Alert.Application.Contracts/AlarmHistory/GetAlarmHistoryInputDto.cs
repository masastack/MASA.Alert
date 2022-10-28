// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmHistory;

public class GetAlarmHistoryInputDto : PaginatedOptionsDto
{
    public string Filter { get; set; } = default!;
}
