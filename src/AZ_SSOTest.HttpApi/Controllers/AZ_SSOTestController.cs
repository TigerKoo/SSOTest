using AZ_SSOTest.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AZ_SSOTest.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AZ_SSOTestController : AbpControllerBase
{
    protected AZ_SSOTestController()
    {
        LocalizationResource = typeof(AZ_SSOTestResource);
    }
}
