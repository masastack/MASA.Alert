// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;
using StackExchange.Redis;

namespace Masa.Alert.Application.AlarmRules.Actors;

public class AlarmRuleActor : Actor, IAlarmRuleActor, IRemindable
{
    private const string RULE_CHECK_STATE_NAME = "RuleCheckState";
    private const string CHECK_COMPLETE_REMINDER = "CheckComplete";
    private readonly ILogger<AlarmRuleActor> _logger;
    private readonly IEventBus _eventBus;

    private Guid AlarmRuleId => Guid.Parse(Id.GetId());

    public AlarmRuleActor(
        ActorHost host,
        ILogger<AlarmRuleActor> logger,
        IEventBus eventBus) : base(host)
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    public async Task<bool> Check(DateTimeOffset? excuteTime)
    {
        //Console.WriteLine("Check:" + Id.GetId());
        //var ruleCheckStates = await StateManager.GetStateAsync<RuleCheckStates>(RULE_CHECK_STATE_NAME);

        //if (ruleCheckStates.Id != RuleCheckStates.Idle.Id)
        //{
        //    _logger.LogWarning("AlarmRule with Id: {AlarmRuleId} please wait for the previous check to complete",
        //        AlarmRuleId);
        //    return false;
        //}

        //await StateManager.SetStateAsync(RULE_CHECK_STATE_NAME, RuleCheckStates.Executing);

        var command = new CheckAlarmRuleCommand(AlarmRuleId, excuteTime);
        await _eventBus.PublishAsync(command);

        //await RegisterReminderAsync(
        //    CHECK_COMPLETE_REMINDER,
        //    null,
        //    TimeSpan.FromSeconds(2),
        //    TimeSpan.FromMilliseconds(-1));

        return true;
    }

    public Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
    {
        switch (reminderName)
        {
            case CHECK_COMPLETE_REMINDER: return OnCheckComplete();
        }

        _logger.LogError("Unknown actor reminder {ReminderName}", reminderName);
        return Task.CompletedTask;
    }

    public async Task OnCheckComplete()
    {
        await StateManager.SetStateAsync(RULE_CHECK_STATE_NAME, RuleCheckStates.Idle);
    }
}
