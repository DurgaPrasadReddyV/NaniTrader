using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NaniTrader.MarketData
{
    public class Candle
    {
        public long t { get; set; }

        public double o { get; set; }

        public double h { get; set; }

        public double l { get; set; }

        public double c { get; set; }

        public long v { get; set; }

        public string tf { get; set; }
    }
}
