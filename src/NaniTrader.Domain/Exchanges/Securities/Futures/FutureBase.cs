using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaniTrader.Exchanges.Securities.Futures
{
    public class FutureBase : SecurityBase
    {
        public Guid UnderlyingId { get; private set; }
        public DateTime ExpiryTime { get; private set; }
    }
}
