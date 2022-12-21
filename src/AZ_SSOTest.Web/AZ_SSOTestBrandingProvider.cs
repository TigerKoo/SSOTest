using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace AZ_SSOTest.Web;

[Dependency(ReplaceServices = true)]
public class AZ_SSOTestBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "AZ_SSOTest";
}
