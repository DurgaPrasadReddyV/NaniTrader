using NaniTrader.Exchanges.Securities.Equities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.Exchanges.Interfaces
{
    public interface IEquityRepository : IReadOnlyRepository<Equity, Guid>
    {
        Task<List<Equity>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
    }
}
