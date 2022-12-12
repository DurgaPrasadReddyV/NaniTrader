using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.Fyers
{
    public interface IFyersRawSymbolRepository : IRepository<FyersRawSymbol, Guid>
    {
        Task<List<FyersRawSymbol>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );

        Task<List<string>> GetUnderlyingSymbolsAsync();
        Task<List<string>> GetStrikesAsync(string underlyingSymbol);
        Task<List<string>> GetExpiryDatesAsync(string underlyingSymbol);
        Task<List<FyersRawSymbol>> GetOptionSymbolsAsync(string underlyingSymbol);
    }
}
