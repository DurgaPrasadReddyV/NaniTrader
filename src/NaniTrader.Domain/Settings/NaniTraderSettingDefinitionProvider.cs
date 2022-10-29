using Volo.Abp.Settings;

namespace NaniTrader.Settings;

public class NaniTraderSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(NaniTraderSettings.MySetting1));
    }
}
