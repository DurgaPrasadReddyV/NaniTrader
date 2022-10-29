using NaniTrader.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace NaniTrader.Permissions;

public class NaniTraderPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(NaniTraderPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(NaniTraderPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<NaniTraderResource>(name);
    }
}
