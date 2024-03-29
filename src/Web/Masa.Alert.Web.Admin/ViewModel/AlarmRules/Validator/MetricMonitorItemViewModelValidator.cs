﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules.Validator;

public class MetricMonitorItemViewModelValidator : AbstractValidator<MetricMonitorItemViewModel>
{
    public MetricMonitorItemViewModelValidator(I18n i18n)
    {
        var scope = "AlarmRuleBlock";
        RuleFor(x => x.Alias).Required(string.Format(i18n.T("RequiredValidator"), i18n.T(scope, "Alias")));
    }
}