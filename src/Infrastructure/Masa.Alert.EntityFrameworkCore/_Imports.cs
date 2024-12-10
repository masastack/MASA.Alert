// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

global using System.Collections.Concurrent;
global using Masa.Alert.Application.Contracts.QueryContext;
global using Masa.Alert.Application.Contracts.QueryModels;
global using Masa.Alert.Domain.AlarmHistories.Aggregates;
global using Masa.Alert.Domain.AlarmHistories.Repositories;
global using Masa.Alert.Domain.AlarmRules;
global using Masa.Alert.Domain.AlarmRules.Aggregates;
global using Masa.Alert.Domain.AlarmRules.Repositories;
global using Masa.Alert.Domain.Shared.Consts;
global using Masa.Alert.Domain.WebHooks.Aggregates;
global using Masa.Alert.Domain.WebHooks.Repositories;
global using Masa.Alert.Infrastructure.EntityFrameworkCore.EntityFrameworkCore.ValueConverters;
global using Masa.BuildingBlocks.Data.UoW;
global using Masa.BuildingBlocks.Data.Contracts;
global using Masa.Contrib.Ddd.Domain.Repository.EFCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Logging;
global using System.Reflection;