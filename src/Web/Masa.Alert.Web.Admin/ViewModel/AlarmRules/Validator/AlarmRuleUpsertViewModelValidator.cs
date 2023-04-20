// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules.Validator;

public class AlarmRuleUpsertViewModelValidator : AbstractValidator<AlarmRuleUpsertViewModel>
{
    public AlarmRuleUpsertViewModelValidator(I18n i18n)
    {
        var scope = "AlarmRuleBlock";
        RuleFor(x => x.ProjectIdentity).Required(string.Format(i18n.T("RequiredValidator"), i18n.T(scope, "ProjectIdentity")))
            .When(x => x.Type == AlarmRuleTypes.Log).When(x => x.Step == 1);
        RuleFor(x => x.AppIdentity).Required(string.Format(i18n.T("RequiredValidator"), i18n.T(scope, "AppIdentity")))
            .When(x => x.Type == AlarmRuleTypes.Log).When(x => x.Step == 1);

        RuleFor(x => x.CheckFrequency).SetValidator(new CheckFrequencyViewModelValidator(i18n))
            .When(x => (x.Type == AlarmRuleTypes.Log && x.Step == 2) || (x.Type == AlarmRuleTypes.Metric && x.Step == 1));
        RuleFor(x => x.LogMonitorItems).Required(string.Format(i18n.T("RequiredValidator"), i18n.T(scope, "VariableConfiguration")))
            .ForEach(x => x.SetValidator(new LogMonitorItemViewModelValidator(i18n)))
            .When(x => x.Type == AlarmRuleTypes.Log && x.Step == 2);
        RuleFor(x => x.MetricMonitorItems).Required(string.Format(i18n.T("RequiredValidator"), i18n.T(scope, "VariableConfiguration")))
            .ForEach(x => x.SetValidator(new MetricMonitorItemViewModelValidator(i18n)))
            .When(x => x.Type == AlarmRuleTypes.Metric && x.Step == 1);

        RuleFor(x => x.DisplayName).Required(string.Format(i18n.T("RequiredValidator"), i18n.T(scope, "DisplayName")))
            .Length(2, 50).WithMessage(string.Format(i18n.T("LengthValidator"), i18n.T(scope, "DisplayName"), 2, 50))
            .When(x => (x.Type == AlarmRuleTypes.Log && x.Step == 3) || (x.Type == AlarmRuleTypes.Metric && x.Step == 2));
        RuleFor(x => x.Items).Required(string.Format(i18n.T("RequiredValidator"), i18n.T(scope, "TriggerRules")))
            .ForEach(x => x.SetValidator(new AlarmRuleItemViewModelValidator(i18n)))
            .When(x => (x.Type == AlarmRuleTypes.Log && x.Step == 3) || (x.Type == AlarmRuleTypes.Metric && x.Step == 2));
        RuleFor(x => x.SilenceCycle).SetValidator(new SilenceCycleViewModelValidator(i18n)).When(x => (x.Type == AlarmRuleTypes.Log && x.Step == 3) || (x.Type == AlarmRuleTypes.Metric && x.Step == 2));
    }
}