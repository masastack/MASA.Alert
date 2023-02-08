// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules.Validator;

public class MetricMonitorItemViewModelValidator : AbstractValidator<MetricMonitorItemViewModel>
{
    public MetricMonitorItemViewModelValidator()
    {
        RuleFor(x => x.Alias).Required();
        RuleFor(x => x.Alias).Required();
    }
}