using NaniTrader.Exchanges.Securities.Equities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.Exchanges.Interfaces
{
    public interface IExchangeRepository : IRepository<Exchange, Guid>
    {
        Task<List<Exchange>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
    }
}
