using System;
using System.Collections.Generic;
using System.Text;
using AZ_SSOTest.Localization;
using Volo.Abp.Application.Services;

namespace AZ_SSOTest;

/* Inherit your application services from this class.
 */
public abstract class AZ_SSOTestAppService : ApplicationService
{
    protected AZ_SSOTestAppService()
    {
        LocalizationResource = typeof(AZ_SSOTestResource);
    }
}
