// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using Mapster;
using Masa.Alert.Infrastructure.Common.Extensions;
using Masa.Alert.Web.Admin;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddSingleton(sp => builder.Configuration);
//builder.Services.AddDccClient("https://dcc-service-dev.masastack.com");
await builder.Services.AddMasaStackConfigAsync(builder.Configuration, builder.HostEnvironment.Environment);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddGlobalForWasmAsync();
var masaStackConfig = builder.Services.GetMasaStackConfig();

MasaOpenIdConnectOptions masaOpenIdConnectOptions = new()
{
    Authority = masaStackConfig.GetSsoDomain(),
    ClientId = masaStackConfig.GetWebId(MasaStackProject.Alert),
    Scopes = new List<string> { "offline_access" }
};

builder.AddMasaOpenIdConnect(masaOpenIdConnectOptions);

builder.Services.AddAlertApiGateways(option =>
{
    option.ServiceBaseAddress = masaStackConfig.GetAlertServiceDomain();
});

builder.Services.AddMasaStackComponent(MasaStackProject.Alert);

builder.Services.AddTscClient(masaStackConfig.GetTscServiceDomain());
builder.Services.AddMapster();
var assemblies = AppDomain.CurrentDomain.GetAllAssemblies();
TypeAdapterConfig.GlobalSettings.Scan(assemblies);
builder.Services.AddAutoInject(assemblies);


var host = builder.Build();
await host.Services.InitializeMasaStackApplicationAsync();
await host.RunAsync();
