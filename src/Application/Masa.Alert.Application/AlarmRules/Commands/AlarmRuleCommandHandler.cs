// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Commands;

public class AlarmRuleCommandHandler
{
    private readonly IAlarmRuleRepository _repository;
    private readonly AlarmRuleDomainService _domainService;
    private readonly ISchedulerClient _schedulerClient;
    private readonly II18n<DefaultResource> _i18n;

    public AlarmRuleCommandHandler(IAlarmRuleRepository repository
        , AlarmRuleDomainService domainService
        , ISchedulerClient schedulerClient
        , II18n<DefaultResource> i18n)
    {
        _repository = repository;
        _domainService = domainService;
        _schedulerClient = schedulerClient;
        _i18n = i18n;
    }

    [EventHandler]
    public async Task CreateAsync(CreateAlarmRuleCommand createCommand)
    {
        var entity = createCommand.AlarmRule.Adapt<AlarmRule>();

        var checkFrequency = createCommand.AlarmRule.CheckFrequency.Adapt<CheckFrequency>();
        await _domainService.CreateAsync(entity, createCommand.AlarmRule.IsEnabled, checkFrequency);

        createCommand.Result = entity.Id;
    }

    [EventHandler]
    public async Task UpdateAsync(UpdateAlarmRuleCommand updateCommand)
    {
        var queryable = await _repository.WithDetailsAsync();
        var entity = await queryable.FirstOrDefaultAsync(x => x.Id == updateCommand.AlarmRuleId);

        MasaArgumentException.ThrowIfNull(entity, _i18n.T("AlarmRule"));       

        updateCommand.AlarmRule.Adapt(entity);

        var checkFrequency = updateCommand.AlarmRule.CheckFrequency.Adapt<CheckFrequency>();
        await _domainService.UpdateAsync(entity, updateCommand.AlarmRule.IsEnabled, checkFrequency);
    }

    [EventHandler]
    public async Task DeleteAsync(DeleteAlarmRuleCommand createCommand)
    {
        var entity = await _repository.FindAsync(x => x.Id == createCommand.AlarmRuleId);

        MasaArgumentException.ThrowIfNull(entity, _i18n.T("AlarmRule"));

        await _repository.RemoveAsync(entity);

        if (entity.SchedulerJobId != default)
        {
            try
            {
                await _schedulerClient.SchedulerJobService.RemoveAsync(new SchedulerJobRequestBase { JobId = entity.SchedulerJobId, OperatorId = entity.Modifier });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to remove scheduler job: {ex.Message}");
            }
        }
    }

    [EventHandler]
    public async Task EnabledAsync(EnabledAlarmRuleCommand command)
    {
        var entity = await _repository.FindAsync(x => x.Id == command.AlarmRuleId);

        MasaArgumentException.ThrowIfNull(entity, _i18n.T("AlarmRule"));

        entity.SetEnabled();
        await _repository.UpdateAsync(entity);
    }

    [EventHandler]
    public async Task DisableAsync(DisableAlarmRuleCommand command)
    {
        var entity = await _repository.FindAsync(x => x.Id == command.AlarmRuleId);

        MasaArgumentException.ThrowIfNull(entity, _i18n.T("AlarmRule"));

        entity.SetDisable();
        await _repository.UpdateAsync(entity);
    }
}
