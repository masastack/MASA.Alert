﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

global using System.Diagnostics;
global using System.Text.Json;
global using System.Text.RegularExpressions;
global using FluentValidation;
global using Mapster;
global using Masa.Alert.Application.AlarmHistories.Commands;
global using Masa.Alert.Application.AlarmHistories.Queries;
global using Masa.Alert.Application.AlarmRules.Commands;
global using Masa.Alert.Application.AlarmRules.Jobs;
global using Masa.Alert.Application.AlarmRules.Queries;
global using Masa.Alert.Application.Contracts.AlarmHistories.Dtos;
global using Masa.Alert.Application.Contracts.AlarmRules.Dtos;
global using Masa.Alert.Application.Contracts.QueryContext;
global using Masa.Alert.Application.Contracts.WebHooks.Dtos;
global using Masa.Alert.Application.WebHooks.Commands;
global using Masa.Alert.Application.WebHooks.Queries;
global using Masa.Alert.Contracts.Admin.Consts;
global using Masa.Alert.Domain.NotificationService;
global using Masa.Alert.EntityFrameworkCore;
global using Masa.Alert.Service.Admin.Infrastructure.Extensions;
global using Masa.Alert.Infrastructure.Cache;
global using Masa.Alert.Infrastructure.Common.Extensions;
global using Masa.Alert.Infrastructure.Ddd.Application.Contracts.Dtos;
global using Masa.Alert.Infrastructure.Middleware;
global using Masa.Alert.NotificationService.Provider.Mc;
global using Masa.Alert.Service.Admin.Internal;
global using Masa.Alert.Service.Admin.Infrastructure.Authentication;
global using Masa.BuildingBlocks.Caching;
global using Masa.BuildingBlocks.Data;
global using Masa.BuildingBlocks.Data.UoW;
global using Masa.BuildingBlocks.Ddd.Domain.Repositories;
global using Masa.BuildingBlocks.Dispatcher.Events;
global using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;
global using Masa.BuildingBlocks.Extensions.BackgroundJobs;
global using Masa.BuildingBlocks.RulesEngine;
global using Masa.BuildingBlocks.StackSdks.Auth.Contracts;
global using Masa.BuildingBlocks.StackSdks.Config;
global using Masa.BuildingBlocks.StackSdks.Config.Consts;
global using Masa.BuildingBlocks.StackSdks.Config.Models;
global using Masa.Contrib.Caching.Distributed.StackExchangeRedis;
global using Masa.Contrib.Configuration.ConfigurationApi.Dcc;
global using Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EFCore;
global using Masa.Contrib.StackSdks.Caller;
global using Masa.Contrib.StackSdks.Config;
global using Masa.Contrib.StackSdks.Isolation;
global using Masa.Contrib.StackSdks.Middleware;
global using Masa.Contrib.StackSdks.Tsc;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Primitives;
global using Microsoft.OpenApi.Models;
global using IdentityModel.Client;