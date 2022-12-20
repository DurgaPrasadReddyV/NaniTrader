using Microsoft.EntityFrameworkCore;
using NaniTrader.EntityFrameworkCore;
using NaniTrader.Exchanges.Interfaces;
using NaniTrader.Exchanges.Securities.Equities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NaniTrader.Exchanges
{
    public class EfCoreIndexRepository : EfCoreRepository<NaniTraderDbContext, Securities.Equities.Index, Guid>, IIndexRepository
    {
        public EfCoreIndexRepository(IDbContextProvider<NaniTraderDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Securities.Equities.Index>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    equity => equity.Description.Contains(filter)
                )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
