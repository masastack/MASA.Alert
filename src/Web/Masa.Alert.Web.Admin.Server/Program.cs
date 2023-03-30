// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ValidatorOptions.Global.LanguageManager = new MasaLanguageManager();
GlobalValidationOptions.SetDefaultCulture("zh-CN");

await builder.Services.AddMasaStackConfigAsync();
var masaStackConfig = builder.Services.GetMasaStackConfig();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDaprStarter(opt =>
    {
        opt.AppId = builder.Configuration.GetValue<string>("AppId");
        opt.DaprHttpPort = 20604;
        opt.DaprGrpcPort = 20603;
    });
}

builder.Services.AddDaprClient();

builder.WebHost.UseKestrel(option =>
{
    option.ConfigureHttpsDefaults(options =>
    {
        /*if (string.IsNullOrEmpty(masaStackConfig.TlsName))
        {
            options.ServerCertificate = new X509Certificate2(Path.Combine("Certificates", "7348307__lonsid.cn.pfx"), "cqUza0MN");
        }
        else
        {
            options.ServerCertificate = X509Certificate2.CreateFromPemFile("./ssl/tls.crt", "./ssl/tls.key");
        }*/
        //options.ServerCertificate = new X509Certificate2(Path.Combine("Certificates", "7348307__lonsid.cn.pfx"), "cqUza0MN");
        options.CheckCertificateRevocation = false;
    });
});
builder.Services.AddObservable(builder.Logging, () =>
{
    return new MasaObservableOptions
    {
        ServiceNameSpace = builder.Environment.EnvironmentName,
        ServiceVersion = masaStackConfig.Version,
        ServiceName = masaStackConfig.GetWebId(MasaStackConstant.ALERT)
    };
}, () =>
{
    return masaStackConfig.OtlpUrl;
}, true);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorDownloadFile();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
var authBaseAddress = masaStackConfig.GetAuthServiceDomain();
var alertBaseAddress = builder.Services.GetMasaConfiguration().ConfigurationApi.GetDefault().GetValue<string>("AppSettings:AlertClient:Url");

if (string.IsNullOrEmpty(alertBaseAddress))
{
    alertBaseAddress = masaStackConfig.GetAlertServiceDomain();
}

builder.AddMasaStackComponentsForServer("wwwroot/i18n", authBaseAddress);

builder.Services.AddHttpContextAccessor();
builder.Services.AddGlobalForServer();
//builder.Services.AddScoped<TokenProvider>();
var assemblies = Assembly.GetExecutingAssembly();
TypeAdapterConfig.GlobalSettings.Scan(assemblies, Assembly.Load("Masa.Alert.Contracts.Admin"));
MasaOpenIdConnectOptions masaOpenIdConnectOptions = new MasaOpenIdConnectOptions
{
    Authority = masaStackConfig.GetSsoDomain(),
    ClientId = masaStackConfig.GetWebId(MasaStackConstant.ALERT),
    Scopes = new List<string> { "offline_access" }
};

IdentityModelEventSource.ShowPII = true;
builder.Services.AddMasaOpenIdConnect(masaOpenIdConnectOptions);

builder.Services.AddAlertApiGateways(option =>
{
    option.AlertServiceBaseAddress = alertBaseAddress;
    option.AuthorityEndpoint = masaOpenIdConnectOptions.Authority;
    option.ClientId = masaOpenIdConnectOptions.ClientId;
    option.ClientSecret = masaOpenIdConnectOptions.ClientSecret;
});

builder.Services.AddTscClient(masaStackConfig.GetTscServiceDomain());
builder.Services.AddMapster();

builder.Services.AddAutoInject(assemblies);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();