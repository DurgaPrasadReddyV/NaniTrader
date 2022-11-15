using Microsoft.EntityFrameworkCore;
using NaniTrader.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NaniTrader.Fyers
{
    public class EfCoreFyersRawSymbolRepository
        : EfCoreRepository<NaniTraderDbContext, FyersRawSymbol, Guid>,
            IFyersRawSymbolRepository
    {
        public EfCoreFyersRawSymbolRepository(
            IDbContextProvider<NaniTraderDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<FyersRawSymbol>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    fyersCredentials => fyersCredentials.Exchange.Contains(filter)
                )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
