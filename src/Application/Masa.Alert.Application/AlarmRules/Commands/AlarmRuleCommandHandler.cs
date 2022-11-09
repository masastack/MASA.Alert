// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Commands;

public class AlarmRuleCommandHandler
{
    private readonly IAlarmRuleRepository _repository;

    public AlarmRuleCommandHandler(IAlarmRuleRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task CreateAsync(CreateAlarmRuleCommand createCommand)
    {
        await ValidateAlarmRuleNameAsync(createCommand.AlarmRule.DisplayName, null);

        var entity = createCommand.AlarmRule.Adapt<AlarmRule>();

        await _repository.AddAsync(entity);
    }

    [EventHandler]
    public async Task UpdateAsync(UpdateAlarmRuleCommand updateCommand)
    {
        await ValidateAlarmRuleNameAsync(updateCommand.AlarmRule.DisplayName, updateCommand.AlarmRuleId);

        var entity = await _repository.FindAsync(x => x.Id == updateCommand.AlarmRuleId);

        Check.NotNull(entity, "alarmRule not found");

        updateCommand.AlarmRule.Adapt(entity);

        await _repository.UpdateAsync(entity);
    }

    [EventHandler]
    public async Task DeleteAsync(DeleteAlarmRuleCommand createCommand)
    {
        var entity = await _repository.FindAsync(x => x.Id == createCommand.AlarmRuleId);

        Check.NotNull(entity, "alarmRule not found");

        await _repository.RemoveAsync(entity);
    }

    private async Task ValidateAlarmRuleNameAsync(string displayName, Guid? id)
    {
        if (await _repository.FindAsync(x => x.DisplayName == displayName && x.Id != id) != null)
        {
            throw new UserFriendlyException("alarmRule name cannot be repeated");
        }
    }
}
