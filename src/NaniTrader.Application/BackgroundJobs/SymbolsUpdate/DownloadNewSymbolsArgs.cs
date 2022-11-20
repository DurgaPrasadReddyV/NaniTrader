using NaniTrader.Fyers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;

namespace NaniTrader.BackgroundJobs.SymbolsUpdate
{
    public class DownloadNewSymbolsArgs
    {
        public string Exchange { get; set; }
    }
}
