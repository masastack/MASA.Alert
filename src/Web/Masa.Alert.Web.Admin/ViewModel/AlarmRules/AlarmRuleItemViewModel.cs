// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class AlarmRuleItemViewModel
{
    public string Expression { get; set; } = string.Empty;

    public AlertSeverity AlertSeverity { get; set; }

    public string MessageTemplateId { get; set; } = string.Empty;

    public string MessageTemplateName { get; set; } = string.Empty;

    public bool IsRecoveryNotification { get; set; }

    public bool IsNotification { get; set; }
}
