// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.WebHooks.Validator;

public class WebHookUpsertViewModelValidator : AbstractValidator<WebHookUpsertViewModel>
{
    public WebHookUpsertViewModelValidator()
    {
        RuleFor(x => x.DisplayName).Required().ChineseLetterNumberSymbol().Length(2, 50);
        RuleFor(x => x.SecretKey).Required();
        RuleFor(x => x.Url).Url();
    }
}