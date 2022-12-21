using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace AZ_SSOTest;

public class AZ_SSOTestWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<AZ_SSOTestWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
