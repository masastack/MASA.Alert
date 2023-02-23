// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules.Validator;

public class CheckFrequencyViewModelValidator : AbstractValidator<CheckFrequencyViewModel>
{
    public CheckFrequencyViewModelValidator(I18n i18n)
    {
        RuleFor(x => x.Type).Required(string.Format(i18n.T("RequiredValidator"), i18n.T("CheckFrequency")));
        RuleFor(x => x.CronExpression).Required(string.Format(i18n.T("RequiredValidator"), i18n.T("CronExpression")))
            .Must(x => CronExpression.IsValidExpression(x)).WithMessage(i18n.T("InvalidCronExpression"))
            .When(x => x.Type == AlarmCheckFrequencyTypes.Cron);
        RuleFor(x => x.FixedInterval).SetValidator(new TimeIntervalViewModelValidator(i18n))
            .When(x => x.Type == AlarmCheckFrequencyTypes.FixedInterval);
    }
}