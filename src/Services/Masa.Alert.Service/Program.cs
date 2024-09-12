// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddMasaStackConfigAsync(MasaStackProject.Alert, MasaStackApp.Service);
var masaStackConfig = builder.Services.GetMasaStackConfig();

builder.Services.AddObservable(builder.Logging, new MasaObservableOptions
{
    ServiceNameSpace = builder.Environment.EnvironmentName,
    ServiceVersion = masaStackConfig.Version,
    ServiceName = masaStackConfig.GetServiceId(MasaStackProject.Alert),
    Layer = masaStackConfig.Namespace,
    ServiceInstanceId = builder.Configuration.GetValue<string>("HOSTNAME")!
}
, masaStackConfig.OtlpUrl, activitySources: new string[] { CheckAlarmRuleJob.ActivitySource.Name });

builder.Services.AddDaprClient();
var identityServerUrl = masaStackConfig.GetSsoDomain();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDaprStarter(opt =>
    {
        opt.AppId = masaStackConfig.GetServiceId(MasaStackProject.Alert);
        opt.DaprHttpPort = 20602;
        opt.DaprGrpcPort = 20601;
    });
}

var publicConfiguration = builder.Services.GetMasaConfiguration().ConfigurationApi.GetPublic();
builder.Services.AddMasaIdentity(options =>
{
    options.Environment = IdentityClaimConsts.ENVIRONMENT;
    options.UserName = IdentityClaimConsts.USER_NAME;
    options.UserId = IdentityClaimConsts.USER_ID;
    options.Mapping(nameof(MasaUser.CurrentTeamId), IdentityClaimConsts.CURRENT_TEAM);
    options.Mapping(nameof(MasaUser.StaffId), IdentityClaimConsts.STAFF);
    options.Mapping(nameof(MasaUser.Account), IdentityClaimConsts.ACCOUNT);
});
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("Bearer", options =>
{
    //todo dcc
    options.Authority = identityServerUrl;
    options.RequireHttpsMetadata = false;
    //options.Audience = "";
    options.TokenValidationParameters.ValidateAudience = false;
    options.MapInboundClaims = false;

    options.BackchannelHttpHandler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (
            sender,
            certificate,
            chain,
            sslPolicyErrors) =>
        { return true; }
    };
});

builder.Services.AddMapster();
var assemblies = AppDomain.CurrentDomain.GetAllAssemblies();
TypeAdapterConfig.GlobalSettings.Scan(assemblies);
builder.Services.AddAutoInject(assemblies);
builder.Services.AddScoped<INotificationSender, McNotificationSender>();
builder.Services.AddSequentialGuidGenerator();
builder.Services.AddI18n(Path.Combine("Assets", "I18n"));
var redisOptions = new RedisConfigurationOptions
{
    Servers = new List<RedisServerOptions> {
        new RedisServerOptions()
        {
            Host= masaStackConfig.RedisModel.RedisHost,
            Port=   masaStackConfig.RedisModel.RedisPort
        }
    },
    DefaultDatabase = masaStackConfig.RedisModel.RedisDb,
    Password = masaStackConfig.RedisModel.RedisPassword
};
#if DEBUG
builder.Services.AddAuthClient("https://auth-service-dev.masastack.com", redisOptions);
builder.Services.AddPmClient("https://pm-service-dev.masastack.com");
builder.Services.AddTscClient("https://tsc-service-dev.masastack.com");
builder.Services.AddMcClient("https://mc-service-dev.masastack.com");
builder.Services.AddSchedulerClient("https://scheduler-service-dev.masastack.com");
#else
builder.Services.AddAuthClient(masaStackConfig.GetAuthServiceDomain(), redisOptions);
builder.Services.AddPmClient(masaStackConfig.GetPmServiceDomain());
builder.Services.AddTscClient(masaStackConfig.GetTscServiceDomain());
builder.Services.AddMcClient(masaStackConfig.GetMcServiceDomain());
builder.Services.AddSchedulerClient(masaStackConfig.GetSchedulerServiceDomain());
#endif
builder.Services.AddRulesEngine(rulesEngineOptions =>
{
    rulesEngineOptions.UseMicrosoftRulesEngine();
});
builder.Services.AddBackgroundJob(options =>
{
    options.UseInMemoryDatabase();
});

builder.Services
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer xxxxxxxxxxxxxxx\"",
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    })
    .AddValidatorsFromAssemblies(assemblies)
    .AddMasaDbContext<AlertDbContext>(builder =>
    {
        builder.UseSqlServer();
        builder.UseFilter(options => options.EnableSoftDelete = true);
    })
    .AddMasaDbContext<AlertQueryContext>(builder =>
    {
        builder.UseSqlServer();
        builder.UseFilter(options => options.EnableSoftDelete = true);
    })
    .AddScoped<IAlertQueryContext, AlertQueryContext>()
    .AddDomainEventBus(dispatcherOptions =>
    {
        dispatcherOptions
        .UseIntegrationEventBus<IntegrationEventLogService>(options => options.UseDapr().UseEventLog<AlertDbContext>())
        .UseEventBus(eventBusBuilder =>
        {
            eventBusBuilder.UseMiddleware(typeof(ValidatorMiddleware<>));
        })
        .UseUoW<AlertDbContext>()
        .UseRepository<AlertDbContext>();
    });
await builder.Services.AddStackIsolationAsync(MasaStackProject.Alert.Name);

builder.Services.AddStackMiddleware();
await builder.MigrateDbContextAsync<AlertDbContext>();

var app = builder.AddServices(options =>
{
    options.MapHttpMethodsForUnmatched = new string[] { "Post" };
});

app.UseI18n();

app.UseMasaExceptionHandler(opt =>
{
    opt.ExceptionHandler = context =>
    {
        if (context.Exception is ValidationException validationException)
        {
            context.ToResult(validationException.Errors.Select(err => err.ToString()).FirstOrDefault()!);
        }
    };
});

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseStackIsolation();
app.UseStackMiddleware();
app.UseCloudEvents();
app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
});
app.UseHttpsRedirection();

app.Run();
