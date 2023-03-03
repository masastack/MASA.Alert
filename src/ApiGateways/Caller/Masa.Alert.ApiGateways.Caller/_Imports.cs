﻿global using Masa.Contrib.Service.Caller.DaprClient;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;
global using Masa.Contrib.Service.Caller;
global using System.Reflection;
global using Masa.BuildingBlocks.Service.Caller;
global using Masa.Alert.ApiGateways.Caller.Base;
global using Masa.Alert.ApiGateways.Caller.Const;
global using Masa.Alert.ApiGateways.Caller.Extensions;
global using Masa.Alert.Infrastructure.Common.Utils;
global using IdentityModel.Client;
global using System.Net.Http.Headers;
global using Microsoft.Extensions.Hosting;
global using Masa.Contrib.Service.Caller.Authentication.OpenIdConnect;
global using Masa.Alert.Application.Contracts.AlarmRules.Dtos;
global using Masa.Alert.Infrastructure.Ddd.Application.Contracts.Dtos;
global using Masa.Alert.ApiGateways.Caller.Services.AlarmRules;
global using Masa.Alert.Application.Contracts.AlarmHistories.Dtos;
global using Masa.Alert.ApiGateways.Caller.Services.AlarmHistories;
global using Masa.Alert.Application.Contracts.WebHooks.Dtos;
global using Masa.Alert.ApiGateways.Caller.Services.WebHooks;
global using Masa.Contrib.Service.Caller.HttpClient;
global using IdentityModel;
global using Masa.Contrib.Service.Caller.Authentication.OpenIdConnect.Jwt;
global using Microsoft.Extensions.Logging;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Security.Cryptography;