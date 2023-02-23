var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDaprClient();
builder.Services.AddMasaConfiguration(configurationBuilder =>
{
    configurationBuilder.UseDcc();
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDaprStarter(opt =>
    {
        opt.AppId = builder.Services.GetMasaConfiguration().Local.GetValue<string>("AppId");
        opt.DaprHttpPort = 20602;
        opt.DaprGrpcPort = 20601;
    });
}

var publicConfiguration = builder.Services.GetMasaConfiguration().ConfigurationApi.GetPublic();
builder.Services.AddObservable(builder.Logging, () =>
{
    return new MasaObservableOptions
    {
        ServiceNameSpace = builder.Environment.EnvironmentName,
        ServiceVersion = "1.0.0",//todo global version
        ServiceName = "masa-alert-service-admin"
    };
}, () =>
{
    return publicConfiguration.GetValue<string>("$public.AppSettings:OtlpUrl");
});
builder.Services.AddMasaIdentity(options =>
{
    options.Environment = "environment";
    options.UserName = "name";
    options.UserId = "sub";
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
    options.Authority = publicConfiguration.GetValue<string>("$public.AppSettings:IdentityServerUrl");
    options.RequireHttpsMetadata = false;
    //options.Audience = "";
    options.TokenValidationParameters.ValidateAudience = false;
    options.MapInboundClaims = false;
});
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy("A healthy result."))
    .AddDbContextCheck<AlertDbContext>();

builder.Services.AddMapster();
var assemblies = AppDomain.CurrentDomain.GetAllAssemblies();
TypeAdapterConfig.GlobalSettings.Scan(assemblies);
builder.Services.AddAutoInject(assemblies);
builder.Services.AddScoped<INotificationSender, McNotificationSender>();
builder.Services.AddSequentialGuidGenerator();
builder.Services.AddI18n(Path.Combine("Assets", "I18n"));
var redisOptions = publicConfiguration.GetSection("$public.RedisConfig").Get<RedisConfigurationOptions>();
builder.Services.AddAuthClient(publicConfiguration.GetValue<string>("$public.AppSettings:AuthClient:Url"), redisOptions);
builder.Services.AddTscClient(publicConfiguration.GetValue<string>("$public.AppSettings:TscClient:Url"));
builder.Services.AddMcClient(publicConfiguration.GetValue<string>("$public.AppSettings:McClient:Url"));
builder.Services.AddSchedulerClient(publicConfiguration.GetValue<string>("$public.AppSettings:SchedulerClient:Url"));
builder.Services.AddRulesEngine(rulesEngineOptions =>
{
    rulesEngineOptions.UseMicrosoftRulesEngine();
});

var app = builder.Services
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
        .UseIsolationUoW<AlertDbContext>(isolationBuilder => isolationBuilder.UseMultiEnvironment("env_key"), null)
        .UseRepository<AlertDbContext>();
    })
    .AddServices(builder);
await builder.MigrateDbContextAsync<AlertDbContext>();
app.UseI18n();
app.UseMasaExceptionHandler(opt =>
{
    opt.ExceptionHandler += context =>
    {
        if (context.Exception is ValidationException validationException)
        {
            context.ToResult(validationException.Errors.Select(err => err.ToString()).FirstOrDefault()!);
        }
    };
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
});
app.UseHttpsRedirection();

app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecks("/liveness", new HealthCheckOptions
{
    Predicate = r => r.Name.Contains("self")
});
app.Run();
