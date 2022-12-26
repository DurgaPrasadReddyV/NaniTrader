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
    public class EfCoreExchangeRepository : EfCoreRepository<NaniTraderDbContext, Exchange, Guid>, IExchangeRepository
    {
        public EfCoreExchangeRepository(IDbContextProvider<NaniTraderDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Exchange> FindByExchangeIdAsync(ExchangeIdentifier exchangeIdentifier)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(x => x.ExchangeIdentifier == exchangeIdentifier);
        }

        public async Task<List<Exchange>> GetListAsync(
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
