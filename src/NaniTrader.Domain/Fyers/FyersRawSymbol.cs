using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NaniTrader.Fyers
{
    public class FyersRawSymbol : FullAuditedAggregateRoot<Guid>
    {
        public FyersRawSymbol(string exchange,
            string column1,
            string column2,
            string column3,
            string column4,
            string column5,
            string column6,
            string column7,
            string column8,
            string column9,
            string column10,
            string column11,
            string column12,
            string column13,
            string column14,
            string column15,
            string column16,
            string column17,
            string column18)
        {
            Exchange = exchange;
            Column1 = column1;
            Column2 = column2;
            Column3 = column3;
            Column4 = column4;
            Column5 = column5;
            Column6 = column6;
            Column7 = column7;
            Column8 = column8;
            Column9 = column9;
            Column10 = column10;
            Column11 = column11;
            Column12 = column12;
            Column13 = column13;
            Column14 = column14;
            Column15 = column15;
            Column16 = column16;
            Column17 = column17;
            Column18 = column18;
        }

        public string Exchange { get; private set; } // Exchange
        public string Column1 { get; private set; } // SymbolLongId
        public string Column2 { get; private set; } // Symbol Description
        public string Column3 { get; private set; } //
        public string Column4 { get; private set; } // Lot Size
        public string Column5 { get; private set; } // Minimum price
        public string Column6 { get; private set; } //
        public string Column7 { get; private set; } // Trading time window
        public string Column8 { get; private set; } // Updated date
        public string Column9 { get; private set; } //
        public string Column10 { get; private set; } // Fyers Trade Symbol
        public string Column11 { get; private set; } //
        public string Column12 { get; private set; } //
        public string Column13 { get; private set; } // SymbolShortId
        public string Column14 { get; private set; } // Underlying Symbol
        public string Column15 { get; private set; } // UnderlyingSymbolShortId
        public string Column16 { get; private set; } // Strike
        public string Column17 { get; private set; } // OptionRight (CE/PE/XX)
        public string Column18 { get; private set; } // UnderlyingSymbolLongId
    }
}
