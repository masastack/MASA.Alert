// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.WebHooks.Validator;
public class WebHookUpsertViewModelValidator : AbstractValidator<WebHookUpsertViewModel>
{
    public WebHookUpsertViewModelValidator(I18n i18n)
    {
        var scope = "WebHookBlock";
        RuleFor(x => x.DisplayName).Required(string.Format(i18n.T("RequiredValidator"), i18n.T(scope,"DisplayName")))
            .ChineseLetterNumberSymbol().WithMessage(string.Format(i18n.T("ChineseLetterNumberSymbolValidator"), i18n.T(scope, "DisplayName")))
            .Length(2, 50).WithMessage(string.Format(i18n.T("LengthValidator"), i18n.T(scope, "DisplayName"), 2, 50));
        RuleFor(x => x.SecretKey).Required(string.Format(i18n.T("RequiredValidator"), i18n.T(scope, "SecretKey")));
        FluentValidation.FluentValidationExtensions.Url( RuleFor(x => x.Url).Required(string.Format(i18n.T("RequiredValidator"), i18n.T(scope, "Url")))
            ).WithMessage(string.Format(i18n.T("UrlValidator"), i18n.T(scope, "Url")));
    }
}