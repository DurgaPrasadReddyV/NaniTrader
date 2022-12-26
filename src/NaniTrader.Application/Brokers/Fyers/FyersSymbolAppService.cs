using FyersAPI;
using Microsoft.AspNetCore.Authorization;
using MoreLinq;
using NaniTrader.ApiClients;
using NaniTrader.BackgroundJobs.FyersSymbols;
using NaniTrader.Brokers.Fyers;
using NaniTrader.Brokers.Fyers.Interfaces;
using NaniTrader.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.Brokers.Fyers
{
    [Authorize(NaniTraderPermissions.FyersSymbols.Default)]
    public class FyersSymbolAppService : NaniTraderAppService, IFyersSymbolAppService
    {
        private readonly IFyersSymbolRepository _fyersSymbolRepository;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public FyersSymbolAppService(IFyersSymbolRepository fyersSymbolRepository, IBackgroundJobManager backgroundJobManager)
        {
            _fyersSymbolRepository = fyersSymbolRepository;
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task<FyersSymbolDto> GetAsync(Guid id)
        {
            var fyersSymbol = await _fyersSymbolRepository.GetAsync(id);
            return ObjectMapper.Map<FyersSymbol, FyersSymbolDto>(fyersSymbol);
        }

        public async Task<PagedResultDto<FyersSymbolDto>> GetListAsync(GetFyersSymbolListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(FyersSymbol.Exchange);
            }

            var fyersSymbols = await _fyersSymbolRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);

            var totalCount = input.Filter == null
                ? await _fyersSymbolRepository.CountAsync()
                : await _fyersSymbolRepository.CountAsync(fyersCredentials => fyersCredentials.Description.Contains(input.Filter));

            return new PagedResultDto<FyersSymbolDto>(totalCount, ObjectMapper.Map<List<FyersSymbol>, List<FyersSymbolDto>>(fyersSymbols));
        }

        public async Task ReviewSymbolsAsync()
        {
            await Task.CompletedTask;
        }

        public async Task SynchronizeSymbolsAsync()
        {
            await _backgroundJobManager.EnqueueAsync(new SynchronizeSymbolsArgs { Exchange = "NSE_CM" });
            await _backgroundJobManager.EnqueueAsync(new SynchronizeSymbolsArgs { Exchange = "NSE_FO" });

        }
    }
}
