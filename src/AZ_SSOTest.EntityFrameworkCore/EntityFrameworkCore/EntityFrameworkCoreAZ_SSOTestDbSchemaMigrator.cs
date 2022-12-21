using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AZ_SSOTest.Data;
using Volo.Abp.DependencyInjection;

namespace AZ_SSOTest.EntityFrameworkCore;

public class EntityFrameworkCoreAZ_SSOTestDbSchemaMigrator
    : IAZ_SSOTestDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreAZ_SSOTestDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the AZ_SSOTestDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<AZ_SSOTestDbContext>()
            .Database
            .MigrateAsync();
    }
}
