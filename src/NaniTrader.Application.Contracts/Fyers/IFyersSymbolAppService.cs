using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NaniTrader.Fyers
{
    public interface IFyersSymbolAppService : IApplicationService
    {
        Task CheckSymbolsAsync();
        Task DownloadNewSymbolsAsync();
        Task UpdateExistingSymbolsAsync();
        Task DeleteExpiredSymbolsAsync();
        Task<FyersSymbolDto> GetAsync(Guid id);
        Task<PagedResultDto<FyersSymbolDto>> GetListAsync(GetFyersSymbolListDto input);
        Task<List<string>> GetUnderlyingSymbolsAsync();
        Task<List<decimal>> GetStrikesAsync(string underlyingSymbol);
        Task<List<DateTimeOffset>> GetExpiryDatesAsync(string underlyingSymbol);
        Task<OptionChainDto> GetOptionChainAsync(string underlyingSymbol);
    }
}
