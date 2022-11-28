// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

global using Masa.Alert.Infrastructure.Ddd.Application.Contracts.Dtos;
global using FluentValidation;
global using System.Reflection;
global using Masa.Alert.Infrastructure.Common.Extensions;
global using System.Collections.ObjectModel;
global using System.ComponentModel.DataAnnotations;
global using Masa.Alert.Infrastructure.Common.Utils;
global using System.Collections.Concurrent;
global using System.Net.Http;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using Masa.Alert.Domain.Shared.AlarmRules;
global using Masa.Alert.Domain.Shared.AlarmHistory;
global using Masa.Alert.Domain.Shared.Enums;
global using Masa.Alert.Application.Contracts.AlarmRules.Dtos;
global using Masa.Alert.Application.Contracts.QueryModels;
global using static Masa.Alert.Application.Contracts.AlarmRules.Dtos.GetAlarmRuleInputDto;
global using Masa.BuildingBlocks.Data;