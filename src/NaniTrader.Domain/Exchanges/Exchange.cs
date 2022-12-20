using NaniTrader.Exchanges.Securities.Equities;
using NaniTrader.Exchanges.Securities.Futures;
using NaniTrader.Exchanges.Securities.Options;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace NaniTrader.Exchanges
{
    public class Exchange : FullAuditedAggregateRoot<Guid>
    {
        public ExchangeIdentifier ExchangeIdentifier { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<Equity> Equities { get; private set; }
        public IEnumerable<ETF> ETFs { get; private set; }
        public IEnumerable<Securities.Equities.Index> Indexes { get; private set; }
        public IEnumerable<EquityFuture> EquityFutures { get; private set; }
        public IEnumerable<IndexFuture> IndexFutures { get; private set; }
        public IEnumerable<EquityOption> EquityOptions { get; private set; }
        public IEnumerable<IndexOption> IndexOptions { get; private set; }

        private Exchange()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

    }
}
