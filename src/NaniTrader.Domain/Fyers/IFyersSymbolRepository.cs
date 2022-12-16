using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.Fyers
{
    public interface IFyersSymbolRepository : IRepository<FyersSymbol, Guid>
    {
        Task<List<FyersSymbol>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );

        Task<List<string>> GetUnderlyingSymbolsAsync();
        Task<List<decimal>> GetStrikesAsync(string underlyingSymbol);
        Task<List<DateTimeOffset>> GetExpiryDatesAsync(string underlyingSymbol);
        Task<List<FyersSymbol>> GetOptionSymbolsAsync(string underlyingSymbol);
    }
}
