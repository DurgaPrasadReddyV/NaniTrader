using Microsoft.AspNetCore.Authorization;
using NaniTrader.ApiClients;
using NaniTrader.Brokers.Fyers;
using NaniTrader.Brokers.Fyers.Interfaces;
using NaniTrader.Exchanges.Interfaces;
using NaniTrader.Exchanges.Securities;
using NaniTrader.Permissions;
using NaniTrader.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Settings;

namespace NaniTrader.Exchanges
{
    [Authorize(NaniTraderPermissions.Exchanges.Default)]
    public class ExchangeAppService : NaniTraderAppService, IExchangeAppService
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly ExchangeManager _exchangeManager;

        public ExchangeAppService(IExchangeRepository exchangeRepository, ExchangeManager exchangeManager)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeManager = exchangeManager;
        }

        public async Task<ExchangeDto> GetByExchangeIdentifierAsync(string exchangeIdentifier)
        {
            var exchange = await _exchangeRepository.FindByExchangeIdAsync(Enum.Parse<ExchangeIdentifier>(exchangeIdentifier, true));

            return ObjectMapper.Map<Exchange, ExchangeDto>(exchange);
        }

        public async Task<ExchangeDto> ConfigureAsync(ConfigureExchangeDto input)
        {
            var exchangeIdentifier = Enum.Parse<ExchangeIdentifier>(input.ExchangeIdentifier, true);
            var existingExchange = await _exchangeRepository.FindByExchangeIdAsync(exchangeIdentifier);

            if (existingExchange is not null)
                throw new InvalidOperationException("Exchange is already configured.");

            var securitiesProvider = Enum.Parse<SecuritiesProvider>(input.SecuritiesProvider, true);

            Exchange exchange = _exchangeManager.Configure(exchangeIdentifier, securitiesProvider, input.Description);

            await _exchangeRepository.InsertAsync(exchange);

            return ObjectMapper.Map<Exchange, ExchangeDto>(exchange);
        }

        public async Task ReconfigureAsync(ReconfigureExchangeDto input)
        {
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid id)
        {
            await Task.CompletedTask;
        }
    }
}
