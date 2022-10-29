using NaniTrader.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace NaniTrader.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class NaniTraderController : AbpControllerBase
{
    protected NaniTraderController()
    {
        LocalizationResource = typeof(NaniTraderResource);
    }
}
