using System;
using NaniTrader.Currencies;
using NaniTrader.Exchanges;
using NaniTrader.Exchanges.Interfaces;
using NaniTrader.Exchanges.Securities.Options;
using Volo.Abp.Domain.Entities.Auditing;

namespace NaniTrader.Brokers.Fyers
{
    public class FyersSymbol : FullAuditedAggregateRoot<Guid>, IExchangeSecurityProperties
    {
        public FyersSymbol(long symbolId,
            long symbolLongId,
            long underlyingId,
            long underlyingLongId,
            string symbolName,
            string underlyingName,
            string description,
            ExchangeIdentifier exchange,
            SymbolType symbolType,
            int lotSize,
            Currency priceStep,
            string isin,
            string timeWindow,
            DateTime updatedTime,
            DateTime expiryTime,
            Currency strikePrice,
            OptionRight optionRight,
            string column11)
        {
            SymbolId = symbolId;
            SymbolLongId = symbolLongId;
            UnderlyingId = underlyingId;
            UnderlyingLongId = underlyingLongId;
            SymbolName = symbolName;
            UnderlyingName = underlyingName;
            Description = description;
            Exchange = exchange;
            SymbolType = symbolType;
            LotSize = lotSize;
            PriceStep = priceStep;
            ISIN = isin;
            TimeWindow = timeWindow;
            UpdatedTime = updatedTime;
            ExpiryTime = expiryTime;
            StrikePrice = strikePrice;
            OptionRight = optionRight;
            Column11 = column11;
        }

        private FyersSymbol()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        public long SymbolId { get; private set; }
        public long SymbolLongId { get; private set; }
        public string SymbolName { get; private set; }
        public long UnderlyingId { get; private set; }
        public long UnderlyingLongId { get; private set; }
        public string UnderlyingName { get; private set; }
        public string Description { get; private set; }
        public ExchangeIdentifier Exchange { get; private set; }
        public SymbolType SymbolType { get; private set; }
        public int LotSize { get; private set; }
        public Currency PriceStep { get; private set; }
        public string ISIN { get; private set; }
        public string TimeWindow { get; private set; }
        public DateTime UpdatedTime { get; private set; }
        public DateTime ExpiryTime { get; private set; }
        public Currency StrikePrice { get; private set; }
        public OptionRight OptionRight { get; private set; }
        public string Column11 { get; private set; } //Unknown
        public Guid ExchangeSecurityId { get; private set; }
    }
}
