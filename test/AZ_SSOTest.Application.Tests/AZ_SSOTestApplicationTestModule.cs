using Volo.Abp.Modularity;

namespace AZ_SSOTest;

[DependsOn(
    typeof(AZ_SSOTestApplicationModule),
    typeof(AZ_SSOTestDomainTestModule)
    )]
public class AZ_SSOTestApplicationTestModule : AbpModule
{

}
