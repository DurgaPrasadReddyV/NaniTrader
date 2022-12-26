using NaniTrader.Exchanges.Securities;
using NaniTrader.Exchanges.Securities.Equities;
using NaniTrader.Exchanges.Securities.Futures;
using NaniTrader.Exchanges.Securities.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;

namespace NaniTrader.Exchanges
{
    public class Exchange : FullAuditedAggregateRoot<Guid>
    {
        public ExchangeIdentifier ExchangeIdentifier { get; private set; }
        public SecuritiesProvider SecuritiesProvider { get; private set; }
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

        internal Exchange(Guid id, ExchangeIdentifier exchangeIdentifier, SecuritiesProvider securitiesProvider, string description)
            : base(id)
        {
            ExchangeIdentifier = exchangeIdentifier;
            SecuritiesProvider = securitiesProvider;
            Description = description;
            Equities = new List<Equity>();
            ETFs = new List<ETF>();
            Indexes = new List<Securities.Equities.Index>();
            EquityFutures = new List<EquityFuture>();
            EquityOptions = new List<EquityOption>();
            IndexFutures = new List<IndexFuture>();
            IndexOptions = new List<IndexOption>();
        }

        public void AddEquity(Equity equity)
        {
            var existingEquity = Equities.FirstOrDefault(eq => eq.Name == equity.Name);

            if (existingEquity is not null)
                throw new InvalidOperationException();

            (Equities as List<Equity>).Add(equity);
        }

        public void AddIndex(Securities.Equities.Index index)
        {
            var existingIndex = Indexes.FirstOrDefault(idx => idx.Name == index.Name);

            if (existingIndex is not null)
                throw new InvalidOperationException();

            (Indexes as List<Securities.Equities.Index>).Add(index);
        }

        public void AddETF(ETF etf)
        {
            var existingETF = ETFs.FirstOrDefault(et => et.Name == etf.Name);

            if (existingETF is not null)
                throw new InvalidOperationException();

            (ETFs as List<ETF>).Add(etf);
        }

        public void AddEquityOption(EquityOption equityOption)
        {
            var existingEquityOption = EquityOptions.FirstOrDefault(eqOp => eqOp.Name == equityOption.Name);

            if (existingEquityOption is not null)
                throw new InvalidOperationException();

            (EquityOptions as List<EquityOption>).Add(equityOption);
        }

        public void AddEquityFuture(EquityFuture equityFuture)
        {
            var existingEquityFuture = EquityFutures.FirstOrDefault(eqFut => eqFut.Name == equityFuture.Name);

            if (existingEquityFuture is not null)
                throw new InvalidOperationException();

            (EquityFutures as List<EquityFuture>).Add(equityFuture);
        }

        public void AddIndexOption(IndexOption indexOption)
        {
            var existingIndexOption = IndexOptions.FirstOrDefault(idxOp => idxOp.Name == indexOption.Name);

            if (existingIndexOption is not null)
                throw new InvalidOperationException();

            (IndexOptions as List<IndexOption>).Add(indexOption);
        }

        public void AddIndexFuture(IndexFuture indexFuture)
        {
            var existingIndexFuture = IndexFutures.FirstOrDefault(idxFut => idxFut.Name == indexFuture.Name);

            if (existingIndexFuture is not null)
                throw new InvalidOperationException();

            (IndexFutures as List<IndexFuture>).Add(indexFuture);
        }

    }
}
