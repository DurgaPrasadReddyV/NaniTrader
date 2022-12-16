using System;
using Volo.Abp.Application.Dtos;

namespace NaniTrader.Fyers
{
    public class FyersSymbolStrikeDto : EntityDto<Guid>
    {
        public string StrikePrice { get; set; }
        public string CESymbol { get; set; }
        public double CEPrice { get; set; }
        public string PESymbol { get; set; }
        public double PEPrice { get; set; }
    }
}
