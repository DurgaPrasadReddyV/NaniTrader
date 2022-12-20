using NaniTrader.Exchanges.Securities.Equities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.Exchanges.Interfaces
{
    public interface IIndexRepository : IReadOnlyRepository<Securities.Equities.Index, Guid>
    {
        Task<List<Securities.Equities.Index>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
    }
}
