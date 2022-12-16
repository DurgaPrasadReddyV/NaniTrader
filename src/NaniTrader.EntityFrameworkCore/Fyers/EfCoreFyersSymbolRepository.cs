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
    public class EfCoreFyersSymbolRepository
        : EfCoreRepository<NaniTraderDbContext, FyersSymbol, Guid>,
            IFyersSymbolRepository
    {
        public EfCoreFyersSymbolRepository(IDbContextProvider<NaniTraderDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<FyersSymbol>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(!filter.IsNullOrWhiteSpace(), fyersCredentials => fyersCredentials.Description.Contains(filter))
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<List<string>> GetUnderlyingSymbolsAsync()
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .GroupBy(x => x.UnderlyingName)
                .Select(x => x.FirstOrDefault().UnderlyingName)
                .ToListAsync();
        }

        public async Task<List<decimal>> GetStrikesAsync(string underlyingSymbol)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(x => x.UnderlyingName == underlyingSymbol)
                .Where(x => x.OptionRight == OptionRight.PE || x.OptionRight == OptionRight.CE)
                .GroupBy(x => x.StrikePrice)
                .Select(x => x.FirstOrDefault().StrikePrice.Amount)
                .ToListAsync();
        }

        public async Task<List<DateTimeOffset>> GetExpiryDatesAsync(string underlyingSymbol)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(x => x.UnderlyingName == underlyingSymbol)
                .Where(x => x.OptionRight == OptionRight.PE || x.OptionRight == OptionRight.CE)
                .GroupBy(x => x.ExpiryTime)
                .Select(x => x.FirstOrDefault().ExpiryTime)
                .ToListAsync();
        }

        public async Task<List<FyersSymbol>> GetOptionSymbolsAsync(string underlyingSymbol)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(x => x.UnderlyingName == underlyingSymbol)
                .Where(x => x.OptionRight == OptionRight.PE || x.OptionRight == OptionRight.CE)
                .ToListAsync();
        }
    }
}
