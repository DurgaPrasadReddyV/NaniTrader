using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace NaniTrader.Fyers
{
    public class FyersSymbolCsvMap
    {
        public long Column1 { get; set; } // SymbolLongId
        public string Column2 { get; set; } // Symbol Description
        public int Column3 { get; set; } // Instrument type: 0 = equity, 4 = etf, 10 = index, 11 = index future , 13 = equity future, 14 = index option, 15 = equity option
        public int Column4 { get; set; } // Lot Size
        public decimal Column5 { get; set; } // Price steps in rupee
        public string Column6 { get; set; } // ISIN Number
        public string Column7 { get; set; } // Trading time window
        public string Column8 { get; set; } // Updated date
        public long Column9 { get; set; } // Expiry time in epoch
        public string Column10 { get; set; } // Fyers Trade Symbol
        public string Column11 { get; set; } //
        public int Column12 { get; set; } // Exchange Type: 10 = Cash, 11 = FNO
        public long Column13 { get; set; } // SymbolShortId
        public string Column14 { get; set; } // Underlying Symbol
        public long Column15 { get; set; } // UnderlyingSymbolShortId
        public decimal Column16 { get; set; } // Strike
        public string Column17 { get; set; } // OptionRight (CE/PE/XX)
        public long Column18 { get; set; } // UnderlyingSymbolLongId
    }
}
