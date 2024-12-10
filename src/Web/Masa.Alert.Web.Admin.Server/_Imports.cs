// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

// System Namespaces
global using System.Security.Cryptography.X509Certificates;

// Microsoft Namespaces
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authentication.Cookies;
global using Microsoft.AspNetCore.Authentication.OpenIdConnect;
global using Microsoft.AspNetCore.Hosting.StaticWebAssets;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.RazorPages;
global using Microsoft.AspNetCore.ResponseCompression;
global using Microsoft.IdentityModel.Logging;
global using Microsoft.IdentityModel.Protocols.OpenIdConnect;

// Third-party Libraries
global using BlazorDownloadFile;
global using FluentValidation;
global using FluentValidation.Resources;
global using Mapster;

// MASA Building Blocks and Contrib Packages
global using Masa.Contrib.StackSdks.Caller;
global using Masa.Contrib.StackSdks.Config;
global using Masa.Contrib.Configuration.ConfigurationApi.Dcc;
global using Masa.Stack.Components;
global using Masa.Stack.Components.Extensions.OpenIdConnect;

// MASA Alert Infrastructure and Extensions
global using Masa.Alert.Infrastructure.Common.Extensions;
global using Masa.Alert.ApiGateways.Caller.Extensions;