// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules.Validator;

public class AlarmRuleItemViewModelValidator : AbstractValidator<AlarmRuleItemViewModel>
{
    public AlarmRuleItemViewModelValidator()
    {
        RuleFor(x => x.Expression).Required();
        RuleFor(x => x.AlertSeverity).IsInEnum();
    }
}