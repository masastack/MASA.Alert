// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules.Validator;

public class CheckFrequencyViewModelValidator : AbstractValidator<CheckFrequencyViewModel>
{
    public CheckFrequencyViewModelValidator()
    {
        RuleFor(x => x.Type).Required();
        RuleFor(x => x.CronExpression).Required().Must(x => CronExpression.IsValidExpression(x)).When(x => x.Type == AlarmCheckFrequencyTypes.Cron);
        RuleFor(x => x.FixedInterval).SetValidator(new TimeIntervalViewModelValidator()).When(x => x.Type == AlarmCheckFrequencyTypes.FixedInterval);
    }
}