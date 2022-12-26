using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace NaniTrader.Exchanges
{
    public interface IExchangeAppService : IApplicationService
    {
        Task<ExchangeDto> GetByExchangeIdentifierAsync(string exchangeIdentifier);
        Task<ExchangeDto> ConfigureAsync(ConfigureExchangeDto input);
        Task ReconfigureAsync(ReconfigureExchangeDto input);
        Task RemoveAsync(Guid id);
    }
}