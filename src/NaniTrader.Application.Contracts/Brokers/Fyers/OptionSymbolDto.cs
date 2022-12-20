using System;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace NaniTrader.Fyers
{
    public class OptionSymbolDto
    {
        /// <summary>
        /// The ticker symbol of the Security for the Option.
        /// </summary>
        /// <value>The ticker symbol of the Security for the Option.</value>
        public string Ticker { get; set; }

        /// <summary>
        /// The date on which the Option expires. The Option becomes invalid after this date and cannot be exercised.
        /// </summary>
        /// <value>The date on which the Option expires. The Option becomes invalid after this date and cannot be exercised.</value>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// The strike price is the fixed price at which a derivative can be exercised, and refers to the price of the derivative’s underlying asset.  In a call option, the strike price is the price at which the option holder can purchase the underlying security.  For a put option, the strike price is the price at which the option holder can sell the underlying security.
        /// </summary>
        /// <value>The strike price is the fixed price at which a derivative can be exercised, and refers to the price of the derivative’s underlying asset.  In a call option, the strike price is the price at which the option holder can purchase the underlying security.  For a put option, the strike price is the price at which the option holder can sell the underlying security.</value>
        public decimal? Strike { get; set; }

        /// <summary>
        /// The type of Option (put or call). A put option is an option contract giving the owner the right, but not the obligation, to sell a specified amount of an underlying asset at a specified price before the option&#39;s expiration date. A call option gives the holder the right to buy an underlying asset at a specified price, before the option&#39;s expiration date.
        /// </summary>
        /// <value>The type of Option (put or call). A put option is an option contract giving the owner the right, but not the obligation, to sell a specified amount of an underlying asset at a specified price before the option&#39;s expiration date. A call option gives the holder the right to buy an underlying asset at a specified price, before the option&#39;s expiration date.</value>
        public string Type { get; set; }

        /// <summary>
        /// The price of the last trade
        /// </summary>
        /// <value>The price of the last trade</value>
        public decimal? Last { get; set; }

        /// <summary>
        /// The price of the top ask order
        /// </summary>
        /// <value>The price of the top ask order</value>
        public decimal? Ask { get; set; }

        /// <summary>
        /// The price of the top bid order
        /// </summary>
        /// <value>The price of the top bid order</value>
        public decimal? Bid { get; set; }

        /// <summary>
        /// The cumulative volume of this options contract that traded that day.
        /// </summary>
        /// <value>The cumulative volume of this options contract that traded that day.</value>
        public long? Volume { get; set; }

        /// <summary>
        /// The total number of this options contract that are still open.
        /// </summary>
        /// <value>The total number of this options contract that are still open.</value>
        public long? OpenInterest { get; set; }

        /// <summary>
        /// The change in the total number of this options contract that are still open from the previous day.
        /// </summary>
        /// <value>The change in the total number of this options contract that are still open from the previous day.</value>
        public long? OpenInterestChange { get; set; }

        /// <summary>
        /// The estimated volatility of the Security&#39;s price. Volatility is a statistical measure of dispersion of returns for the Security. Standard deviation of a Security&#39;s returns and a market index is an example of a measurement of volatility. Implied volatility approximates the future value of an option, and the option&#39;s current value takes this into consideration.
        /// </summary>
        /// <value>The estimated volatility of the Security&#39;s price. Volatility is a statistical measure of dispersion of returns for the Security. Standard deviation of a Security&#39;s returns and a market index is an example of a measurement of volatility. Implied volatility approximates the future value of an option, and the option&#39;s current value takes this into consideration.</value>
        public decimal? ImpliedVolatility { get; set; }

        /// <summary>
        /// The change in implied volatility for that day.
        /// </summary>
        /// <value>The change in implied volatility for that day.</value>
        public decimal? ImpliedVolatilityChange { get; set; }

        /// <summary>
        /// Delta measures the degree to which an options contract is exposed to shifts in the price of the underlying Security. Values of delta range from 0.0 to 1.0 for call options and -1.0 to 0.0 for put options. For example, if a put option has a delta of -0.50, if the price of the underlying Security increases by $1, the price of the put option will decrease by $0.50.
        /// </summary>
        /// <value>Delta measures the degree to which an options contract is exposed to shifts in the price of the underlying Security. Values of delta range from 0.0 to 1.0 for call options and -1.0 to 0.0 for put options. For example, if a put option has a delta of -0.50, if the price of the underlying Security increases by $1, the price of the put option will decrease by $0.50.</value>
        public decimal? Delta { get; set; }
    }
}
