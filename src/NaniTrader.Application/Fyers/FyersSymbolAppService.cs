using FyersAPI;
using Microsoft.AspNetCore.Authorization;
using MoreLinq;
using NaniTrader.ApiClients;
using NaniTrader.BackgroundJobs.SymbolsUpdate;
using NaniTrader.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.Fyers
{
    [Authorize(NaniTraderPermissions.FyersSymbols.Default)]
    public class FyersSymbolAppService : NaniTraderAppService, IFyersSymbolAppService
    {
        private readonly IFyersSymbolRepository _fyersSymbolRepository;
        private readonly IFyersCredentialsRepository _fyersCredentialsRepository;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly FyersApiClient _fyersApiClient;

        public FyersSymbolAppService(
            IFyersSymbolRepository fyersSymbolRepository,
            IFyersCredentialsRepository fyersCredentialsRepository,
            IBackgroundJobManager backgroundJobManager,
            FyersApiClient fyersApiClient)
        {
            _fyersSymbolRepository = fyersSymbolRepository;
            _fyersCredentialsRepository = fyersCredentialsRepository;
            _backgroundJobManager = backgroundJobManager;
            _fyersApiClient = fyersApiClient;
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

        public async Task CheckSymbolsAsync()
        {
            await Task.CompletedTask;
        }

        public async Task DownloadNewSymbolsAsync()
        {
            await _backgroundJobManager.EnqueueAsync(new DownloadNewSymbolsArgs { Exchange = "NSE_FO" });
            await _backgroundJobManager.EnqueueAsync(new DownloadNewSymbolsArgs { Exchange = "NSE_CM" });
        }

        public async Task UpdateExistingSymbolsAsync()
        {
            await _backgroundJobManager.EnqueueAsync(new UpdateExistingSymbolsArgs { Exchange = "NSE_FO" });
            await _backgroundJobManager.EnqueueAsync(new UpdateExistingSymbolsArgs { Exchange = "NSE_CM" });
        }

        public async Task DeleteExpiredSymbolsAsync()
        {
            await _backgroundJobManager.EnqueueAsync(new RemoveExpiredSymbolsArgs { Exchange = "NSE_FO" });
            await _backgroundJobManager.EnqueueAsync(new RemoveExpiredSymbolsArgs { Exchange = "NSE_CM" });
        }

        public async Task<List<string>> GetUnderlyingSymbolsAsync()
        {
            var symbols = await _fyersSymbolRepository.GetUnderlyingSymbolsAsync();

            return symbols.Where(x => x.Equals("NIFTY", StringComparison.OrdinalIgnoreCase) || x.Equals("BANKNIFTY", StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public async Task<List<DateTimeOffset>> GetExpiryDatesAsync(string underlyingSymbol)
        {
            return await _fyersSymbolRepository.GetExpiryDatesAsync(underlyingSymbol);
        }

        public async Task<List<decimal>> GetStrikesAsync(string underlyingSymbol)
        {
            return await _fyersSymbolRepository.GetStrikesAsync(underlyingSymbol);
        }

        public async Task<OptionChainDto> GetOptionChainAsync(string underlyingSymbol)
        {
            var optionChain = new OptionChainDto();
            optionChain.Underlying = underlyingSymbol;
            var optionSymbolDtos = new List<OptionSymbolDto>();
            var fyersCurrentUserCredentials = await _fyersCredentialsRepository.FindAsync(x => x.UserId == CurrentUser.Id.Value);
            var optionSymbols = await _fyersSymbolRepository.GetOptionSymbolsAsync(underlyingSymbol);
            var optionSymbolTickers = optionSymbols.Select(x => x.SymbolName).ToList();
            var optionQuotes = new List<Quote>();
            foreach (var optionSymbolTickersBatch in optionSymbolTickers.Batch(50))
            {
                var quotes = await _fyersApiClient.GetQuotesAsync(
                    optionSymbolTickersBatch.ToList(),
                    fyersCurrentUserCredentials.AppId,
                    fyersCurrentUserCredentials.Token);
                optionQuotes.AddRange(quotes);
            }

            foreach (var optionSymbolTicker in optionSymbolTickers)
            {
                var optionSymbolDto = new OptionSymbolDto();
                var optionSymbol = optionSymbols.FirstOrDefault(x => x.SymbolName == optionSymbolTicker);
                var optionQuote = optionQuotes.FirstOrDefault(x => x.n == optionSymbolTicker);
                optionSymbolDto.Ticker = optionSymbolTicker;
                optionSymbolDto.Strike = optionSymbol.StrikePrice.Amount;
                optionSymbolDto.Expiration = optionSymbol.ExpiryTime;
                optionSymbolDto.Type = optionSymbol.OptionRight.ToString();
                optionSymbolDto.Last = Convert.ToDecimal(optionQuote.v.lp);
                optionSymbolDto.Ask = Convert.ToDecimal(optionQuote.v.ask);
                optionSymbolDto.Bid = Convert.ToDecimal(optionQuote.v.bid);
                optionSymbolDto.Volume = optionQuote.v.volume;
                optionSymbolDtos.Add(optionSymbolDto);
            }

            optionChain.OptionSymbolDtos = optionSymbolDtos;
            return optionChain;
        }
    }
}
