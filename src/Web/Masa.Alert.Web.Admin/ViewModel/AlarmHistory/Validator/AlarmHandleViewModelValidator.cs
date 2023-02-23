// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmHistory.Validator;

public class AlarmHandleViewModelValidator : AbstractValidator<AlarmHandleViewModel>
{
    public AlarmHandleViewModelValidator(I18n i18n)
    {
        RuleFor(x => x.NotificationConfig).SetValidator(new NotificationConfigViewModelValidator(i18n)).When(x => x.IsHandleNotice);
    }
}