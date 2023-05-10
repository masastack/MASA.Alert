// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Actors;

public class RuleCheckStates
{
    public static readonly RuleCheckStates Idle = new RuleCheckStates(1, nameof(Idle));
    public static readonly RuleCheckStates Executing = new RuleCheckStates(2, nameof(Executing));

    public RuleCheckStates()
    {
    }

    public RuleCheckStates(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; } = default!;
}
