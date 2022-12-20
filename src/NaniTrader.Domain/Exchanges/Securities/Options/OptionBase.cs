using NaniTrader.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaniTrader.Exchanges.Securities.Options
{
    public class OptionBase : SecurityBase
    {
        public Guid UnderlyingId { get; private set; }
        public DateTime ExpiryTime { get; private set; }
        public Currency StrikePrice { get; private set; }
        public OptionRight OptionRight { get; private set; }
    }
}
