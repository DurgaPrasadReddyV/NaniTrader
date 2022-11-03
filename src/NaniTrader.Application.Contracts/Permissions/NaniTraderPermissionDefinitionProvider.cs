using NaniTrader.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace NaniTrader.Permissions;

public class NaniTraderPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var bookStoreGroup = context.AddGroup(NaniTraderPermissions.GroupName, L("Permission:BookStore"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(NaniTraderPermissions.MyPermission1, L("Permission:MyPermission1"));

        var booksPermission = bookStoreGroup.AddPermission(NaniTraderPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(NaniTraderPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(NaniTraderPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(NaniTraderPermissions.Books.Delete, L("Permission:Books.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<NaniTraderResource>(name);
    }
}
