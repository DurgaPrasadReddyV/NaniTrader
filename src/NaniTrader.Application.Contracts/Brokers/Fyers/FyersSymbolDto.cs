using System;
using Volo.Abp.Application.Dtos;

namespace NaniTrader.Fyers
{
    public class FyersSymbolDto : EntityDto<Guid>
    {
        public long SymbolId { get; set; }
        public long SymbolLongId { get;  set; }
        public string SymbolName { get;  set; }
        public long UnderlyingId { get;  set; }
        public long UnderlyingLongId { get;  set; }
        public string UnderlyingName { get;  set; }
        public string Description { get;  set; }
        public string Exchange { get;  set; }
        public string SymbolType { get;  set; }
        public int LotSize { get;  set; }
        public decimal PriceStep { get;  set; }
        public string ISIN { get;  set; }
        public string TimeWindow { get;  set; }
        public DateTime UpdatedTime { get;  set; }
        public DateTime ExpiryTime { get;  set; }
        public decimal StrikePrice { get;  set; }
        public string OptionRight { get;  set; }
        public string Column11 { get;  set; } //Unknown
    }
}
