using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NaniTrader.Brokers.Fyers
{
    public interface IFyersSymbolAppService : IApplicationService
    {
        Task ReviewSymbolsAsync();
        Task SynchronizeSymbolsAsync();
        Task<FyersSymbolDto> GetAsync(Guid id);
        Task<PagedResultDto<FyersSymbolDto>> GetListAsync(GetFyersSymbolListDto input);
    }
}
