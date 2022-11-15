using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NaniTrader.Fyers
{
    public class FyersRawSymbol : FullAuditedAggregateRoot<Guid>
    {
        public string Exchange { get; private set; }
        public string Column1 { get; private set; }
        public string Column2 { get; private set; }
        public string Column3 { get; private set; }
        public string Column4 { get; private set; }
        public string Column5 { get; private set; }
        public string Column6 { get; private set; }
        public string Column7 { get; private set; }
        public string Column8 { get; private set; }
        public string Column9 { get; private set; }
        public string Column10 { get; private set; }
        public string Column11 { get; private set; }
        public string Column12 { get; private set; }
        public string Column13 { get; private set; }
        public string Column14 { get; private set; }
        public string Column15 { get; private set; }
        public string Column16 { get; private set; }
        public string Column17 { get; private set; }
        public string Column18 { get; private set; }
    }
}
