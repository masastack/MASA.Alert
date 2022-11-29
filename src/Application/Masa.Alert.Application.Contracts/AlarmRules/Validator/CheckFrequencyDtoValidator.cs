// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Validator;

public class CheckFrequencyDtoValidator : AbstractValidator<CheckFrequencyDto>
{
    public CheckFrequencyDtoValidator()
    {
        RuleFor(x => x.Type).Required();
        RuleFor(x => x.CronExpression).Required().Must(x => CronExpression.IsValidExpression(x)).When(x => x.Type == AlarmCheckFrequencyTypes.Cron);
        RuleFor(x => x.FixedInterval.IntervalTimeType).IsInEnum().When(x => x.Type == AlarmCheckFrequencyTypes.FixedInterval);
    }
}
