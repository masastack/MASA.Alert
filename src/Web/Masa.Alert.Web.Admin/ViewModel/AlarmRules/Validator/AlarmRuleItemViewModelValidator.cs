// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules.Validator;

public class AlarmRuleItemViewModelValidator : AbstractValidator<AlarmRuleItemViewModel>
{
    public AlarmRuleItemViewModelValidator(I18n i18n)
    {
        var scope = "AlarmRuleBlock";
        RuleFor(x => x.Expression).Required(string.Format(i18n.T("RequiredValidator"), i18n.T(scope, "Expression")));
        RuleFor(x => x.AlertSeverity).IsInEnum().WithMessage(string.Format(i18n.T("IsInEnumValidator"), i18n.T(scope, "AlertSeverity")));
    }
}