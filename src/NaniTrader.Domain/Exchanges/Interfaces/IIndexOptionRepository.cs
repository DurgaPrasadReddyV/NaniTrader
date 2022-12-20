using NaniTrader.Exchanges.Securities.Equities;
using NaniTrader.Exchanges.Securities.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.Exchanges.Interfaces
{
    public interface IIndexOptionRepository : IReadOnlyRepository<IndexOption, Guid>
    {
        Task<List<IndexOption>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
    }
}
