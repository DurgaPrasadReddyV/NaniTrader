using System.Collections.Generic;

namespace NaniTrader.Fyers
{
    public class OptionChainDto
    {
        public string Underlying { get; set; }
        public List<OptionSymbolDto> OptionSymbolDtos { get; set; }
    }
}
