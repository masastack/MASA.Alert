﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmHistories.Commands;

public record ChangeHandlerAlarmHistoryCommand(Guid AlarmHistoryId, Guid Handler) : Command
{
}
