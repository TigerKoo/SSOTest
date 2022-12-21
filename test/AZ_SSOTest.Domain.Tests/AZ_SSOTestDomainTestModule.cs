using AZ_SSOTest.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace AZ_SSOTest;

[DependsOn(
    typeof(AZ_SSOTestEntityFrameworkCoreTestModule)
    )]
public class AZ_SSOTestDomainTestModule : AbpModule
{

}
