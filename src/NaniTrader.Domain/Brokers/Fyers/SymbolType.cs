using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaniTrader.Brokers.Fyers
{
    public enum SymbolType
    {
        UNKNOWN = 0,
        EQUITY = 1,
        ETF = 2,
        INDEX = 3,
        INDEX_FUTURE = 4,
        INDEX_OPTION = 5,
        EQUITY_FUTURE = 6,
        EQUITY_OPTION = 7,
    }
}
