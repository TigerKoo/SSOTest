using AZ_SSOTest.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace AZ_SSOTest.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class AZ_SSOTestPageModel : AbpPageModel
{
    protected AZ_SSOTestPageModel()
    {
        LocalizationResourceType = typeof(AZ_SSOTestResource);
    }
}
