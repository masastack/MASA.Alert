// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules.Validator;

public class TimeIntervalViewModelValidator : AbstractValidator<TimeIntervalViewModel>
{
    public TimeIntervalViewModelValidator(I18n i18n)
    {
        var scope = "AlarmRuleBlock";
        RuleFor(x => x.IntervalTimeType).IsInEnum().WithMessage(string.Format(i18n.T("IsInEnumValidator"), i18n.T(scope, "IntervalTimeType")));
        RuleFor(x => x.IntervalTime).InclusiveBetween(1, 59).WithMessage(string.Format(i18n.T("InclusiveBetweenValidator"), i18n.T(scope, "IntervalTime"), 1, 59))
            .When(x => x.IntervalTimeType == TimeTypes.Minute);
        RuleFor(x => x.IntervalTime).InclusiveBetween(1, 23).WithMessage(string.Format(i18n.T("InclusiveBetweenValidator"), i18n.T(scope, "IntervalTime"), 1, 23))
            .When(x => x.IntervalTimeType == TimeTypes.Hour);
        RuleFor(x => x.IntervalTime).InclusiveBetween(1, 31).WithMessage(string.Format(i18n.T("InclusiveBetweenValidator"), i18n.T(scope, "IntervalTime"), 1, 31))
            .When(x => x.IntervalTimeType == TimeTypes.Day);
    }
}
