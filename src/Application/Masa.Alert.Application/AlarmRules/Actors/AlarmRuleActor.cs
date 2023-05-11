// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

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
        var command = new CheckAlarmRuleCommand(AlarmRuleId, excuteTime);
        await _eventBus.PublishAsync(command);

        return true;
    }

    public Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
    {
        throw new NotImplementedException();
    }
}