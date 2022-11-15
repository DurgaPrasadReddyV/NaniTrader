using NaniTrader.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace NaniTrader.Permissions;

public class NaniTraderPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var naniTraderGroup = context.AddGroup(NaniTraderPermissions.GroupName, L("Permissions:NaniTrader"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(NaniTraderPermissions.MyPermission1, L("Permission:MyPermission1"));

        _ = naniTraderGroup.AddPermission(NaniTraderPermissions.FyersCredentials.Default, L("Permissions:NaniTrader:FyersCredentials"));
        _ = naniTraderGroup.AddPermission(NaniTraderPermissions.FyersRawSymbols.Default, L("Permissions:NaniTrader:FyersRawSymbols"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<NaniTraderResource>(name);
    }
}
