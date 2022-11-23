// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore.Repositories;

public class WebHookRepository : Repository<AlertDbContext, WebHook>, IWebHookRepository
{
    public WebHookRepository(AlertDbContext context, IUnitOfWork unitOfWork)
        : base(context, unitOfWork) { }
}
