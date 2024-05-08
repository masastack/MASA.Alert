// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Jobs;

public class CheckAlarmRuleJob : BackgroundJobBase<CheckAlarmRuleJobArgs>
{
    private readonly IEventBus _eventBus;
    public static ActivitySource ActivitySource { get; private set; } = new("Masa.Alert.Background");

    public CheckAlarmRuleJob(ILogger<BackgroundJobBase<CheckAlarmRuleJobArgs>>? logger
        , IEventBus eventBus) : base(logger)
    {
        _eventBus = eventBus;
    }

    protected async override Task ExecutingAsync(CheckAlarmRuleJobArgs args)
    {
        var command = new CheckAlarmRuleCommand(args.AlarmRuleId, args.ExcuteTime);
        var activity = string.IsNullOrEmpty(args.TraceParent) ? default : ActivitySource.StartActivity("", ActivityKind.Consumer, args.TraceParent);
        try
        {
            await _eventBus.PublishAsync(command);
        }
        finally
        {
            activity?.Dispose();
        }
    }
}
