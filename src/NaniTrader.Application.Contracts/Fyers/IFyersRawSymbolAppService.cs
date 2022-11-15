using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NaniTrader.Fyers
{
    public interface IFyersRawSymbolAppService : IApplicationService
    {
        Task CreateAsync();
        Task UpdateAsync();
        Task DeleteAsync(Guid id);
        Task<FyersRawSymbolDto> GetAsync(Guid id);
        Task<PagedResultDto<FyersRawSymbolDto>> GetListAsync(GetFyersRawSymbolListDto input);
    }
}
