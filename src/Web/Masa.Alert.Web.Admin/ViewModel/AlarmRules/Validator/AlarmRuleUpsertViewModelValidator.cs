// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules.Validator;

public class AlarmRuleUpsertViewModelValidator : AbstractValidator<AlarmRuleUpsertViewModel>
{
    public AlarmRuleUpsertViewModelValidator()
    {
        RuleFor(x => x.ProjectIdentity).Required().When(x => x.Type == AlarmRuleTypes.Log).When(x => x.Step == 1);
        RuleFor(x => x.AppIdentity).Required().When(x => x.Type == AlarmRuleTypes.Log).When(x => x.Step == 1);

        RuleFor(x => x.CheckFrequency).SetValidator(new CheckFrequencyViewModelValidator())
            .When(x => (x.Type == AlarmRuleTypes.Log && x.Step == 2) || (x.Type == AlarmRuleTypes.Metric && x.Step == 1));
        RuleFor(x => x.LogMonitorItems).Required()
            .Must(x => !x.Any(x => string.IsNullOrEmpty(x.Field)))
            .Must(x => !x.Any(x => string.IsNullOrEmpty(x.Alias)))
            .When(x => x.Type == AlarmRuleTypes.Log && x.Step == 2);
        RuleFor(x => x.MetricMonitorItems).Required()
            .Must(x => !x.Any(x => string.IsNullOrEmpty(x.Alias)))
            .When(x => x.Type == AlarmRuleTypes.Metric && x.Step == 1);

        RuleFor(x => x.DisplayName).Required().ChineseLetterNumberSymbol().Length(2, 50).When(x => (x.Type == AlarmRuleTypes.Log && x.Step == 3) || (x.Type == AlarmRuleTypes.Metric && x.Step == 2));
        RuleFor(x => x.Items).Required()
            .Must(x => !x.Any(x => x.AlertSeverity == default))
            .When(x => (x.Type == AlarmRuleTypes.Log && x.Step == 3) || (x.Type == AlarmRuleTypes.Metric && x.Step == 2));
        RuleFor(x => x.SilenceCycle).SetValidator(new SilenceCycleViewModelValidator()).When(x => (x.Type == AlarmRuleTypes.Log && x.Step == 3) || (x.Type == AlarmRuleTypes.Metric && x.Step == 2));
    }
}