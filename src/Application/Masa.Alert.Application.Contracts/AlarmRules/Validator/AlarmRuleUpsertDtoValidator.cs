// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Validator;

public class AlarmRuleUpsertDtoValidator : AbstractValidator<AlarmRuleUpsertDto>
{
    public AlarmRuleUpsertDtoValidator()
    {
        RuleFor(x => x.DisplayName).Required().Length(2, 50);
        RuleFor(x => x.ProjectIdentity).Required().When(x => x.Type == AlarmRuleTypes.Log);
        RuleFor(x => x.AppIdentity).Required().When(x => x.Type == AlarmRuleTypes.Log);
        RuleFor(x => x.CheckFrequency).SetValidator(new CheckFrequencyDtoValidator());
        RuleFor(x => x.LogMonitorItems).Required().When(x => x.Type == AlarmRuleTypes.Log);
        RuleFor(x => x.MetricMonitorItems).Required().When(x => x.Type == AlarmRuleTypes.Metric);
        RuleFor(x => x.Items).Required();
    }
}