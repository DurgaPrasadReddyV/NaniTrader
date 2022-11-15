using NaniTrader.Localization;
using Volo.Abp.Application.Services;

namespace NaniTrader;

/* Inherit your application services from this class.
 */
public abstract class NaniTraderAppService : ApplicationService
{
    protected NaniTraderAppService()
    {
        LocalizationResource = typeof(NaniTraderResource);
    }
}
