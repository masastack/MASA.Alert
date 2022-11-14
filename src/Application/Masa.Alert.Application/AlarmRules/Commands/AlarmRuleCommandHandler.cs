// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Commands;

public class AlarmRuleCommandHandler
{
    private readonly IAlarmRuleRepository _repository;
    private readonly AlarmRuleDomainService _domainService;

    public AlarmRuleCommandHandler(IAlarmRuleRepository repository
        , AlarmRuleDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    [EventHandler]
    public async Task CreateAsync(CreateAlarmRuleCommand createCommand)
    {
        var entity = createCommand.AlarmRule.Adapt<AlarmRule>();

        await _domainService.ValidateRuleAsync(entity);

        await _repository.AddAsync(entity);
    }

    [EventHandler]
    public async Task UpdateAsync(UpdateAlarmRuleCommand updateCommand)
    {
        var queryable = await _repository.WithDetailsAsync();
        var entity = await queryable.FirstOrDefaultAsync(x => x.Id == updateCommand.AlarmRuleId);

        Check.NotNull(entity, "alarmRule not found");

        entity.Items.RemoveAll(x => !updateCommand.AlarmRule.Items.Any(y => y.Id == x.Id));
        updateCommand.AlarmRule.Adapt(entity);

        await _domainService.ValidateRuleAsync(entity);

        await _repository.UpdateAsync(entity);
    }

    [EventHandler]
    public async Task DeleteAsync(DeleteAlarmRuleCommand createCommand)
    {
        var entity = await _repository.FindAsync(x => x.Id == createCommand.AlarmRuleId);

        Check.NotNull(entity, "alarmRule not found");

        await _repository.RemoveAsync(entity);
    }

    [EventHandler]
    public async Task EnabledAsync(EnabledAlarmRuleCommand command)
    {
        var entity = await _repository.FindAsync(x => x.Id == command.AlarmRuleId);

        Check.NotNull(entity, "alarmRule not found");

        entity.SetEnabled();
        await _repository.UpdateAsync(entity);
    }

    [EventHandler]
    public async Task DisableAsync(DisableAlarmRuleCommand command)
    {
        var entity = await _repository.FindAsync(x => x.Id == command.AlarmRuleId);

        Check.NotNull(entity, "alarmRule not found");

        entity.SetDisable();
        await _repository.UpdateAsync(entity);
    }
}
