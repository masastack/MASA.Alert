// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.ApiGateways.Caller.Base;

public abstract class BaseDaprCaller : DaprCallerBase
{
    protected BaseDaprCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

        if (!webHostEnvironment.IsDevelopment())
        {
            AppId = $"{AppId}-{webHostEnvironment.EnvironmentName.ToLower()}";
        }
    }
}