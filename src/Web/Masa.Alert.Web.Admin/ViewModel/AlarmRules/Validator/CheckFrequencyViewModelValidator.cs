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
            .Must(x =>
            {
                var startTime = DateTimeOffset.UtcNow;
                var cronExpression = new CronExpression(x);
                var nextExcuteTime = cronExpression.GetNextValidTimeAfter(startTime);
                if (nextExcuteTime.HasValue == false)
                {
                    return false;
                }
                var nextExcuteTime2 = cronExpression.GetNextValidTimeAfter(nextExcuteTime.Value);
                if (nextExcuteTime2.HasValue && (nextExcuteTime2.Value - nextExcuteTime.Value).TotalSeconds < 30)
                    return false;
                return true;
            }).WithMessage(i18n.T("RunningIntervalTips"))
            .When(x => x.Type == AlarmCheckFrequencyTypes.Cron);
        RuleFor(x => x.FixedInterval).SetValidator(new TimeIntervalViewModelValidator(i18n))
            .When(x => x.Type == AlarmCheckFrequencyTypes.FixedInterval);
    }
}