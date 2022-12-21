using AZ_SSOTest.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace AZ_SSOTest.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AZ_SSOTestEntityFrameworkCoreModule),
    typeof(AZ_SSOTestApplicationContractsModule)
    )]
public class AZ_SSOTestDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
