// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

// System Namespaces
global using System.Collections.Concurrent;
global using System.Collections.ObjectModel;

// MASA Building Blocks and Contrib Packages
global using Masa.BuildingBlocks.Caching;
global using Masa.BuildingBlocks.Data;
global using Masa.BuildingBlocks.Data.Contracts;
global using Masa.BuildingBlocks.Ddd.Domain.Entities.Full;
global using Masa.BuildingBlocks.Ddd.Domain.Events;
global using Masa.BuildingBlocks.Ddd.Domain.Repositories;
global using Masa.BuildingBlocks.Ddd.Domain.Services;
global using Masa.BuildingBlocks.Ddd.Domain.Values;
global using Masa.BuildingBlocks.RulesEngine;
global using Masa.Contrib.Dispatcher.Events;

// MASA Alert Domain Shared
global using Masa.Alert.Domain.Shared.AlarmRules;
global using Masa.Alert.Domain.Shared.AlarmHistory;
global using Masa.Alert.Domain.Shared.Consts;

// MASA Alert Domain Aggregates
global using Masa.Alert.Domain.AlarmRules.Aggregates;
global using Masa.Alert.Domain.AlarmHistories.Aggregates;
global using Masa.Alert.Domain.WebHooks.Aggregates;

// MASA Alert Domain Events
global using Masa.Alert.Domain.AlarmRules.Events;
global using Masa.Alert.Domain.AlarmHistories.Events;
global using Masa.Alert.Domain.WebHooks.Events;

// MASA Alert Domain Repositories
global using Masa.Alert.Domain.AlarmRules.Repositories;

// MASA Alert Infrastructure
global using Masa.Alert.Infrastructure.Common.Utils;