// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules.Validator;

public class LogMonitorItemViewModelValidator : AbstractValidator<LogMonitorItemViewModel>
{
    public LogMonitorItemViewModelValidator()
    {
        RuleFor(x => x.Field).Required();
        RuleFor(x => x.Alias).Required();
    }
}