// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmHistory.Validator;

public class NotificationConfigViewModelValidator : AbstractValidator<NotificationConfigViewModel>
{
    public NotificationConfigViewModelValidator(I18n i18n)
    {
        RuleFor(x => x.TemplateName).Required(string.Format(i18n.T("RequiredValidator"), i18n.T("MessageTemplate")));
        RuleFor(x => x.Receivers).Required(string.Format(i18n.T("RequiredValidator"), i18n.T("Receiver", true)));
    }
}