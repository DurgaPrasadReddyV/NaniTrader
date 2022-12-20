using NaniTrader.Exchanges.Securities.Equities;
using NaniTrader.Exchanges.Securities.Futures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.Exchanges.Interfaces
{
    public interface IEquityFutureRepository : IReadOnlyRepository<EquityFuture, Guid>
    {
        Task<List<EquityFuture>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
    }
}
