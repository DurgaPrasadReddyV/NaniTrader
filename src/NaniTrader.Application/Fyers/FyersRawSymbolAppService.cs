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
    [Authorize(NaniTraderPermissions.FyersRawSymbols.Default)]
    public class FyersRawSymbolAppService : NaniTraderAppService, IFyersRawSymbolAppService
    {
        private readonly IFyersRawSymbolRepository _fyersRawSymbolRepository;
        private readonly IFyersCredentialsRepository _fyersCredentialsRepository;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly FyersApiClient _fyersApiClient;

        public FyersRawSymbolAppService(
            IFyersRawSymbolRepository fyersRawSymbolRepository,
            IFyersCredentialsRepository fyersCredentialsRepository,
            IBackgroundJobManager backgroundJobManager,
            FyersApiClient fyersApiClient)
        {
            _fyersRawSymbolRepository = fyersRawSymbolRepository;
            _fyersCredentialsRepository = fyersCredentialsRepository;
            _backgroundJobManager = backgroundJobManager;
            _fyersApiClient = fyersApiClient;
        }

        public async Task<FyersRawSymbolDto> GetAsync(Guid id)
        {
            var fyersRawSymbol = await _fyersRawSymbolRepository.GetAsync(id);
            return ObjectMapper.Map<FyersRawSymbol, FyersRawSymbolDto>(fyersRawSymbol);
        }

        public async Task<PagedResultDto<FyersRawSymbolDto>> GetListAsync(GetFyersRawSymbolListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(FyersRawSymbol.Exchange);
            }

            var fyersRawSymbols = await _fyersRawSymbolRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);

            var totalCount = input.Filter == null
                ? await _fyersRawSymbolRepository.CountAsync()
                : await _fyersRawSymbolRepository.CountAsync(fyersCredentials => fyersCredentials.Exchange.Contains(input.Filter));

            return new PagedResultDto<FyersRawSymbolDto>(totalCount, ObjectMapper.Map<List<FyersRawSymbol>, List<FyersRawSymbolDto>>(fyersRawSymbols));
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
            var symbols = await _fyersRawSymbolRepository.GetUnderlyingSymbolsAsync();

            return symbols.Where(x => x.Equals("NIFTY", StringComparison.OrdinalIgnoreCase) || x.Equals("BANKNIFTY", StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public async Task<List<string>> GetExpiryDatesAsync(string underlyingSymbol)
        {
            return await _fyersRawSymbolRepository.GetExpiryDatesAsync(underlyingSymbol);
        }

        public async Task<List<string>> GetStrikesAsync(string underlyingSymbol)
        {
            return await _fyersRawSymbolRepository.GetStrikesAsync(underlyingSymbol);
        }

        public async Task<OptionChainDto> GetOptionChainAsync(string underlyingSymbol)
        {
            var optionChain = new OptionChainDto();
            optionChain.Underlying = underlyingSymbol;
            var optionSymbolDtos = new List<OptionSymbolDto>();
            var fyersCurrentUserCredentials = await _fyersCredentialsRepository.FindAsync(x => x.UserId == CurrentUser.Id.Value);
            var optionRawSymbols = await _fyersRawSymbolRepository.GetOptionSymbolsAsync(underlyingSymbol);
            var optionSymbolTickers = optionRawSymbols.Select(x => x.Column10).ToList();
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
                var optionRawSymbol = optionRawSymbols.FirstOrDefault(x => x.Column10 == optionSymbolTicker);
                var optionQuote = optionQuotes.FirstOrDefault(x => x.n == optionSymbolTicker);
                optionSymbolDto.Ticker = optionSymbolTicker;
                optionSymbolDto.Strike = Convert.ToDecimal(optionRawSymbol.Column16);
                optionSymbolDto.Expiration = optionRawSymbol.Column9;
                optionSymbolDto.Type = optionRawSymbol.Column17;
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
