// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.Shared.AlarmHistory;

public enum AlarmHistoryHandleStatuses
{
    Pending = 1,
    InProcess,
    ProcessingCompleted,
    Notified
}