using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaniTrader.Exchanges.Securities.Equities
{
    public class Equity : SecurityBase
    {
        public string ISIN { get; private set; }

        private Equity()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
    }
}

