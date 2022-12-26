using System;
using Volo.Abp.Application.Dtos;

namespace NaniTrader.Exchanges
{
    public class ExchangeDto : EntityDto<Guid>
    {
        public string ExchangeIdentifier { get; set; }
        public string Description { get; set; }
        public string SecuritiesProvider { get; set; }
    }
}
