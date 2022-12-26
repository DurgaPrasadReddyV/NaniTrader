using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NaniTrader.Brokers.Fyers
{
    public interface IFyersCredentialsAppService : IApplicationService
    {
        Task<FyersCredentialsDto> ConfigureAsync(ConfigureFyersCredentialsDto input);
        Task ReconfigureAsync(Guid id, ReconfigureFyersCredentialsDto input);
        Task RemoveAsync(Guid id);
        Task GenerateTokenAsync(string fyersApp, string authCode);
        Task<FyersCredentialsDto> GetCurrentUserAsync();
    }
}
