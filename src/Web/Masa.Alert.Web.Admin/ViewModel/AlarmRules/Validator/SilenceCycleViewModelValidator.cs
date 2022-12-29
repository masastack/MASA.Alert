// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules.Validator;

public class SilenceCycleViewModelValidator : AbstractValidator<SilenceCycleViewModel>
{
    public SilenceCycleViewModelValidator()
    {
        RuleFor(x => x.TimeInterval).SetValidator(new TimeIntervalViewModelValidator()).When(x => x.Type == SilenceCycleTypes.Time);
    }
}
