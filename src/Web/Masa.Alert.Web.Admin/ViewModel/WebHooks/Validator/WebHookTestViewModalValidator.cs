// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.WebHooks.Validator;

public class WebHookTestViewModalValidator : AbstractValidator<WebHookTestViewModal>
{
    public WebHookTestViewModalValidator()
    {
        RuleFor(x => x.UserId).Required();
        RuleFor(x => x.WebHookId).Required();
    }
}