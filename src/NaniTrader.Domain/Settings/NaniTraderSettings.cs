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

    public static class Exchanges
    {
        public const string SecuritiesProvider = Prefix + "Exchanges.SecuritiesProvider";
    }

    public static class MarketData
    {
        public const string TouchlineDataProvider = Prefix + "MarketData.TouchlineDataProvider";
    }
}
