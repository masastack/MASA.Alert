﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

global using Masa.Alert.Application.Contracts.AlarmRules.Dtos;
global using Masa.Alert.Infrastructure.Ddd.Application.Contracts.Dtos;
global using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
global using System.Linq.Expressions;
global using Masa.Alert.Domain.AlarmRules;
global using Masa.BuildingBlocks.Ddd.Domain.Repositories;
global using Masa.Contrib.Dispatcher.Events;
global using Mapster;
global using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;
global using FluentValidation;
global using Masa.Alert.Application.Contracts.AlarmRules.Validator;
global using Masa.BuildingBlocks.StackSdks.Auth;
global using Masa.Alert.Infrastructure.EntityFrameworkCore.Extensions;
global using Microsoft.EntityFrameworkCore;
global using static Masa.Alert.Application.Contracts.AlarmRules.Dtos.GetAlarmRuleInputDto;
global using Masa.Alert.Infrastructure.Common.Extensions;
global using System.Xml.Linq;
global using Masa.Alert.Domain.Shared.AlarmRules;
global using System.Collections.Concurrent;
global using Masa.BuildingBlocks.Ddd.Domain.Entities;
global using Masa.BuildingBlocks.RulesEngine;
global using Masa.Alert.Domain.AlarmRules.Repositories;
global using Masa.Alert.Domain.AlarmRules.Services;
global using Masa.Alert.Domain.AlarmRules.Aggregates;
global using Masa.Alert.Domain.AlarmRules.Events;
global using Masa.BuildingBlocks.StackSdks.Mc;
global using Masa.BuildingBlocks.StackSdks.Auth.Contracts.Model;
global using Masa.BuildingBlocks.StackSdks.Mc.Enum;
global using Masa.BuildingBlocks.StackSdks.Mc.Model;
global using Masa.Alert.Domain.AlarmHistories.Repositories;
global using Masa.Alert.Application.Contracts.AlarmHistories.Dtos;
global using Masa.Alert.Application.AlarmRules.Queries;
global using Masa.Alert.Application.Contracts.QueryContext;
global using Masa.Alert.Domain.AlarmHistories.Aggregates;
global using Microsoft.EntityFrameworkCore.Query;
global using Masa.Alert.Application.Contracts.QueryModels;
global using Masa.Alert.Domain.Shared.AlarmHistory;
global using static Masa.Alert.Application.Contracts.AlarmHistories.Dtos.GetAlarmHistoryInputDto;
global using Masa.Alert.Domain.AlarmHistories.Events;
global using Masa.Alert.Application.AlarmRules.Commands;
global using Masa.BuildingBlocks.Authentication.Identity;
global using Masa.BuildingBlocks.StackSdks.Tsc;
global using Masa.Alert.Domain.NotificationService;
global using Masa.BuildingBlocks.StackSdks.Tsc.Contracts.Model.Aggregate;
global using Masa.Alert.Application.Contracts.WebHooks.Dtos;
global using Masa.Alert.Domain.WebHooks.Aggregates;
global using Masa.Alert.Domain.WebHooks.Repositories;
global using System.Net.Http;
global using System.Text;
global using System.Text.Json;
global using Masa.Alert.Domain.WebHooks.Events;
global using Microsoft.Extensions.Logging;
global using Masa.Alert.Application.AlarmHistories.Commands;
global using Masa.BuildingBlocks.Ddd.Domain.Events;
global using Masa.BuildingBlocks.Dispatcher.Events;
global using Microsoft.AspNetCore.Components.Forms;
global using Microsoft.AspNetCore.Mvc;
global using Masa.BuildingBlocks.StackSdks.Tsc.Model;
global using Masa.BuildingBlocks.Ddd.Domain.SeedWork;