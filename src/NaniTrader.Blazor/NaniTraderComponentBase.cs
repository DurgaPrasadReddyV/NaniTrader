using NaniTrader.Localization;
using Volo.Abp.AspNetCore.Components;

namespace NaniTrader.Blazor;

public abstract class NaniTraderComponentBase : AbpComponentBase
{
    protected NaniTraderComponentBase()
    {
        LocalizationResource = typeof(NaniTraderResource);
    }
}
