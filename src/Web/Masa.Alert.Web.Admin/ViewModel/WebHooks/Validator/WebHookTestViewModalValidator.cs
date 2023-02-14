// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.WebHooks.Validator;

public class WebHookTestViewModalValidator : AbstractValidator<WebHookTestViewModal>
{
    public WebHookTestViewModalValidator(I18n i18n)
    {
        RuleFor(x => x.UserId).Required(string.Format(i18n.T("RequiredValidator"), i18n.T("User")));
        RuleFor(x => x.WebHookId).Required();
    }
}