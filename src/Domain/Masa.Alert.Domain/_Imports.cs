// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

global using Masa.BuildingBlocks.Ddd.Domain.Entities.Full;
global using Masa.BuildingBlocks.Ddd.Domain.Entities;
global using Masa.Alert.Domain.Shared.AlarmRules;
global using System.Collections.ObjectModel;
global using Masa.BuildingBlocks.Ddd.Domain.Repositories;
global using Masa.Alert.Domain.Shared.Enums;
global using Masa.BuildingBlocks.Ddd.Domain.Values;
global using Masa.Contrib.Ddd.Domain;
global using Masa.BuildingBlocks.Ddd.Domain.Events;
global using Masa.BuildingBlocks.RulesEngine;
global using Masa.Contrib.RulesEngine.MicrosoftRulesEngine;
global using System.Collections.Concurrent;
global using Masa.BuildingBlocks.Dispatcher.Events;
global using Masa.Alert.Domain.AlarmRules.Aggregates;
global using Masa.BuildingBlocks.Data;
global using Masa.Alert.Domain.AlarmRules.Repositories;
global using FluentValidation.Results;
global using Masa.Contrib.Dispatcher.Events;
global using Microsoft.Extensions.Logging;
global using Masa.Alert.Domain.Shared.AlarmHistory;
global using Masa.Alert.Domain.AlarmHistorys;
global using Masa.Alert.Domain.AlarmHistorys.Events;
global using Masa.Alert.Domain.Shared;
global using Masa.Alert.Domain.AlarmHistorys.Aggregates;
global using Masa.Alert.Domain.AlarmHistorys.Repositories;
global using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;