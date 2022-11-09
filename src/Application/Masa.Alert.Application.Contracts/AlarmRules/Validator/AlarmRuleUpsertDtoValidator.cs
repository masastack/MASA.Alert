// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Validator;

public class AlarmRuleUpsertDtoValidator : AbstractValidator<AlarmRuleUpsertDto>
{
    public AlarmRuleUpsertDtoValidator()
    {
        RuleFor(inputDto => inputDto.DisplayName).Required().ChineseLetterNumberSymbol().Length(2, 50);
    }
}