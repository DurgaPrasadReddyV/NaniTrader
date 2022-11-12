using Microsoft.EntityFrameworkCore;
using NaniTrader.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NaniTrader.Fyers
{
    public class EfCoreFyersCredentialsRepository
        : EfCoreRepository<NaniTraderDbContext, FyersCredentials, Guid>,
            IFyersCredentialsRepository
    {
        public EfCoreFyersCredentialsRepository(
            IDbContextProvider<NaniTraderDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<FyersCredentials> FindByAppIdAsync(string appId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(fyersCredentials => fyersCredentials.AppId == appId);
        }

        public async Task<List<FyersCredentials>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    fyersCredentials => fyersCredentials.AppId.Contains(filter)
                )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
