using NaniTrader.Exchanges.Securities;
using NaniTrader.Localization;
using NaniTrader.MarketData;
using Volo.Abp.Emailing;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace NaniTrader.Settings;

public class NaniTraderSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(NaniTraderSettings.MySetting1));

        context.Add(new SettingDefinition(NaniTraderSettings.Brokers.Fyers.RedirectUri,
            "https://localhost:44380/callback/fyers",
            new LocalizableString(typeof(NaniTraderResource), "DisplayName:NaniTraderSettings.Brokers.Fyers.RedirectUri"),
            new LocalizableString(typeof(NaniTraderResource), "Description:NaniTraderSettings.Brokers.Fyers.RedirectUri")));

        context.Add(new SettingDefinition(NaniTraderSettings.Exchanges.SecuritiesProvider,
            SecuritiesProvider.BROKER_FYERS.ToString(),
            new LocalizableString(typeof(NaniTraderResource), "DisplayName:NaniTraderSettings.Exchanges.SecuritiesProvider"),
            new LocalizableString(typeof(NaniTraderResource), "Description:NaniTraderSettings.Exchanges.SecuritiesProvider")));

        context.Add(new SettingDefinition(NaniTraderSettings.MarketData.TouchlineDataProvider,
            TouchlineDataProvider.BROKER_FYERS.ToString(),
            new LocalizableString(typeof(NaniTraderResource), "DisplayName:NaniTraderSettings.MarketData.TouchlineDataProvider"),
            new LocalizableString(typeof(NaniTraderResource), "Description:NaniTraderSettings.MarketData.TouchlineDataProvider")));
    }
}
