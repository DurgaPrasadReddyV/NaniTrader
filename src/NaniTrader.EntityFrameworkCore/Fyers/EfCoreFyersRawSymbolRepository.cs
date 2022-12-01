using Microsoft.EntityFrameworkCore;
using NaniTrader.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.SettingManagement;

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
                .WhereIf(!filter.IsNullOrWhiteSpace(), fyersCredentials => fyersCredentials.Exchange.Contains(filter))
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<List<string>> GetUnderlyingSymbolsAsync()
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .OrderBy(x => x.Column14)
                .GroupBy(x => x.Column14)
                .Select(x => x.FirstOrDefault().Column14)
                .ToListAsync();
        }

        public async Task<List<string>> GetStrikesAsync(string underlyingSymbol)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(x => x.Column14 == underlyingSymbol)
                .OrderBy(x => x.Column16)
                .GroupBy(x => x.Column16)
                .Select(x => x.FirstOrDefault().Column16)
                .ToListAsync();
        }

        public async Task<List<string>> GetExpiryDatesAsync(string underlyingSymbol)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(x => x.Column14 == underlyingSymbol)
                .OrderBy(x => x.Column9)
                .GroupBy(x => x.Column9)
                .Select(x => x.FirstOrDefault().Column9)
                .ToListAsync();
        }

        public async Task<List<FyersRawSymbol>> GetPESymbolsForExpiryAsync(string underlyingSymbol, string expiry)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(x => x.Column14 == underlyingSymbol)
                .Where(x => x.Column9 == expiry)
                .Where(x => x.Column17 == "PE")
                .ToListAsync();
        }

        public async Task<List<FyersRawSymbol>> GetCESymbolsForExpiryAsync(string underlyingSymbol, string expiry)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(x => x.Column14 == underlyingSymbol)
                .Where(x => x.Column9 == expiry)
                .Where(x => x.Column17 == "CE")
                .ToListAsync();
        }
    }
}
