// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using static System.Formats.Asn1.AsnWriter;

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules.Validator;

public class LogMonitorItemViewModelValidator : AbstractValidator<LogMonitorItemViewModel>
{
    public LogMonitorItemViewModelValidator(I18n i18n)
    {
        var scope = "AlarmRuleBlock";
        RuleFor(x => x.Field).Required(string.Format(i18n.T("RequiredValidator"), i18n.T(scope, "Field")));
        RuleFor(x => x.Alias).Required(string.Format(i18n.T("RequiredValidator"), i18n.T(scope, "Alias")));
    }
}