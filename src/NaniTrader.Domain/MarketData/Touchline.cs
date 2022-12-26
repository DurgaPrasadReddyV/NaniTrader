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
        public Guid ExchangeSecurityId { get; private set; }
        public TouchlineDataProvider TouchlineDataProvider { get; private set; }
        
        public DateTime RecordedTime { get; private set; }
        
        public double LastTradedPrice { get; private set; }

        public double Spread { get; private set; }

        public double Ask { get; set; }

        public double Bid { get; set; }

        public double OpenPrice { get; set; }

        public double HighPrice { get; set; }

        public double LowPrice { get; set; }

        public double PreviousClosePrice { get; set; }

        public long Volume { get; set; }

        private Touchline()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
    }
}
