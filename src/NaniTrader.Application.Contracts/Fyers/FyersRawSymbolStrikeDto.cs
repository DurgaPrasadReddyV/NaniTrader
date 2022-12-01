﻿using System;
using Volo.Abp.Application.Dtos;

namespace NaniTrader.Fyers
{
    public class FyersRawSymbolStrikeDto : EntityDto<Guid>
    {
        public string StrikePrice { get; set; }
        public string CESymbol { get; set; }
        public string PESymbol { get; set; }
    }
}
