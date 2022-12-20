using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Domain.Entities.Auditing;

namespace NaniTrader.MarketData
{
    public class Touchline : FullAuditedAggregateRoot<Guid>
    {
        public double ch { get; set; }

        public double chp { get; set; }

        public double lp { get; set; }

        public double spread { get; set; }

        public double ask { get; set; }

        public double bid { get; set; }

        public double open_price { get; set; }

        public double high_price { get; set; }

        public double low_price { get; set; }

        public double prev_close_price { get; set; }

        public long volume { get; set; }

        public string short_name { get; set; }

        public string exchange { get; set; }

        public string description { get; set; }

        public string original_name { get; set; }

        public string symbol { get; set; }

        public string fyToken { get; set; }

        public long tt { get; set; }

        public Candle cmd { get; set; }

        private Touchline()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
    }
}
