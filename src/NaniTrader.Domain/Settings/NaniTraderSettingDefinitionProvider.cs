using NaniTrader.Localization;
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
    }
}
