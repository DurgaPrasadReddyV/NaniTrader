using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NaniTrader.Fyers
{
    public interface IFyersRawSymbolAppService : IApplicationService
    {
        Task CheckSymbolsAsync();
        Task DownloadNewSymbolsAsync();
        Task UpdateExistingSymbolsAsync();
        Task DeleteExpiredSymbolsAsync();
        Task<FyersRawSymbolDto> GetAsync(Guid id);
        Task<PagedResultDto<FyersRawSymbolDto>> GetListAsync(GetFyersRawSymbolListDto input);
    }
}
