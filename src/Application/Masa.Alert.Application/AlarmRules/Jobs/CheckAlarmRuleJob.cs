// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Jobs
{
    public class CheckAlarmRuleJob : BackgroundJobBase<CheckAlarmRuleJobArgs>
    {
        private readonly IEventBus _eventBus;

        public CheckAlarmRuleJob(ILogger<BackgroundJobBase<CheckAlarmRuleJobArgs>>? logger
            , IEventBus eventBus) : base(logger)
        {
            _eventBus = eventBus;
        }

        protected override async Task ExecutingAsync(CheckAlarmRuleJobArgs args)
        {
            var command = new CheckAlarmRuleCommand(args.AlarmRuleId, args.ExcuteTime);
            await _eventBus.PublishAsync(command);
        }
    }
}
