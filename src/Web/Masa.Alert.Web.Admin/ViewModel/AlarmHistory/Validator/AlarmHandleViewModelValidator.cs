// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmHistory.Validator;

public class AlarmHandleViewModelValidator : AbstractValidator<AlarmHandleViewModel>
{
    public AlarmHandleViewModelValidator()
    {
        RuleFor(x => x.NotificationConfig).SetValidator(new NotificationConfigViewModelValidator()).When(x => x.IsHandleNotice);
    }
}