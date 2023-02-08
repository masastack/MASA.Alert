// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmHistory.Validator;

public class NotificationConfigViewModelValidator : AbstractValidator<NotificationConfigViewModel>
{
    public NotificationConfigViewModelValidator()
    {
        RuleFor(x => x.TemplateName).Required();
        RuleFor(x => x.Receivers).Required();
    }
}