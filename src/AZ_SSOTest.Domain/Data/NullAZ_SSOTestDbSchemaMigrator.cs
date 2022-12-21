using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AZ_SSOTest.Data;

/* This is used if database provider does't define
 * IAZ_SSOTestDbSchemaMigrator implementation.
 */
public class NullAZ_SSOTestDbSchemaMigrator : IAZ_SSOTestDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
