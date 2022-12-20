namespace NaniTrader.Settings;

public static class NaniTraderSettings
{
    private const string Prefix = "NaniTrader";

    //Add your own setting names here. Example:
    //public const string MySetting1 = Prefix + ".MySetting1";

    public static class Brokers
    {
        public static class Fyers
        {
            public const string RedirectUri = Prefix + "Brokers.Fyers.RedirectUri";
        }     
    }
}
