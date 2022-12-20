using NaniTrader.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace NaniTrader.Exchanges.Securities
{
    public class SecurityBase : FullAuditedEntity<Guid>
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public int LotSize { get; private set; }

        public Currency PriceStep { get; private set; }
    }
}
