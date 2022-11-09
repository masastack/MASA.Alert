// Copyright (c) MASA Stack All rights reserved.
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