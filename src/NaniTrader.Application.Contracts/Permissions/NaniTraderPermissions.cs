namespace NaniTrader.Permissions;

public static class NaniTraderPermissions
{
    public const string GroupName = "NaniTrader";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class FyersCredentials
    {
        public const string Default = GroupName + ".FyersCredentials";
    }

    public static class FyersRawSymbols
    {
        public const string Default = GroupName + ".FyersRawSymbols";
    }
}
